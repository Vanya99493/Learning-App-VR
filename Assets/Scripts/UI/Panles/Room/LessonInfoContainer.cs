using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class LessonInfoContainer : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text _lessonName;

		[SerializeField]
		private TMP_Text _difficultyText;

		[SerializeField]
		private TMP_Text _timeText;

		[SerializeField]
		private TMP_Text _questionsText;

		[SerializeField]
		private Button _startLessonButton;

		public void Open(LessonData lessonData, Action<LessonData> onStartButtonClick)
		{
			_lessonName.text = lessonData.LessonName;
			_difficultyText.text = $"Difficulty: {lessonData.Difficulty + 1}";
			_timeText.text = $"Time: {lessonData.LessonTime.ToTimerString()}";
			_questionsText.text = $"Questions: {lessonData.QuestionsData.Count}";
			
			_startLessonButton.onClick.RemoveAllListeners();
			_startLessonButton.onClick.AddListener(() => onStartButtonClick?.Invoke(lessonData));
			
			gameObject.SetActive(true);
		}

		public void Close()
		{
			gameObject.SetActive(false);
		}
	}
}