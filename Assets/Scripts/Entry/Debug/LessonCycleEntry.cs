using System.Collections.Generic;
using LearningAppVR.DataAssets;
using LearningAppVR.UI;
using NaughtyAttributes;
using UnityEngine;

namespace LearningAppVR
{
	public class LessonCycleEntry : MonoBehaviour
	{
		[SerializeField]
		private LessonController _lessonController;

		[SerializeField]
		private LessonPanel _lessonPanel;

		[SerializeField]
		private List<LessonDataAsset> _lessonDataAssets;

		private void Awake()
		{
			Initialize();
			_lessonPanel.gameObject.SetActive(false);
		}

		private void Initialize() 
		{
			_lessonController.Initialize(_lessonPanel);
			_lessonController.EndLessonEvent += OnEndLessonEventHandler;
		}

		private void OnEndLessonEventHandler(int correctAnswers)
		{
			Debug.Log($"Correct answers: {correctAnswers}");
			_lessonPanel.gameObject.SetActive(false);
		}

		[Button]
		private void StartLesson()
		{
			_lessonController.StartLesson(_lessonDataAssets[Random.Range(0, _lessonDataAssets.Count)]);
			_lessonPanel.gameObject.SetActive(true);
		}
	}
}