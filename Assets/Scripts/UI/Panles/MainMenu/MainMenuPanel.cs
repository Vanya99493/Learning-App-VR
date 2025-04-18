using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class MainMenuPanel : BasePanel
	{
		[SerializeField]
		private Button _settingsButton;

		public override void Initialize(IUIManager uiManager)
		{
			base.Initialize(uiManager);
			
			_settingsButton.onClick.AddListener(_uiManager.OpenSettingsPanel);
		}
	}
}