using System;
using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class LessonEditorPanel : BasePanel, ILessonEditor
	{
		public event Action<LessonData> SaveLessonEvent;
		public event Action<LessonData> DeleteLessonEvent;
		
		[SerializeField]
		private Button _saveButton;

		[SerializeField]
		private Button _returnButton;

		[Space(10)]
		[SerializeField]
		private Button _generalSettingsButton;

		[SerializeField]
		private Button _questionsSettingsButton;

		[SerializeField]
		private Button _deleteLessonButton;

		[Space(10)]
		[SerializeField]
		private GeneralSettingsContainer _generalSettingsContainer;

		[SerializeField]
		private QuestionsSettingsContainer _questionsSettingsContainer;

		[Space(10)]
		[SerializeField]
		private Validator _validator;

		private LessonData _lessonData;

		public override void Initialize(IUIManager uiManager)
		{
			_saveButton.onClick.AddListener(Save);
			_returnButton.onClick.AddListener(Return);
			
			_generalSettingsButton.onClick.AddListener(() => SwitchSettings(SettingsType.General));
			_questionsSettingsButton.onClick.AddListener(() => SwitchSettings(SettingsType.Questions));
			_deleteLessonButton.onClick.AddListener(OnDeleteLessonButtonClick);
			
			_generalSettingsContainer.Initialize();
			_questionsSettingsContainer.Initialize();
			
			base.Initialize(uiManager);
		}

		public void Open(LessonData lessonData)
		{
			_lessonData = lessonData.Clone();
			_generalSettingsContainer.SetupSettings(_lessonData);
			_questionsSettingsContainer.SetupSettings(_lessonData);
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
			if (!_validator.Validate())
			{
				_uiManager.ActivatePopup(new PopupData()
				{
					PopupTextInfo = "Validation failed",
					FirstButtonData = new ButtonData()
					{
						ButtonText = "Continue",
						ButtonCallback = () =>
						{
							_uiManager.DeactivatePopup();
						}
					}
				});
				return;
			}
			
			_lessonData = _generalSettingsContainer.LessonData.Clone();
			
			_lessonData.QuestionsData.Clear();
			foreach (var questionData in _questionsSettingsContainer.LessonData.QuestionsData)
			{
				_lessonData.QuestionsData.Add(questionData.Clone());
			}
			
			SaveLessonEvent?.Invoke(_lessonData);

			_questionsSettingsContainer.IsChanged = false;
			_generalSettingsContainer.IsChanged = false;
		}

		private void Return()
		{
			if (_questionsSettingsContainer.IsChanged || _generalSettingsContainer.IsChanged)
			{
				_uiManager.ActivatePopup(new PopupData()
				{
					PopupTextInfo = "Exit without saving?",
					FirstButtonData = new ButtonData()
					{
						ButtonText = "Exit",
						ButtonCallback = () =>
						{
							_uiManager.DeactivatePopup();
							_uiManager.OpenRoomEditorPanel(null);
						}
					},
					SecondButtonData = new ButtonData()
					{
						ButtonText = "Cancel",
						ButtonCallback = () =>
						{
							_uiManager.DeactivatePopup();
						}
					}
				});
			}
			else
			{
				_uiManager.OpenRoomEditorPanel(null);
			}
		}

		private void OnDeleteLessonButtonClick()
		{
			_uiManager.ActivatePopup(new PopupData()
			{
				PopupTextInfo = "Do you want to delete the lesson?",
				FirstButtonData = new ButtonData()
				{
					ButtonText = "Confirm",
					ButtonCallback = () =>
					{
						_uiManager.DeactivatePopup();
						DeleteLessonEvent?.Invoke(_lessonData);
						_uiManager.OpenRoomEditorPanel(null);
					}
				},
				SecondButtonData = new ButtonData()
				{
					ButtonText = "Cancel",
					ButtonCallback = () =>
					{
						_uiManager.DeactivatePopup();
					}
				}
			});
		}
	}
}