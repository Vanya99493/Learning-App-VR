using LearningAppVR.UI;
using UnityEngine;

namespace LearningAppVR
{
	public class LessonStarter : MonoBehaviour
	{
		[SerializeField]
		private LessonController _lessonController;

		public void Initialize(LessonPanel lessonPanel)
		{
			_lessonController.Initialize(lessonPanel.QuizPanel);
		}

		public void PrepareLesson(LessonData lessonData)
		{
			//TODO: start lesson
		}
	}
}