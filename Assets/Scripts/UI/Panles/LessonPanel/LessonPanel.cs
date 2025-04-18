using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class LessonPanel : MonoBehaviour
	{
		public event Action<string> SelectAnswerEvent;
		
		[SerializeField]
		private TMP_Text _lessonNameText;

		[SerializeField]
		private TMP_Text _questionText;
		
		[SerializeField]
		private List<AnswerButton> _answerButtons = new();

		public void Initialize(string lessonNameText)
		{
			ResetPanel();
			_lessonNameText.text = lessonNameText;
		}

		public void ResetPanel()
		{
			foreach (var answerButton in _answerButtons)
			{
				answerButton.DeInitialize();
			}

			_lessonNameText.text = "Lesson name";
		}

		public void SetupQuestion(QuestionData questionData)
		{
			_questionText.text = questionData.Question;
			InitializeAnswersButtons(questionData.Answers);
		}

		private void InitializeAnswersButtons(List<string> answers)
		{
			int answerIndex = 0;
			foreach (var answerButton in _answerButtons)
			{
				if (answerIndex < answers.Count)
				{
					string answer = answers[answerIndex];
					answerButton.Initialize(answer, () => OnAnswerButtonClickEventHandler(answer));
				}
				else
				{
					answerButton.DeInitialize();
				}

				answerIndex++;
			}
		}

		private void OnAnswerButtonClickEventHandler(string selectedAnswer)
		{
			SelectAnswerEvent?.Invoke(selectedAnswer);
		}
	}
}