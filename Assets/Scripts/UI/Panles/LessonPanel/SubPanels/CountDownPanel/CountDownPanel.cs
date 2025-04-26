using TMPro;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class CountDownPanel : LessonSubPanel
	{
		[SerializeField]
		private TMP_Text _countdownText;

		private Timer _timer;

		public override void Initialize(ILessonPanel lessonPanel)
		{
			base.Initialize(lessonPanel);

			_timer = new Timer();
			_timer.TimerUpdateEvent += UpdateCountdownText;
			_timer.TimerEndEvent += _lessonPanel.OnEndCountdown;
		}

		private void OnDestroy()
		{
			_timer.TimerUpdateEvent -= UpdateCountdownText;
			_timer.TimerEndEvent -= _lessonPanel.OnEndCountdown;
		}

		public override void Open()
		{
			_timer.Start(3f);
			base.Open();
		}

		private void UpdateCountdownText()
		{
			var timeLeft = Mathf.CeilToInt(_timer.Time);
			_countdownText.text = timeLeft + " second" + (timeLeft != 1 ? "s" : "");
		}
	}
}