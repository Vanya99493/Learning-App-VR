using LearningAppVR.Player;
using LearningAppVR.UI;
using UnityEngine;

namespace LearningAppVR
{
	public class LessonStarter : MonoBehaviour
	{
		[SerializeField]
		private DataProvider _dataProvider;
		
		[SerializeField]
		private LessonController _lessonController;

		private LessonPanel _lessonPanel;

		private string _roomId;
		private LessonData _lessonData;

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
			_roomId = roomId;
			_lessonData = lessonData;
			_lessonController.SetupLesson(_roomId, _lessonData);
		}

		private void StartLesson()
		{
			_lessonController.StartLesson();
		}

		private void ReStartLesson()
		{
			_lessonController.SetupLesson(_roomId, _lessonData);
			_lessonPanel.OpenPreparationPanel();
		}

		private void SaveResult(ResultData resultData)
		{
			_dataProvider.PlayerInfo.SaveResult(resultData);
		}
	}
}