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
		private const string PLAYFAB_KEY_PERSONAL_DATA = "Personal Data";
		private const string PLAYFAB_KEY_TRAINEE_RESULTS_DATA = "Trainee Results Data";
		private const string PLAYFAB_KEY_TEACHER_ROOMS_DATA = "Teacher Rooms Data";
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
			
			if (result.Data.TryGetValue(PLAYFAB_KEY_TRAINEE_RESULTS_DATA, out var traineeResults))
			{
				userData.TraineeResults = JsonUtility.FromJson<TraineeResults>(traineeResults.Value);
			}
			else
			{
				userData.TraineeResults = new TraineeResults();
			}
			
			if (result.Data.TryGetValue(PLAYFAB_KEY_PERSONAL_DATA, out var teacherRooms))
			{
				userData.TeacherRooms = JsonUtility.FromJson<TeacherRooms>(teacherRooms.Value);
			}
			else
			{
				userData.TeacherRooms = new TeacherRooms();
			}

			return userData;
		}

		public void SavePersonalData(PersonalData personalData)
		{
			var playerStatistics = new Dictionary<string, string>()
			{
				{ PLAYFAB_KEY_PERSONAL_DATA, JsonUtility.ToJson(personalData) }
			};
			
			SavePlayerStatistics(playerStatistics);
		}

		public void SaveTraineeResults(TraineeResults traineeResults)
		{
			var playerStatistics = new Dictionary<string, string>()
			{
				{ PLAYFAB_KEY_TRAINEE_RESULTS_DATA, JsonUtility.ToJson(traineeResults) }
			};
			
			SavePlayerStatistics(playerStatistics);
		}

		public void SaveTeacherRooms(TeacherRooms teacherRooms)
		{
			var playerStatistics = new Dictionary<string, string>()
			{
				{ PLAYFAB_KEY_TEACHER_ROOMS_DATA, JsonUtility.ToJson(teacherRooms) }
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

		public void SaveUserData(UserData userData)
		{
			var playerStatistics = new Dictionary<string, string>()
			{
				{ PLAYFAB_KEY_PERSONAL_DATA, JsonUtility.ToJson(userData.PersonalData) },
				{ PLAYFAB_KEY_TRAINEE_RESULTS_DATA, JsonUtility.ToJson(userData.TraineeResults) },
				{ PLAYFAB_KEY_TEACHER_ROOMS_DATA, JsonUtility.ToJson(userData.TeacherRooms) }
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