using LearningAppVR.Player;
using LearningAppVR.UI;
using UnityEngine;

namespace LearningAppVR
{
	public class LessonStarter : MonoBehaviour
	{
		[SerializeField]
		private PlayerInfo _playerInfo;
		
		[SerializeField]
		private LessonController _lessonController;

		private LessonPanel _lessonPanel;

		public void Initialize(LessonPanel lessonPanel)
		{
			_lessonPanel = lessonPanel;
			
			_lessonController.Initialize(_lessonPanel);
			_lessonPanel.StartLessonEvent += PrepareLesson;
			_lessonPanel.EndCountdownEvent += StartLesson;

			_lessonPanel.ResultsPanel.RestartEvent += ReStartLesson;

			_lessonController.EndLessonEvent += SaveResult;
		}

		private void OnDestroy()
		{
			_lessonPanel.StartLessonEvent -= PrepareLesson;
			_lessonPanel.EndCountdownEvent -= StartLesson;
			
			_lessonPanel.ResultsPanel.RestartEvent -= ReStartLesson;

			_lessonController.EndLessonEvent -= SaveResult;
		}

		private void PrepareLesson(string roomId, LessonData lessonData)
		{
			_lessonController.SetupLesson(roomId, lessonData);
		}

		private void StartLesson()
		{
			_lessonController.StartLesson();
		}

		private void ReStartLesson()
		{
			_lessonController.ReSetupLesson();
			_lessonPanel.OpenPreparationPanel();
		}

		private void SaveResult(ResultData resultData)
		{
			_playerInfo.SaveResult(resultData);
		}
	}
}