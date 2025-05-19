using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class QuizPanel : LessonSubPanel
	{
		public event Action LessonTimeEndEvent;
		public event Action FinishLessonEvent;

		[SerializeField]
		private TimeCountElement _timeCountElement;

		[SerializeField]
		private Button _finishButton;

		[SerializeField]
		private QuestionContainer _questionContainer;

		[SerializeField]
		private QuestionsContainer _questionsContainer;

		public void Initialize(ILessonPanel lessonPanel, Action<string> onSelectAnswer)
		{
			_timeCountElement.Initialize();
			_questionContainer.Initialize(lessonPanel, onSelectAnswer);
			
			_finishButton.onClick.AddListener(OnFinishButtonClick);

			_timeCountElement.TimerEndEvent += OnEndTimerEventHandler;
		}

		public void Open(int timeInSeconds)
		{
			_timeCountElement.Setup(timeInSeconds);
			base.Open();
		}

		public void InitializeQuestionsButtons(int questionsCount, Action<int> onQuestionButtonClick)
		{
			List<int> intCollection = new();
			for (int i = 1; i <= questionsCount; i++)
			{
				intCollection.Add(i);
			}
			_questionsContainer.FillContainer(intCollection);
			_questionsContainer.SelectEvent += onQuestionButtonClick;
		}

		public void SetupQuestion(QuestionData questionData, string previousAnswer)
		{
			switch (questionData.QuestionType)
			{
				case QuestionType.Variants:
					_questionContainer.SetupVariantsForAnswer(questionData.Question, questionData.Answers, previousAnswer);
					break;
				case QuestionType.Input:
					_questionContainer.SetupInputFieldForAnswer(questionData.Question, previousAnswer);
					break;
			}
		}

		private void OnEndTimerEventHandler()
		{
			_lessonPanel.UIManager.DeactivatePopup();
			LessonTimeEndEvent?.Invoke();
		}

		private void OnFinishButtonClick()
		{
			_lessonPanel.UIManager.ActivatePopup(new PopupData()
			{
				PopupTextInfo = "Finish the lesson?",
				FirstButtonData = new ButtonData()
				{
					ButtonText = "Confirm",
					ButtonCallback = () =>
					{
						_lessonPanel.UIManager.DeactivatePopup();
						FinishLessonEvent?.Invoke();
					}
				},
				SecondButtonData = new ButtonData()
				{
					ButtonText = "Cancel",
					ButtonCallback = () =>
					{
						_lessonPanel.UIManager.DeactivatePopup();
					}
				}
			});
		}
	}
}