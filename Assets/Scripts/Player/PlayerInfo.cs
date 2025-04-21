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
		}
	}
}