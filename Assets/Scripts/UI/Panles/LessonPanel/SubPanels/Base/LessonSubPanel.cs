using UnityEngine;

namespace LearningAppVR.UI
{
	public class LessonSubPanel : MonoBehaviour
	{
		protected ILessonPanel _lessonPanel;
		
		public virtual void Initialize(ILessonPanel lessonPanel)
		{
			_lessonPanel = lessonPanel;
			Close();
		}
		
		public virtual void SetActive(bool isActive)
		{
			if (isActive)
			{
				Open();
			}
			else
			{
				Close();
			}
		}
		
		public virtual void Open()
		{
			gameObject.SetActive(true);
		}

		public virtual void Close()
		{
			gameObject.SetActive(false);
		}
	}
}