using System;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class QuizPanel : LessonSubPanel
	{
		[SerializeField]
		private TimeCountElement _timeCountElement;

		[SerializeField]
		private QuestionContainer _questionContainer;

		[SerializeField]
		private QuestionsContainer _questionsContainer;

		public void Initialize(Action<string> onSelectAnswer)
		{
			_questionContainer.Initialize(onSelectAnswer);
		}

		public void InitializeQuestionsButtons(int questionsCount, Action<int> onQuestionButtonClickCallback)
		{
			_questionsContainer.ResetContainer();
			for (int i = 0; i < questionsCount; i++)
			{
				_questionsContainer.AddQuestionButton(onQuestionButtonClickCallback);
			}
		}

		public void SetupQuestion(QuestionData questionData, string previouslySelectedAnswer = null)
		{
			switch (questionData.QuestionType)
			{
				case QuestionType.Variants:
					_questionContainer.SetupVariantsForAnswer(questionData.Question, questionData.Answers);
					break;
				case QuestionType.Input:
					_questionContainer.SetupInputFieldForAnswer(questionData.Question);
					break;
			}
		}

		public void SetupButtonWrapperState(int buttonWrapperIndex, ButtonStateType stateType)
		{
			_questionsContainer.SetupButtonWrapperState(buttonWrapperIndex, stateType);
		}

		public void UpdateTimeCountUI(int timeLeftInSeconds)
		{
			_timeCountElement.Setup(timeLeftInSeconds);
		}
	}
}