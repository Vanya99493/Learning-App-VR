namespace LearningAppVR.UI
{
	public interface IPCUIManager
	{
		void ActivatePopup(PopupData popupData);
		void DeactivatePopup();

		void CloseCurrentPanel();
	}
}