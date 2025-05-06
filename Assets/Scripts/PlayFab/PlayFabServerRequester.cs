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
		private const string PLAYFAB_KEY_SHARED_ROOM_NAME = "SharedRoom";
		private const string PLAYFAB_KEY_ROOMS_DATA = "Rooms Data";
		
		private const string PLAYFAB_KEY_PERSONAL_DATA = "Personal Data";
		private const string PLAYFAB_KEY_REQUESTS_DATA = "Requests Data";
		
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
			
			if (result.Data.TryGetValue(PLAYFAB_KEY_PERSONAL_DATA, out var teacherRooms))
			{
				userData.OwnedRoomsCollection = JsonUtility.FromJson<OwnedRoomsCollection>(teacherRooms.Value);
			}
			else
			{
				userData.OwnedRoomsCollection = new OwnedRoomsCollection();
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

		public void SavePersonalData(PersonalData personalData)
		{
			var playerStatistics = new Dictionary<string, string>()
			{
				{ PLAYFAB_KEY_PERSONAL_DATA, JsonUtility.ToJson(personalData) }
			};
			
			SavePlayerStatistics(playerStatistics);
		}

		public void SaveRoleRequest(RoleRequestData roleRequestData)
		{
			var playerStatistics = new Dictionary<string, string>()
			{
				{ PLAYFAB_KEY_REQUESTS_DATA, JsonUtility.ToJson(roleRequestData) }
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

		private void CreateSharedRoom()
		{
			var request = new CreateSharedGroupRequest
			{
				SharedGroupId = PLAYFAB_KEY_SHARED_ROOM_NAME
			};

			PlayFabClientAPI.CreateSharedGroup(request,
				result =>
				{
					Debug.Log("Shared group created");
				},
				error =>
				{
					Debug.LogError("Failed to create shared group: " + error.GenerateErrorReport());
				});
		}

		private void OnPublicDataSend(UpdateUserDataResult result)
		{
			Debug.Log("Successful public statistics send");
		}

		private void OnError(PlayFabError error)
		{
			Debug.LogError(error.GenerateErrorReport());
		}
	}
}