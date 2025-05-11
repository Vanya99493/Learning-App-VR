namespace LearningAppVR.UI
{
	public interface IUIManager
	{
		ILessonEditor LessonEditor { get; }
		IRoomEditor RoomEditor { get; }
		
		void ActivatePopup(PopupData popupData);
		void DeactivatePopup();
		
		void OpenMainMenuPanel();
		void OpenSettingsPanel();
		void OpenProfilePanel();
		void OpenSelectRoomPanel(bool needToUpdateRoomsCollection);
		void OpenRoomPanel(RoomData roomData);
		void OpenLessonPanel(string roomId, LessonData lessonData);
		void OpenOwnedRoomsPanel(bool needToUpdateRoomsCollection);
		void OpenRoomEditorPanel(RoomData roomData);
		void OpenLessonEditorPanel(LessonData lessonData);
		void OpenLeaderboardPanel(string roomId);

		void CloseCurrentPanel();
	}
}