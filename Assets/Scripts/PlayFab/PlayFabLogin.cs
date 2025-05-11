using System;
using PlayFab.ClientModels;
using UnityEngine;

namespace PlayFab
{
	public class PlayFabLogin : MonoBehaviour
	{
		public void CheckAccountExists(string username, string password, Action<bool, PlayFabErrorCode> callback)
		{
			var request = new LoginWithPlayFabRequest
			{
				Username = username,
				Password = password
			};

			PlayFabClientAPI.LoginWithPlayFab(request,
				result => {
					Debug.Log("Login successful");
					callback?.Invoke(true, PlayFabErrorCode.Success);
				},
				error => {
					Debug.LogWarning("Login failed: " + error.ErrorMessage);

					switch (error.Error)
					{
						case PlayFabErrorCode.AccountNotFound:
							callback?.Invoke(false, PlayFabErrorCode.AccountNotFound);
							break;
						case PlayFabErrorCode.InvalidUsernameOrPassword:
							callback?.Invoke(false, PlayFabErrorCode.InvalidUsernameOrPassword);
							break;
						default:
							Debug.LogError("Login error: " + error.GenerateErrorReport());
							callback?.Invoke(false, PlayFabErrorCode.Unknown);
							break;
					}
				});
		}

		public void RegisterAccount(string username, string password, Action<bool> callback)
		{
			var request = new RegisterPlayFabUserRequest
			{
				Username = username,
				Password = password,
				RequireBothUsernameAndEmail = false
			};

			PlayFabClientAPI.RegisterPlayFabUser(request,
				result => {
					Debug.Log("Registration successful");

					var displayNameRequest = new UpdateUserTitleDisplayNameRequest
					{
						DisplayName = username
					};

					PlayFabClientAPI.UpdateUserTitleDisplayName(displayNameRequest,
						displayResult => {
							Debug.Log("DisplayName set successfully");
							callback?.Invoke(true);
						},
						displayError => {
							Debug.LogWarning("Failed to set DisplayName: " + displayError.GenerateErrorReport());
							callback?.Invoke(false);
						});
				},
				error => {
					Debug.LogError("Registration failed: " + error.GenerateErrorReport());
					callback?.Invoke(false);
				});
		}
	}
}