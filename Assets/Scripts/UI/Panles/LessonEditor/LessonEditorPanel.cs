using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class LessonEditorPanel : BasePanel
	{
		[SerializeField]
		private Button _saveButton;

		[SerializeField]
		private Button _returnButton;

		[Space(10)]
		[SerializeField]
		private Button _generalSettingsButton;

		[SerializeField]
		private Button _questionsSettingsButton;

		[Space(10)]
		[SerializeField]
		private GeneralSettingsContainer _generalSettingsContainer;

		[SerializeField]
		private QuestionsSettingsContainer _questionsSettingsContainer;

		public override void Initialize(IUIManager uiManager)
		{
			_saveButton.onClick.AddListener(Save);
			_returnButton.onClick.AddListener(() => _uiManager.OpenRoomEditorPanel(null));
			
			_generalSettingsButton.onClick.AddListener(() => SwitchSettings(SettingsType.General));
			_questionsSettingsButton.onClick.AddListener(() => SwitchSettings(SettingsType.Questions));
			
			_generalSettingsContainer.Initialize();
			_questionsSettingsContainer.Initialize();
			
			base.Initialize(uiManager);
		}

		public void Open(LessonData lessonData)
		{
			_generalSettingsContainer.SetupSettings(lessonData);
			_questionsSettingsContainer.SetupSettings(lessonData);
			SwitchSettings(SettingsType.General);
			
			base.Open();
		}

		private void SwitchSettings(SettingsType settingsType)
		{
			switch (settingsType)
			{
				case SettingsType.General:
					_questionsSettingsContainer.gameObject.SetActive(false);
					_generalSettingsContainer.gameObject.SetActive(true);
					break;
				case SettingsType.Questions:
					_generalSettingsContainer.gameObject.SetActive(false);
					_questionsSettingsContainer.gameObject.SetActive(true);
					break;
			}
		}

		private void Save()
		{
			// TODO: add saving of the data
		}
	}
}