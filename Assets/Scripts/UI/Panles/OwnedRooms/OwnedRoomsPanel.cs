using System;
using LearningAppVR.Player;
using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class OwnedRoomsPanel : BasePanel
	{
		[SerializeField]
		private DataProvider _dataProvider;
		
		[Space(10)]
		[SerializeField]
		private Button _returnButton;

		[SerializeField]
		private EditRoomsContainer _editRoomsContainer;

		private RoomsCollection _roomsCollection;

		public override void Initialize(IUIManager uiManager)
		{
			base.Initialize(uiManager);

			_editRoomsContainer.Initialize(OnAddButtonClick);
			_editRoomsContainer.SelectEvent += OnSelectRoom;
			
			_returnButton.onClick.AddListener(_uiManager.OpenProfilePanel);
		}

		public void Open(bool needToUpdateRoomsCollection)
		{
			if (needToUpdateRoomsCollection)
			{
				ObtainRooms(FillRooms);
			}
			
			base.Open();
		}

		public override void Close()
		{
			base.Close();
			_roomsCollection = null;
		}

		private async void ObtainRooms(Action callback)
		{
			_roomsCollection = await _dataProvider.GetRoomsCollection();
			callback?.Invoke();
		}

		private void FillRooms()
		{
			_editRoomsContainer.FillContainer(_roomsCollection.Rooms);
		}

		private void OnAddButtonClick()
		{
			_uiManager.OpenRoomEditorPanel(new RoomData());
		}

		private void OnSelectRoom(RoomData roomData)
		{
			_uiManager.OpenRoomEditorPanel(roomData);
		}
	}
}