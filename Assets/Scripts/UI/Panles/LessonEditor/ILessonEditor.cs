using System;

namespace LearningAppVR.UI
{
	public interface ILessonEditor
	{
		public event Action<LessonData> SaveLessonEvent;
		public event Action<LessonData> DeleteLessonEvent;
	}
}