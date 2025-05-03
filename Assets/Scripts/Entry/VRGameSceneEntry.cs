using NaughtyAttributes;
using PlayFab;
using UnityEngine;

namespace LearningAppVR
{
	public class VRGameSceneEntry : EntryPoint
	{
		[SerializeField]
		private LessonStarter _lessonStarter;

		protected override void Initialize()
		{
			base.Initialize();
			
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