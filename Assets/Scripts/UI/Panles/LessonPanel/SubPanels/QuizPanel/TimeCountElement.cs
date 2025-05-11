using System;
using TMPro;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class TimeCountElement : MonoBehaviour
	{
		public event Action TimerEndEvent;
		
		[SerializeField]
		private TMP_Text _timeLeftText;

		private Timer _timer;
		private int _timeInSeconds;
		private int _previousTime;

		public void Initialize()
		{
			_timer = new Timer();
			_timer.TimerUpdateEvent += OnTimerUpdate;
			_timer.TimerEndEvent += OnTimerEnd;
		}

		public void Setup(int timeInSeconds)
		{
			_timeInSeconds = timeInSeconds;
			_timer.Start(_timeInSeconds);
		}

		private void OnTimerUpdate()
		{
			int floorTime = (int)Mathf.Floor(_timer.Time);
			if (_previousTime != floorTime)
			{
				_timeLeftText.text = (_timeInSeconds - Mathf.CeilToInt(_timer.Time)).ToTimerString();
				_previousTime = floorTime;
			}
		}

		private void OnTimerEnd()
		{
			TimerEndEvent?.Invoke();
		}
	}
}