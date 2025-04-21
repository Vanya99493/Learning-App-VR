using System;
using PlayFab;
using UnityEngine;

namespace LearningAppVR.Player
{
	public class PlayerInfo : MonoBehaviour
	{
		private const string REMEMBER_ME_KEY = "remember me";
		private const string USERNAME_KEY = "username";
		private const string PASSWORD_KEY = "password";

		[SerializeField]
		private bool _cleanRememberedUserInfo;
		
		public bool HasRememberedInfo { get; private set; }
		public string UserName { get; private set; }
		public string Password { get; private set; }
		public UserRole UserRole => _userData.PersonalData.UserRole;

		private PlayFabServerRequester _playFabServerRequester;
		private UserData _userData;
		
		private bool _isInitialized = false;
		private Action _finishInitializingSubscribers;

		public void Initialize()
		{
			if (_cleanRememberedUserInfo)
			{
				PlayerPrefs.SetInt(REMEMBER_ME_KEY, 0);
			}
			
			InitializeUserInfo();
		}

		private void InitializeUserInfo()
		{
			HasRememberedInfo = PlayerPrefs.GetInt(REMEMBER_ME_KEY, 0) != 0;

			if (HasRememberedInfo)
			{
				UserName = PlayerPrefs.GetString(USERNAME_KEY);
				Password = PlayerPrefs.GetString(PASSWORD_KEY);
			}
		}

		public void SetupUserInfo(string username, string password)
		{
			UserName = username;
			Password = password;
			
			PlayerPrefs.SetString(USERNAME_KEY, UserName);
			PlayerPrefs.SetString(PASSWORD_KEY, Password);
			PlayerPrefs.SetInt(REMEMBER_ME_KEY, 1);

			HasRememberedInfo = true;
			InitializeUserData();
		}

		public void SubscribeOnFinishInitialization(Action subscriber)
		{
			if (_isInitialized)
			{
				subscriber?.Invoke();
				return;
			}

			_finishInitializingSubscribers += subscriber;
		}

		public void SendTeacherRoleRequest()
		{
			_playFabServerRequester.SaveRoleRequest(new RoleRequestData()
			{
				HasTeacherRoleRequest = true
			});
		}

		private async void InitializeUserData()
		{
			_playFabServerRequester = new();

			_userData = await _playFabServerRequester.GetUserData();

			_isInitialized = true;
			_finishInitializingSubscribers?.Invoke();
		}
	}
}