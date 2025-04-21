using UnityEngine;

namespace LearningAppVR.UI
{
	public class UIManager : MonoBehaviour, IUIManager
	{
		[SerializeField]
		private PopupsManager _popupsManager;

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

		[Space(10)]
		[SerializeField]
		private bool _openByDefault;

		private BasePanel _currentActivePanel;

		public void Initialize()
		{
			_loginPanel.Initialize(this);
			_mainMenuPanel.Initialize(this);
			_settingsPanel.Initialize(this);
			_profilePanel.Initialize(this);

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