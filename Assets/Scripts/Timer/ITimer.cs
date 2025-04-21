using System;

namespace LearningAppVR
{
	public interface ITimer
	{
		event Action TimerUpdateEvent;
		event Action TimerEndEvent;
		
		float Time { get; }

		void Start(float timerLimit = -1);
		void Stop();

	}
}