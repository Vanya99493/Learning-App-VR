using UnityEngine;

namespace LearningAppVR.UI
{
	public abstract class BasePanel : MonoBehaviour
	{
		protected IUIManager _uiManager;
		
		public virtual void Initialize(IUIManager uiManager)
		{
			_uiManager = uiManager;
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