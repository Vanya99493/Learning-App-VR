namespace LearningAppVR.UI
{
	public interface IUIManager
	{
		void ActivatePopup(PopupData popupData);
		void DeactivatePopup();
		
		void OpenMainMenuPanel();
		void OpenSettingsPanel();
		void OpenProfilePanel();
		void OpenSelectRoomPanel(bool needToUpdateRoomsCollection);
		void OpenRoomPanel(RoomData roomData);
		void OpenLessonPanel();

		void CloseCurrentPanel();
	}
}