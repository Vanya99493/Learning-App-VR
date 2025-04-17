using System;
using JetBrains.Annotations;
using LearningAppVR.DataAssets;
using LearningAppVR.UI;
using UnityEngine;

namespace LearningAppVR
{
	public class LessonController : MonoBehaviour
	{
		[CanBeNull]
		public event Action<int> EndLessonEvent;
		
		private LessonPanel _lessonPanel;
		
		private LessonDataAsset _lessonDataAsset;
		private int _currentQuestion;
		private int _correctAnswers;

		public void Initialize(LessonPanel lessonPanel)
		{
			_lessonPanel = lessonPanel;
			_lessonPanel.SelectAnswerEvent += OnAnswerSelectedEventHandler;
		}

		private void OnDestroy()
		{
			_lessonPanel.SelectAnswerEvent -= OnAnswerSelectedEventHandler;
		}

		public void StartLesson(LessonDataAsset lessonDataAsset)
		{
			_correctAnswers = 0;
			_currentQuestion = 0;
			_lessonDataAsset = lessonDataAsset;
			
			_lessonPanel.Initialize(lessonDataAsset.LessonName);
			SetupNewQuestion();
		}

		private void SetupNewQuestion()
		{
			_lessonPanel.SetupQuestion(_lessonDataAsset.Questions[_currentQuestion]);
		}

		private void OnAnswerSelectedEventHandler(string selectedAnswer)
		{
			if (selectedAnswer == _lessonDataAsset.Questions[_currentQuestion].CorrectAnswer)
			{
				_correctAnswers++;
			}

			_currentQuestion++;

			if (_currentQuestion < _lessonDataAsset.Questions.Count)
			{
				SetupNewQuestion();
			}
			else
			{
				EndLessonEvent?.Invoke(_correctAnswers);
			}
		}
	}
}