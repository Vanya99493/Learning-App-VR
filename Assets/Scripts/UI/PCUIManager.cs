using UnityEngine;

namespace LearningAppVR.UI
{
	public class PCUIManager : MonoBehaviour, IPCUIManager
	{
		[SerializeField]
		private PopupsManager _popupsManager;

		[Space(10)]
		[SerializeField]
		private LoginPanel _loginPanel;
		
		[Space(10)]
		[SerializeField]
		private bool _openByDefault;

		private BasePanel _currentActivePanel;

		public void Initialize()
		{
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