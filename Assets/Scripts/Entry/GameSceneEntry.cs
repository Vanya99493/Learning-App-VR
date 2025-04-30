using LearningAppVR.Player;
using LearningAppVR.UI;
using NaughtyAttributes;
using PlayFab;
using UnityEngine;

namespace LearningAppVR
{
	public class GameSceneEntry : MonoBehaviour
	{
		[SerializeField]
		private PlayerInfo _playerInfo;

		[SerializeField]
		private UIManager _uiManager;

		[SerializeField]
		private LessonStarter _lessonStarter;

		private void Awake()
		{
			_playerInfo.Initialize();
			_uiManager.Initialize();
			//_lessonStarter.Initialize(_uiManager.LessonPanel);
		}

		[Button]
		private void CheckIdGeneratingLogic()
		{
			PlayFabCloudScriptsInvoke.GenerateId(Log);
		}

		private void Log(string result)
		{
			Debug.Log(result);
		}
	}
}