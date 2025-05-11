using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class MainMenuPanel : BasePanel
	{
		[SerializeField]
		private Button _startButton;
		
		[SerializeField]
		private Button _profileButton;

		[SerializeField]
		private Button _settingsButton;

		[SerializeField]
		private Button _exitButton;

		public override void Initialize(IUIManager uiManager)
		{
			base.Initialize(uiManager);
			
			_startButton.onClick.AddListener(() => _uiManager.OpenSelectRoomPanel(true));
			_profileButton.onClick.AddListener(_uiManager.OpenProfilePanel);
			_settingsButton.onClick.AddListener(_uiManager.OpenSettingsPanel);
			_exitButton.onClick.AddListener(OnExitButtonClick);
		}

		public override void Open()
		{
			_startButton.gameObject.SetActive(!AppSettings.BlockLessonsExperienceOnPC || AppSettings.DeviceType == DeviceType.VR);
			
			base.Open();
		}

		private void OnExitButtonClick()
		{
			Application.Quit();
		}

		[Button("Open")]
		public void Foo()
		{
			_startButton.onClick.Invoke();
		}
	}
}