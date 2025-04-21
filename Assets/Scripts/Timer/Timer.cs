using System;
using DG.Tweening;

namespace LearningAppVR
{
	public class Timer : IPausable
	{
		public event Action TimerUpdateEvent;
		public event Action TimerEndEvent;

		public float Time { get; private set; }
		public bool IsPaused { get; private set; }

		private Tween _timerTween;

		public void Start(float timerLimit = -1)
		{
			Time = 0f;
			IsPaused = false;
			timerLimit = timerLimit <= 0 ? Int32.MaxValue : timerLimit;
			
			_timerTween?.Kill();
			_timerTween = DOTween.To(() => Time, x => Time = x, timerLimit, timerLimit)
				.SetEase(Ease.Linear)
				.OnUpdate(() => TimerUpdateEvent?.Invoke())
				.OnComplete(() => TimerEndEvent?.Invoke());
		}

		public void Stop()
		{
			Time = 0;
			_timerTween?.Kill();
			_timerTween = null;
		}

		public void Pause()
		{
			_timerTween?.Pause();
			IsPaused = true;
		}

		public void Unpause()
		{
			_timerTween?.Play();
			IsPaused = false;
		}

	}
}