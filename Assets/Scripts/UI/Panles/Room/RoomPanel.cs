using LearningAppVR.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class RoomPanel : BasePanel
	{
		[SerializeField]
		private DataProvider _dataProvider;

		[Space(5)]
		[Header("Elements")]
		[SerializeField]
		private Button _returnButton;
		
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
		private LeaderboardElement _leaderboard;

		private string _roomId;
		
		public override void Initialize(IUIManager uiManager)
		{
			base.Initialize(uiManager);

			_returnButton.onClick.AddListener(() => _uiManager.OpenSelectRoomPanel(false));
			_lessonsContainer.SelectEvent += OnSelectLevelEventHandler;
			_lessonInfoContainer.Close();
		}

		public void Open(RoomData roomData)
		{
			_roomId = roomData.Id;
			_roomNameText.text = roomData.RoomName;
			_authorText.text = roomData.Author;
			_subjectText.text = roomData.SubjectType.ToString();
			_lessonsContainer.FillContainer(roomData.Lessons);
			
			FillLeaderboard();
			base.Open();
		}

		public override void Close()
		{
			_lessonInfoContainer.Close();
			base.Close();
		}

		private async void FillLeaderboard()
		{
			var result = await _dataProvider.GetLeaderboardData(_roomId);
			_leaderboard.FillContainer(result.UserResults);
		}

		private void OnSelectLevelEventHandler(LessonData lessonData)
		{
			_lessonInfoContainer.Open(lessonData, OnStartLevelButtonClick);
		}

		private void OnStartLevelButtonClick(LessonData lessonData)
		{
			_uiManager.OpenLessonPanel(_roomId, lessonData);
		}
	}
}