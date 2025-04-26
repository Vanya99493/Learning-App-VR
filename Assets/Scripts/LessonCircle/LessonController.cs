using System.Collections.Generic;
using LearningAppVR.UI;
using UnityEngine;

namespace LearningAppVR
{
	public class LessonController : MonoBehaviour
	{
		private QuizPanel _quizPanel;
		private LessonData _lessonData;
		private Dictionary<int, string> _answersContainer = new();
		private Timer _timer;

		private int _questionNumber;

		public void Initialize(QuizPanel quizPanel)
		{
			_quizPanel = quizPanel;
			_quizPanel.Initialize(OnAnswerSelected);
			
			_timer = new Timer();
			_timer.TimerUpdateEvent += OnTimerUpdate;
			_timer.TimerEndEvent += OnTimerEnd;
		}
		
		public void SetupLesson(LessonData lessonData)
		{
			_lessonData = lessonData;
			
			_answersContainer.Clear();
			for (int i = 1; i <= lessonData.QuestionsData.Count; i++)
			{
				_answersContainer.Add(i, null);
			}

			_quizPanel.InitializeQuestionsButtons(lessonData.QuestionsData.Count, OnQuestionButtonWrapperSelected);
			
			SetupQuestion();
			_quizPanel.SetupButtonWrapperState(0, ButtonStateType.Active);
			
			_timer.Start(_lessonData.LessonTime);
		}

		private void EndLesson()
		{
			// TODO: add logic of calculation of the lesson quiz
		}

		private void SetupQuestion(int questionNumber = 1)
		{
			_questionNumber = questionNumber;
			
			_quizPanel.SetupQuestion(_lessonData.QuestionsData[_questionNumber - 1], _answersContainer[_questionNumber]);
		}

		private void OnQuestionButtonWrapperSelected(int questionIndex)
		{
			_quizPanel.SetupButtonWrapperState(_questionNumber - 1, _answersContainer[_questionNumber] is null ? ButtonStateType.Passive : ButtonStateType.Answered);
			_questionNumber = questionIndex;
			_quizPanel.SetupButtonWrapperState(_questionNumber - 1, ButtonStateType.Active);
		}

		private void OnAnswerSelected(string answer)
		{
			var previousAnswer = _answersContainer[_questionNumber];
			if (previousAnswer is null)
			{
				_answersContainer[_questionNumber] = answer;
			}
			else
			{
				_answersContainer[_questionNumber] = null;
			}
			_quizPanel.SetupQuestion(_lessonData.QuestionsData[_questionNumber - 1], _answersContainer[_questionNumber]);
		}

		private void OnTimerUpdate()
		{
			_quizPanel.UpdateTimeCountUI(Mathf.CeilToInt(_timer.Time));
		}

		private void OnTimerEnd()
		{
			if (_lessonData.BlockAnswersAfterTimeOut)
			{
				EndLesson();
			}
		}
	}
}