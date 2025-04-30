using System;

namespace LearningAppVR
{
	public interface IDestroyable
	{
		event Action<IDestroyable> DestroyEvent;

		void Destroy();
	}
}