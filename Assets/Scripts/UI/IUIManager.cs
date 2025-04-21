namespace LearningAppVR.UI
{
	public interface IUIManager
	{
		void ActivatePopup(PopupData popupData);
		void DeactivatePopup();
		
		void OpenMainMenuPanel();
		void OpenSettingsPanel();
	}
}