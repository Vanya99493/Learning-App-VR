using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LearningAppVR
{
	public class SceneLoader
	{
		public async void LoadScene(string sceneName, Action callback = null)
		{
			await SceneManager.LoadSceneAsync(sceneName);
			callback?.Invoke();
		}
	}
}