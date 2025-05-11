using System;
using TMPro;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class CountDownPanel : LessonSubPanel
	{
		public event Action EndCountdownEvent;
		
		[SerializeField]
		private TMP_Text _countdownText;

		[SerializeField]
		private int _countdownTimeInSeconds = 3;

		private Timer _timer;

		public override void Initialize(ILessonPanel lessonPanel)
		{
			base.Initialize(lessonPanel);

			_timer = new Timer();
			_timer.TimerUpdateEvent += UpdateCountdownText;
			_timer.TimerEndEvent += OnEndCountdown;
		}

		private void OnDestroy()
		{
			_timer.TimerUpdateEvent -= UpdateCountdownText;
			_timer.TimerEndEvent -= OnEndCountdown;
		}

		public override void Open()
		{
			_timer.Start(_countdownTimeInSeconds);
			base.Open();
		}

		private void UpdateCountdownText()
		{
			var timeLeft = (int)Mathf.Floor(_timer.Time);
			_countdownText.text = _countdownTimeInSeconds - timeLeft + " second" + (timeLeft != 1 ? "s" : "");
		}

		private void OnEndCountdown()
		{
			EndCountdownEvent?.Invoke();
		}
	}
}