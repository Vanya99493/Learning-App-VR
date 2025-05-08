using System.Collections.Generic;
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

		private QuestionData _questionData;

		public void Initialize()
		{
			_variantsAnswersContainer.Initialize();
			_inputAnswerContainer.Initialize();
			
			_variantsAnswersContainer.UpdateVariantsEvent += OnUpdateVariantsEventHandler;
			_inputAnswerContainer.UpdateCorrectAnswerEvent += OnUpdateCorrectAnswerEventHandler;
			
			_variantsButton.onClick.AddListener(() => SwitchAnswerType(QuestionType.Variants));
			_inputButton.onClick.AddListener(() => SwitchAnswerType(QuestionType.Input));
		}
		
		public void Setup(QuestionData questionData)
		{
			_questionData = questionData;
			_inputAnswerContainer.SetupAnswer(_questionData.CorrectAnswer);
			_variantsAnswersContainer.Setup(_questionData.Answers.Clone(), _questionData.CorrectAnswer);
			
			SwitchAnswerType(_questionData.QuestionType);
		}
		
		private void SwitchAnswerType(QuestionType questionType)
		{
			_questionData.QuestionType = questionType;
			
			switch (questionType)
			{
				case QuestionType.Variants:
					_inputAnswerContainer.gameObject.SetActive(false);
					_variantsAnswersContainer.Setup(_questionData.Answers.Clone(), _questionData.CorrectAnswer);
					_variantsAnswersContainer.gameObject.SetActive(true);
					break;
				case QuestionType.Input:
					_variantsAnswersContainer.gameObject.SetActive(false);
					_inputAnswerContainer.SetupAnswer(_questionData.CorrectAnswer);
					_inputAnswerContainer.gameObject.SetActive(true);
					break;
			}
		}

		private void OnUpdateVariantsEventHandler(List<Pair<VariantElement, bool>> setVariants)
		{
			_questionData.Answers.Clear();
			foreach (var variantPair in setVariants)
			{
				_questionData.Answers.Add(variantPair.Key.VariantValue);
				if (variantPair.Value)
				{
					_questionData.CorrectAnswer = variantPair.Key.VariantValue;
				}
			}
		}

		private void OnUpdateCorrectAnswerEventHandler(string newCorrectAnswer)
		{
			_questionData.CorrectAnswer = newCorrectAnswer;
		}
	}
}