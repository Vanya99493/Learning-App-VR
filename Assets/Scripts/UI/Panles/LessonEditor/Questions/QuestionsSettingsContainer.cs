using UnityEngine;

namespace LearningAppVR.UI
{
	public class QuestionsSettingsContainer : MonoBehaviour
	{
		[SerializeField]
		private EditQuestionsContainer _editQuestionsContainer;

		[SerializeField]
		private QuestionEditSettingsContainer _questionEditSettingsContainer;

		public void Initialize()
		{
			_editQuestionsContainer.Initialize(OnAddButtonClick);
			_questionEditSettingsContainer.Initialize();
			
			_editQuestionsContainer.SelectEvent += OnSelectQuestionButtonClick;
		}
		
		public void SetupSettings(LessonData lessonData)
		{
			if (lessonData.QuestionsData.Count > 0)
			{
				
			}
			else
			{
				_questionEditSettingsContainer.gameObject.SetActive(false);
			}
		}

		private void OnAddButtonClick()
		{
			_editQuestionsContainer.AddElement(new QuestionData());
		}

		private void OnSelectQuestionButtonClick(QuestionData questionData)
		{
			_questionEditSettingsContainer.Setup(questionData);
			_questionEditSettingsContainer.gameObject.SetActive(true);
		}
	}
}