using System;
using System.Collections.Generic;
using PlayFab.ClientModels;
using PlayFab.Json;
using UnityEngine;
using ExecuteCloudScriptRequest = PlayFab.ClientModels.ExecuteCloudScriptRequest;

namespace PlayFab
{
	public static class PlayFabCloudScriptsInvoke
	{
		private static Action<string> _successCallbackSubscribers;
		
		public static void GenerateId(Action<string> onSuccessCallback)
		{
			_successCallbackSubscribers = null;
			_successCallbackSubscribers += onSuccessCallback;
			
			var request = new ExecuteCloudScriptRequest()
			{
				FunctionName = "GenerateSequentialKey",
				GeneratePlayStreamEvent = false
			};
			
			PlayFabClientAPI.ExecuteCloudScript(request, OnSuccess, OnError);
		}
		
		private static void OnSuccess(ExecuteCloudScriptResult result)
		{
			if (result.FunctionResult == null)
			{
				Debug.LogWarning("CloudScript returned null.");
				return;
			}
			
			if (result.FunctionResult is Dictionary<string, object> data && data.TryGetValue("key", out var key))
			{
				Debug.Log("Отриманий ключ: " + key);
				_successCallbackSubscribers?.Invoke((string)key);
			}
			else
			{
				Debug.LogWarning("Ключ не знайдено у результаті.");
			}
		}

		private static void OnError(PlayFabError error)
		{
			Debug.LogError("CloudScript error: " + error.GenerateErrorReport());
		}
	}
}