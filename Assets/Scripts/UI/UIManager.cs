using System;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class UIManager : MonoBehaviour, IUIManager
	{
		[SerializeField]
		private PopupsManager _popupsManager;

		[SerializeField]
		private KeyboardsManager _keyboardsManager;

		[Space(10)]
		[Header("Panels")]
		[SerializeField]
		private LoginPanel _loginPanel;
		
		[SerializeField]
		private MainMenuPanel _mainMenuPanel;

		[SerializeField]
		private SettingsPanel _settingsPanel;

		[SerializeField]
		private ProfilePanel _profilePanel;

		[SerializeField]
		private SelectRoomPanel _selectRoomPanel;

		[SerializeField]
		private RoomPanel _roomPanel;

		[SerializeField]
		private LessonPanel _lessonPanel;
		
		[SerializeField]
		private OwnedRoomsPanel _ownedRoomsPanel;

		[SerializeField]
		private RoomEditorPanel _roomEditorPanel;

		[SerializeField]
		private LessonEditorPanel _lessonEditorPanel;

		[SerializeField]
		private LeaderboardPanel _leaderboardPanel;

		[Space(10)]
		[SerializeField]
		private bool _openByDefault;

		private BasePanel _currentActivePanel;

		public LessonPanel LessonPanel => _lessonPanel;
		public ILessonEditor LessonEditor => _lessonEditorPanel;
		public IRoomEditor RoomEditor => _roomEditorPanel;

		public void Initialize()
		{
			_keyboardsManager.Initialize();
			
			_loginPanel.Initialize(this);
			_mainMenuPanel.Initialize(this);
			_settingsPanel.Initialize(this);
			_profilePanel.Initialize(this);
			_selectRoomPanel.Initialize(this);
			_roomPanel.Initialize(this);
			_lessonPanel.Initialize(this);
			_ownedRoomsPanel.Initialize(this);
			_roomEditorPanel.Initialize(this);
			_lessonEditorPanel.Initialize(this);
			_leaderboardPanel.Initialize(this);

			if (_openByDefault)
			{
				OpenNewPanel(_loginPanel);
			}
		}

		public void ActivatePopup(PopupData popupData)
		{
			_popupsManager.ActivatePopup(popupData);
		}

		public void DeactivatePopup()
		{
			_popupsManager.DeactivatePopup();
		}

		public void ActivateKeyboard(KeyboardType keyboardType, Action<KeyboardCode, string> onKeyButtonClick)
		{
			_keyboardsManager.ActivateKeyboard(keyboardType, onKeyButtonClick);
		}

		public void DeactivateKeyboard()
		{
			_keyboardsManager.DeactivateKeyboard();
		}

		public void OpenMainMenuPanel()
		{
			OpenNewPanel(_mainMenuPanel);
		}
		
		public void OpenSettingsPanel()
		{
			OpenNewPanel(_settingsPanel);
		}

		public void OpenProfilePanel()
		{
			OpenNewPanel(_profilePanel);
		}

		public void OpenSelectRoomPanel(bool needToUpdateRoomsCollection)
		{
			CloseCurrentPanel();
			_currentActivePanel = _selectRoomPanel;
			_selectRoomPanel.Open(needToUpdateRoomsCollection);
		}

		public void OpenRoomPanel(RoomData roomData)
		{
			CloseCurrentPanel();
			_currentActivePanel = _roomPanel;
			_roomPanel.Open(roomData);
		}
		
		public void OpenLessonPanel(string roomId, LessonData lessonData)
		{
			CloseCurrentPanel();
			_currentActivePanel = _lessonPanel;
			_lessonPanel.Open(roomId, lessonData);
		}
		
		public void OpenOwnedRoomsPanel(bool needToUpdateRoomsCollection)
		{
			CloseCurrentPanel();
			_currentActivePanel = _ownedRoomsPanel;
			_ownedRoomsPanel.Open(needToUpdateRoomsCollection);
		}

		public void OpenRoomEditorPanel(RoomData roomData)
		{
			CloseCurrentPanel();
			_currentActivePanel = _roomEditorPanel;
			_roomEditorPanel.Open(roomData);
		}

		public void OpenLessonEditorPanel(LessonData lessonData)
		{
			CloseCurrentPanel();
			_currentActivePanel = _lessonEditorPanel;
			_lessonEditorPanel.Open(lessonData);
		}

		public void OpenLeaderboardPanel(string roomId)
		{
			CloseCurrentPanel();
			_currentActivePanel = _leaderboardPanel;
			_leaderboardPanel.Open(roomId);
		}

		public void CloseCurrentPanel()
		{
			_currentActivePanel?.Close();
			_currentActivePanel = null;
		}

		private void OpenNewPanel(BasePanel newPanel)
		{
			_currentActivePanel?.Close();
			_currentActivePanel = newPanel;
			_currentActivePanel.Open();
		}
	}
}