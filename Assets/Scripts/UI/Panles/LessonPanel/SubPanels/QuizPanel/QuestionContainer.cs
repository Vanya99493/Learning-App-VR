using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class QuestionContainer : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text _questionText;

		[SerializeField]
		private AnswerButtonsContainer _answerButtonsContainer;

		[SerializeField]
		private AnswerInputFieldContainer _answerInputFieldContainer;

		public void Initialize(Action<string> onSelectAnswer)
		{
			_answerInputFieldContainer.Initialize(onSelectAnswer);
			_answerButtonsContainer.SelectEvent += onSelectAnswer;
		}
		
		public void SetupInputFieldForAnswer(string question, string previousAnswer = null)
		{
			_questionText.text = question;
			_answerInputFieldContainer.Setup(previousAnswer);
			
			_answerButtonsContainer.gameObject.SetActive(false);
			_answerInputFieldContainer.gameObject.SetActive(true);
		}

		public void SetupVariantsForAnswer(string question, List<string> answers, string previousAnswer = null)
		{
			_questionText.text = question;
			_answerButtonsContainer.FillContainer(answers, previousAnswer);
			
			_answerInputFieldContainer.gameObject.SetActive(false);
			_answerButtonsContainer.gameObject.SetActive(true);
		}
	}
}