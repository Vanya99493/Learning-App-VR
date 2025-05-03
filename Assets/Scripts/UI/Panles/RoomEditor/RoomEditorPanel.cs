using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class RoomEditorPanel : BasePanel
	{
		[SerializeField]
		private TMP_InputField _roomNameInputField;

		[SerializeField]
		private TMP_Dropdown _roomSubjectDropdown;

		[SerializeField]
		private EditLessonsContainer editLessonsContainer;
		
		[SerializeField]
		private Button _leaderboardButton;

		[SerializeField]
		private Button _returnButton;

		public override void Initialize(IUIManager uiManager)
		{
			base.Initialize(uiManager);
			
			editLessonsContainer.Initialize(OnAddLessonEventHandler);
			editLessonsContainer.SelectEvent += OnSelectLevelEventHandler;
			
			_returnButton.onClick.AddListener(OnReturnButtonClick);
			_roomSubjectDropdown.ClearOptions();
			_roomSubjectDropdown.AddOptions(new List<string>()
			{
				SubjectType.All.ToString(),
				SubjectType.Math.ToString(),
				SubjectType.Physic.ToString()
			});
		}

		public void Open(RoomData roomData)
		{
			if (roomData != null)
			{
				_leaderboardButton.onClick.RemoveAllListeners();
				_leaderboardButton.onClick.AddListener(() => _uiManager.OpenLeaderboardPanel(roomData.RoomLeaderboard));
				_roomNameInputField.text = roomData.RoomName;
			
				editLessonsContainer.FillContainer(roomData.Lessons);
			}

			base.Open();
		}

		private void OnSelectLevelEventHandler(LessonData lessonData)
		{
			_uiManager.OpenLessonEditorPanel(lessonData);
		}

		private void OnAddLessonEventHandler()
		{
			_uiManager.OpenLessonEditorPanel(new LessonData());
		}

		private void OnReturnButtonClick()
		{
			_uiManager.OpenOwnedRoomsPanel(false);
		}
	}
}