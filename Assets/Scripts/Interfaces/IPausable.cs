namespace LearningAppVR
{
	public interface IPausable
	{
		bool IsPaused { get; }

		void SetPause(bool pause)
		{
			if (IsPaused && !pause)
			{
				Unpause();
			}
			else if (!IsPaused && pause)
			{
				Pause();
			}
		}
		void Pause();
		void Unpause();
	}
}