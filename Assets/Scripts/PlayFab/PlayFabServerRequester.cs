using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LearningAppVR;
using PlayFab.ClientModels;
using UnityEngine;

namespace PlayFab
{
	public class PlayFabServerRequester
	{
		private const string PLAYFAB_KEY_ROOMS_DATA = "Rooms Data";
		private const string PLAYFAB_KEY_PERSONAL_DATA = "Personal Data";
		private const string PLAYFAB_KEY_REQUESTS_DATA = "Requests Data";
		private const string PLAYFAB_KEY_RESULTS_DATA = "Results Data";
		
		public async Task<UserData> GetUserData()
		{
			var tcs = new TaskCompletionSource<UserData>();
			
			PlayFabClientAPI.GetUserData(
				new GetUserDataRequest(),
				result => tcs.SetResult(ParseData(result)), 
				error => tcs.SetException(new Exception(error.GenerateErrorReport())));

			return await tcs.Task;
		}

		private UserData ParseData(GetUserDataResult result)
		{
			UserData userData = new();
			
			if (result.Data.TryGetValue(PLAYFAB_KEY_PERSONAL_DATA, out var personalData))
			{
				userData.PersonalData = JsonUtility.FromJson<PersonalData>(personalData.Value);
			}
			else
			{
				userData.PersonalData = new PersonalData();
				SavePersonalData(userData.PersonalData);
			}

			return userData;
		}

		public async Task<RoomsCollection> GetAllRoomsData()
		{
			var tcs = new TaskCompletionSource<RoomsCollection>();

			var request = new GetTitleDataRequest();

			PlayFabClientAPI.GetTitleData(request, result =>
				{
					if (result.Data != null && result.Data.ContainsKey(PLAYFAB_KEY_ROOMS_DATA))
					{
						string json = result.Data[PLAYFAB_KEY_ROOMS_DATA];
						Debug.Log("Rooms Title Data: " + json);
						var rooms = JsonUtility.FromJson<RoomsCollection>(json);
						Debug.Log("Loaded Rooms: " + rooms.Rooms.Count);
						tcs.SetResult(rooms);
					}
					else
					{
						Debug.Log("No Title Data found for 'Rooms'");
						tcs.SetResult(new RoomsCollection());
					}
				},
				error =>
				{
					Debug.LogError("Error getting title data: " + error.GenerateErrorReport());
					tcs.SetResult(new RoomsCollection());
				});

			await tcs.Task;

			return tcs.Task.Result;
		}

		public async Task<RoomsCollection> GetOwnedRoomsData(string username)
		{
			var allRoomsCollection = await GetAllRoomsData();

			RoomsCollection ownedRoomsCollection = new RoomsCollection();

			foreach (var roomData in allRoomsCollection.Rooms)
			{
				if (roomData.Author == username)
				{
					ownedRoomsCollection.Rooms.Add(roomData);
				}
			}

			return ownedRoomsCollection;
		}

		public async Task<bool> UpdateRoomsData(RoomsCollection roomsCollection)
		{
			var tcs = new TaskCompletionSource<bool>();
			var json = JsonUtility.ToJson(roomsCollection);

			var request = new ExecuteCloudScriptRequest
			{
				FunctionName = "SetRoomsData",
				FunctionParameter = new Dictionary<string, object>
				{
					{ "roomsJson", json }
				},
				GeneratePlayStreamEvent = false
			};

			PlayFabClientAPI.ExecuteCloudScript(request, result =>
				{
					Debug.Log("Cloud Script executed: " + result.FunctionResult.ToString());
					tcs.SetResult(true);
				},
				error =>
				{
					Debug.LogError("Cloud Script error: " + error.GenerateErrorReport());
					tcs.SetResult(false);
				});
			
			await tcs.Task;

			return tcs.Task.Result;
		}

		public async Task<RoomLeaderboardData> GetLeaderboard(string roomId)
		{
			var tcs = new TaskCompletionSource<RoomLeaderboardData>();
			
			var request = new GetLeaderboardRequest
			{
				StatisticName = $"{roomId}",
				StartPosition = 0,
				MaxResultsCount = 100
			};
			PlayFabClientAPI.GetLeaderboard(request,
				result => tcs.SetResult(GetRoomLeaderboard(result)),
				error => tcs.SetException(new Exception(error.GenerateErrorReport())));

			return await tcs.Task;
		}
		
		private RoomLeaderboardData GetRoomLeaderboard(GetLeaderboardResult result)
		{
			RoomLeaderboardData leaderboardCollection = new();
			foreach (var leaderboardEntry in result.Leaderboard)
			{
				leaderboardCollection.UserResults.Add(new UserResult()
				{
					Position = (leaderboardEntry.Position + 1).ToString(),
					UserName = leaderboardEntry.DisplayName,
					Score = leaderboardEntry.StatValue
				});
			}

			return leaderboardCollection;
		}
		
		public async Task SaveUserResult(ResultData resultData)
		{
			var userResultsCollection = await GetUserStatistics();

			bool findResult = false;
			foreach (var previousResultData in userResultsCollection.Results)
			{
				if (previousResultData.RoomId == resultData.RoomId &&
				    previousResultData.LessonId == resultData.LessonId)
				{
					if (previousResultData.EarnedPoints < resultData.EarnedPoints)
					{
						Debug.Log($"Change old result ({previousResultData.EarnedPoints}) to {resultData.EarnedPoints}");
						previousResultData.EarnedPoints = resultData.EarnedPoints;
						SendLeaderboard(resultData.RoomId, CalculateRoomScore(userResultsCollection, resultData.RoomId));
						SaveResultsData(userResultsCollection);
					}
					findResult = true;
					break;
				}
			}

			if (!findResult)
			{
				Debug.Log($"Add new result {resultData.EarnedPoints}");
				userResultsCollection.Results.Add(resultData);
				SendLeaderboard(resultData.RoomId, CalculateRoomScore(userResultsCollection, resultData.RoomId));
				SaveResultsData(userResultsCollection);
			}
		}

		private async Task<ResultsCollectionData> GetUserStatistics()
		{
			var tcs = new TaskCompletionSource<ResultsCollectionData>();
			
			PlayFabClientAPI.GetUserData(
				new GetUserDataRequest(),
				result => tcs.SetResult(ParseUserStatistics(result)), 
				error => tcs.SetException(new Exception(error.GenerateErrorReport())));

			return await tcs.Task;
		}

		private ResultsCollectionData ParseUserStatistics(GetUserDataResult result)
		{
			ResultsCollectionData userResults = new();
			
			if (result.Data.TryGetValue(PLAYFAB_KEY_RESULTS_DATA, out var personalData))
			{
				userResults = JsonUtility.FromJson<ResultsCollectionData>(personalData.Value);
			}
			else
			{
				userResults = new ResultsCollectionData();
			}

			return userResults;
		}
		
		private void SendLeaderboard(string roomId, int score)
		{
			var request = new UpdatePlayerStatisticsRequest
			{
				Statistics = new List<StatisticUpdate>
				{
					new StatisticUpdate
					{
						StatisticName = roomId,
						Value = score
					}
				}
			};
			PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
		}

		public void SaveRoleRequest(RoleRequestData roleRequestData)
		{
			var playerStatistics = new Dictionary<string, string>()
			{
				{ PLAYFAB_KEY_REQUESTS_DATA, JsonUtility.ToJson(roleRequestData) }
			};
			
			SavePlayerStatistics(playerStatistics);
		}

		private void SavePersonalData(PersonalData personalData)
		{
			var playerStatistics = new Dictionary<string, string>()
			{
				{ PLAYFAB_KEY_PERSONAL_DATA, JsonUtility.ToJson(personalData) }
			};
			
			SavePlayerStatistics(playerStatistics);
		}

		private void SaveResultsData(ResultsCollectionData resultsCollectionData)
		{
			var playerStatistics = new Dictionary<string, string>()
			{
				{ PLAYFAB_KEY_RESULTS_DATA, JsonUtility.ToJson(resultsCollectionData) }
			};
			
			SavePlayerStatistics(playerStatistics);
		}
		
		private void SavePlayerStatistics(Dictionary<string, string> playerStatistics, UserDataPermission userDataPermission = UserDataPermission.Public)
		{
			var request = new UpdateUserDataRequest()
			{
				Data = playerStatistics,
				Permission = userDataPermission
			};
			PlayFabClientAPI.UpdateUserData(request, OnPublicDataSend, OnError);
		}

		private int CalculateRoomScore(ResultsCollectionData resultsCollectionData, string targetRoomId)
		{
			int score = 0;
			foreach (var resultData in resultsCollectionData.Results)
			{
				if (resultData.RoomId == targetRoomId)
				{
					score += resultData.EarnedPoints;
				}
			}
			
			return score;
		}

		private void OnPublicDataSend(UpdateUserDataResult result)
		{
			Debug.Log("Successful public statistics send");
		}

		private void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
		{
			Debug.Log("Successful update leaderboard score");
		}

		private void OnError(PlayFabError error)
		{
			Debug.LogError(error.GenerateErrorReport());
		}
	}
}