using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class SettingsPanel : BasePanel
	{
		[SerializeField]
		private Button _mainMenuButton;

		public override void Initialize(IUIManager uiManager)
		{
			base.Initialize(uiManager);

			_mainMenuButton.onClick.AddListener(_uiManager.OpenMainMenuPanel);
		}
	}
}