using System;
using System.Collections.Generic;
using LearningAppVR.UI;
using UnityEngine;

namespace LearningAppVR
{
	public class LessonController : MonoBehaviour
	{
		public event Action<ResultData> EndLessonEvent;
		
		private LessonPanel _lessonPanel;
		private string _roomId;
		private LessonData _lessonData;
		private Dictionary<int, string> _answersContainer = new();

		private int _questionNumber;

		public void Initialize(LessonPanel lessonPanel)
		{
			_lessonPanel = lessonPanel;
			_lessonPanel.QuizPanel.Initialize(OnAnswerSelected);
			_lessonPanel.QuizPanel.LessonTimeEndEvent += OnTimerEnd;
			_lessonPanel.QuizPanel.FinishLessonEvent += EndLesson;
		}
		
		public void SetupLesson(string roomId, LessonData lessonData)
		{
			_roomId = roomId;
			_lessonData = lessonData;
			
			_answersContainer.Clear();
			for (int i = 1; i <= lessonData.QuestionsData.Count; i++)
			{
				_answersContainer.Add(i, null);
			}

			_lessonPanel.QuizPanel.InitializeQuestionsButtons(lessonData.QuestionsData.Count, SetupQuestion);
		}

		public void ReSetupLesson()
		{
			SetupLesson(_roomId, _lessonData);
		}

		public void StartLesson()
		{
			_lessonPanel.OpenQuizPanel(_lessonData.LessonTime);
			SetupQuestion();
		}

		private void EndLesson()
		{
			int earnedPoints = CalculatePoints();
			EndLessonEvent?.Invoke(new ResultData()
			{
				RoomId = _roomId,
				LessonId = _lessonData.Id,
				EarnedPoints = earnedPoints
			});
			_lessonPanel.OpenResultsPanel(earnedPoints, _lessonData.GetGlobalPoints());
		}

		private void SetupQuestion(int questionNumber = 1)
		{
			_questionNumber = questionNumber;
			_lessonPanel.QuizPanel.SetupQuestion(_lessonData.QuestionsData[questionNumber - 1], _answersContainer[questionNumber]);
		}

		private void OnAnswerSelected(string answer)
		{
			var previousAnswer = _answersContainer[_questionNumber];
			if (previousAnswer is null)
			{
				_answersContainer[_questionNumber] = answer;
			}
		}

		private void OnTimerEnd()
		{
			if (_lessonData.BlockAnswersAfterTimeOut)
			{
				EndLesson();
			}
		}

		private int CalculatePoints()
		{
			int earnedPoints = 0;
			int index = 1;
			
			foreach (var questionData in _lessonData.QuestionsData)
			{
				earnedPoints += _answersContainer[index] is not null && _answersContainer[index] == questionData.CorrectAnswer ? questionData.Points : 0;
				index++;
			}

			return earnedPoints;
		}
	}
}