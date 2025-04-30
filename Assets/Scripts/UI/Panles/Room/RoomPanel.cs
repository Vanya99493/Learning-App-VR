using TMPro;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class RoomPanel : BasePanel
	{
		[SerializeField]
		private LessonStarter _lessonStarter;
		
		[Space(10)]
		[Header("Elements")]
		[SerializeField]
		private TMP_Text _roomNameText;
		
		[SerializeField]
		private TMP_Text _authorText;

		[SerializeField]
		private TMP_Text _subjectText;

		[SerializeField]
		private LessonsContainer _lessonsContainer;

		[SerializeField]
		private LessonInfoContainer _lessonInfoContainer;

		[Space(10)]
		[SerializeField]
		private LeaderboardPanel _leaderboard;

		public override void Initialize(IUIManager uiManager)
		{
			base.Initialize(uiManager);
			
			_leaderboard.Initialize(uiManager);

			_lessonsContainer.SelectLevelEvent += OnSelectLevelEventHandler;
			_lessonInfoContainer.Close();
		}

		public void Open(RoomData roomData)
		{
			_roomNameText.text = roomData.RoomName;
			_authorText.text = roomData.Author;
			_subjectText.text = roomData.SubjectType.ToString();
			_lessonsContainer.FillLessons(roomData.Lessons);
			base.Open();
		}

		public void OnSelectLevelEventHandler(LessonData lessonData)
		{
			_lessonInfoContainer.Open(lessonData, OnStartLevelButtonClick);
		}

		private void OnStartLevelButtonClick(LessonData lessonData)
		{
			_uiManager.CloseCurrentPanel();
			_lessonStarter.PrepareLesson(lessonData);
		}
	}
}