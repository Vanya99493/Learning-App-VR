using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class AnswerSelectionContainer : MonoBehaviour
	{
		[SerializeField]
		private Button _variantsButton;

		[SerializeField]
		private Button _inputButton;

		[SerializeField]
		private VariantsAnswersContainer _variantsAnswersContainer;

		[SerializeField]
		private InputAnswerContainer _inputAnswerContainer;

		private QuestionType _activeQuestionType;

		public void Initialize()
		{
			_variantsAnswersContainer.Initialize();
			
			_variantsButton.onClick.AddListener(() => SwitchAnswerType(QuestionType.Variants));
			_inputButton.onClick.AddListener(() => SwitchAnswerType(QuestionType.Input));
		}
		
		public void Setup(QuestionData questionData)
		{
			_inputAnswerContainer.SetupAnswer(questionData.CorrectAnswer);
			_variantsAnswersContainer.Setup(questionData.Answers, questionData.CorrectAnswer);
			
			SwitchAnswerType(questionData.QuestionType);
		}
		
		private void SwitchAnswerType(QuestionType questionType)
		{
			_activeQuestionType = questionType;
			
			switch (questionType)
			{
				case QuestionType.Variants:
					_inputAnswerContainer.gameObject.SetActive(false);
					_variantsAnswersContainer.gameObject.SetActive(true);
					break;
				case QuestionType.Input:
					_variantsAnswersContainer.gameObject.SetActive(false);
					_inputAnswerContainer.gameObject.SetActive(true);
					break;
			}
		}
	}
}