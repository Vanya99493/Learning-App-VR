using UnityEngine;

namespace LearningAppVR.UI
{
	public class PopupsManager : MonoBehaviour
	{
		[SerializeField]
		private Popup _popup;

		public void ActivatePopup(PopupData popupData)
		{
			_popup.Activate(popupData);
		}

		public void DeactivatePopup()
		{
			_popup.Deactivate();
		}
	}
}