using UnityEngine;

namespace LearningAppVR
{
	public static class LocalIdGenerator
	{
		private const string PLAYER_PREFS_LAST_ID_KEY = "LastKey";
		
		public static string GetId()
		{
			string key = PlayerPrefs.GetString(PLAYER_PREFS_LAST_ID_KEY, "aaaaaa");
			char[] chars = key.ToCharArray();
			for (int i = chars.Length - 1; i >= 0; i--)
			{
				if (chars[i] < 'z')
				{
					chars[i]++;
					for (int j = i + 1; j < chars.Length; j++)
					{
						chars[j] = 'a';
					}

					break;
				}
			}

			key = new string(chars);
			PlayerPrefs.SetString(PLAYER_PREFS_LAST_ID_KEY, key);
			return key;
		}
	}
}