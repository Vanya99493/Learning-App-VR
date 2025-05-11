using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class ResultsPanel : LessonSubPanel
	{
		public event Action RestartEvent;
		
		[SerializeField]
		private TMP_Text _earnedPointsText;

		[SerializeField]
		private Button _menuButton;

		[SerializeField]
		private Button _restartButton;

		public override void Initialize(ILessonPanel lessonPanel)
		{
			base.Initialize(lessonPanel);
			
			_menuButton.onClick.AddListener(_lessonPanel.CloseLessonPanel);
			_restartButton.onClick.AddListener(OnRestartButtonClick);
		}

		public void Open(int earnedPoint, int maxPoints = -1)
		{
			_earnedPointsText.text = earnedPoint + (maxPoints > -1 ? $"/{maxPoints}" : "");
			base.Open();
		}

		private void OnRestartButtonClick()
		{
			RestartEvent?.Invoke();
		}
	}
}