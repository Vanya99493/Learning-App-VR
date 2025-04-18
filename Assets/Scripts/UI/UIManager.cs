using System;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class UIManager : MonoBehaviour, IUIManager
	{
		[SerializeField]
		private MainMenuPanel _mainMenuPanel;

		[SerializeField]
		private SettingsPanel _settingsPanel;

		private BasePanel _currentActivePanel;

		private void Awake()
		{
			_mainMenuPanel.Initialize(this);
			_settingsPanel.Initialize(this);
			
			OpenNewPanel(_mainMenuPanel);
		}

		public void OpenMainMenuPanel()
		{
			OpenNewPanel(_mainMenuPanel);
		}
		
		public void OpenSettingsPanel()
		{
			OpenNewPanel(_settingsPanel);
		}

		private void OpenNewPanel(BasePanel newPanel)
		{
			_currentActivePanel?.Close();
			_currentActivePanel = newPanel;
			_currentActivePanel.Open();
		}
	}
}