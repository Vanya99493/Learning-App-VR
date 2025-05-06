using UnityEngine;

namespace LearningAppVR.UI
{
	public class QuestionsSettingsContainer : MonoBehaviour
	{
		[SerializeField]
		private EditQuestionsContainer _editQuestionsContainer;

		[SerializeField]
		private QuestionEditSettingsContainer _questionEditSettingsContainer;

		public LessonData LessonData;
		public bool IsChanged;
		
		public void Initialize()
		{
			_editQuestionsContainer.Initialize(OnAddButtonClick);
			_questionEditSettingsContainer.Initialize();
			
			_editQuestionsContainer.SelectEvent += OnSelectQuestionButtonClick;
		}
		
		public void SetupSettings(LessonData lessonData)
		{
			LessonData = lessonData.Clone();
			
			_editQuestionsContainer.FillContainer(LessonData.QuestionsData);
			
			if (LessonData.QuestionsData.Count <= 0)
			{
				_questionEditSettingsContainer.gameObject.SetActive(false);
			}
		}

		private void OnAddButtonClick()
		{
			var questionData = new QuestionData();
			LessonData.QuestionsData.Add(questionData);
			_editQuestionsContainer.AddElement(questionData);
		}

		private void OnSelectQuestionButtonClick(QuestionData questionData)
		{
			_questionEditSettingsContainer.Setup(questionData);
			_questionEditSettingsContainer.gameObject.SetActive(true);
		}
	}
}