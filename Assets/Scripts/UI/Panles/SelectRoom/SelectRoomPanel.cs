using System;
using System.Linq;
using LearningAppVR.Player;
using LearningAppVR.Room;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class SelectRoomPanel : BasePanel
	{
		[SerializeField]
		private DataProvider _dataProvider;
		
		[Space(10)]
		[SerializeField]
		private Button _allRoomsSwitchButton;

		[SerializeField]
		private Button _subscribedRoomsSwitchButton;

		[SerializeField]
		private TMP_Text _titleText;
		
		[Space(10)]
		[SerializeField]
		private Button _returnButton;
		
		[Space(10)]
		[SerializeField]
		private RoomsContainer _roomsContainer;

		[SerializeField]
		private FiltersContainer _filtersContainer;

		private RoomsCollection _roomsCollection;
		private RoomStatus _roomsListStatus;
		private RoomFilters _roomFilters;

		public override void Initialize(IUIManager uiManager)
		{
			base.Initialize(uiManager);
			
			_roomFilters = new()
			{
				SubjectType = SubjectType.All,
				RoomName = "",
				AuthorName = "",
				Access = Access.Public
			};
			
			_filtersContainer.Initialize();
			_filtersContainer.ChangeFilterEvent += OnChangeFiltersEventHandler;

			_roomsContainer.SelectRoomEvent += OnSelectRoomEventHandler;
			
			_allRoomsSwitchButton.onClick.AddListener(() => SwitchStatus(RoomStatus.Global));
			_subscribedRoomsSwitchButton.onClick.AddListener(() => SwitchStatus(RoomStatus.Subscribed));
			
			_returnButton.onClick.AddListener(() => _uiManager.OpenMainMenuPanel());
		}

		public void Open(bool needToUpdateRoomsCollection)
		{
			if (needToUpdateRoomsCollection)
			{
				_roomsContainer.ClearRoomsContainer();
				ObtainRooms();
			}
			else
			{
				FillRoomsContainer(_roomFilters);
			}
			
			base.Open();
		}

		private async void ObtainRooms()
		{
			_roomsCollection = await _dataProvider.GetRoomsCollection();
			SwitchStatus(RoomStatus.Global);
		}

		private void FillRoomsContainer(RoomFilters roomFilters)
		{
			_roomsContainer.ClearRoomsContainer();

			var filteredRoomsCollection = _roomsCollection.Rooms
				.Where(roomData =>
					roomData.SubjectType == roomFilters.SubjectType &&
					roomData.RoomName.StartsWith(roomFilters.RoomName, StringComparison.OrdinalIgnoreCase) &&
					roomData.Author.StartsWith(roomFilters.AuthorName, StringComparison.OrdinalIgnoreCase) &&
					roomData.Access == roomFilters.Access)
				.ToList();

			_roomsContainer.FillContainer(filteredRoomsCollection);
		}

		private void SwitchStatus(RoomStatus roomStatus)
		{
			_roomsListStatus = roomStatus;
			_titleText.text = _roomsListStatus.ToString();
			FillRoomsContainer(_roomFilters);
		}

		private void OnChangeFiltersEventHandler(RoomFilters roomFilters)
		{
			_roomFilters = roomFilters;
		}

		private void OnSelectRoomEventHandler(RoomData roomData)
		{
			_uiManager.OpenRoomPanel(roomData);
		}
	}
}