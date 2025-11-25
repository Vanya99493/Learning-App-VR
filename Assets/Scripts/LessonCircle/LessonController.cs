using System;
using System.Collections.Generic;
using LearningAppVR.UI;
using UnityEngine;

namespace LearningAppVR
{
	public class LessonController : MonoBehaviour
	{
		public event Action<GlobalResultData> EndLessonEvent;
		
		private readonly Dictionary<int, string> _answersContainer = new();
		
		private LessonPanel _lessonPanel;
		private string _roomId;
		private LessonData _lessonData;

		private int _questionNumber;

		public void Initialize(LessonPanel lessonPanel)
		{
			_lessonPanel = lessonPanel;
			_lessonPanel.QuizPanel.Initialize(lessonPanel, OnAnswerSelected);
			_lessonPanel.QuizPanel.LessonTimeEndEvent += OnTimerEnd;
			_lessonPanel.QuizPanel.FinishLessonEvent += EndLesson;
		}
		
		public void SetupLesson(string roomId, LessonData lessonData)
		{
			_roomId = roomId;
			_lessonData = lessonData.Clone();

			if (_lessonData.EnableRandomQuestionsPool)
			{
				int questionsCount = _lessonData.RandomQuestionsPoolCount <= 0 || _lessonData.RandomQuestionsPoolCount > _lessonData.QuestionsData.Count
					? _lessonData.QuestionsData.Count
					: _lessonData.RandomQuestionsPoolCount;

				List<QuestionData> newQuestionsPool = new();
				_lessonData.QuestionsData.Shuffle();

				int index = 0;
				foreach (var questionData in _lessonData.QuestionsData)
				{
					if (index >= questionsCount)
					{
						break;
					}
					newQuestionsPool.Add(questionData);
					index++;
				}

				_lessonData.QuestionsData = newQuestionsPool;
			}
			
			_answersContainer.Clear();
			for (int i = 1; i <= _lessonData.QuestionsData.Count; i++)
			{
				_answersContainer.Add(i, null);
			}

			_lessonPanel.QuizPanel.InitializeQuestionsButtons(_lessonData.QuestionsData.Count, SetupQuestion);
		}

		public void StartLesson()
		{
			_lessonPanel.OpenQuizPanel(_lessonData.LessonTime);
			SetupQuestion();
		}

		private void EndLesson(int spentTime)
		{
			int earnedPoints = CalculatePoints();
			EndLessonEvent?.Invoke(new GlobalResultData()
			{
				RoomId = _roomId,
				LessonId = _lessonData.Id,
				EarnedPoints = earnedPoints,
				SpentTime = spentTime
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
				EndLesson(_lessonData.LessonTime);
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