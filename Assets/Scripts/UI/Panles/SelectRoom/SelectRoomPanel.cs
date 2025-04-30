using System;
using System.Linq;
using LearningAppVR.Room;
using PlayFab;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class SelectRoomPanel : BasePanel
	{
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

		private PlayFabServerRequester _playFabServerRequester;
		private RoomsCollection _roomsCollection;
		private RoomStatus _roomsListStatus;
		private RoomFilters _roomFilters;

		public override void Initialize(IUIManager uiManager)
		{
			base.Initialize(uiManager);

			_playFabServerRequester = new PlayFabServerRequester();
			
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
			base.Open();
			
			if (needToUpdateRoomsCollection)
			{
				_roomsContainer.ClearRoomsContainer();
				SwitchStatus(RoomStatus.Global);
				ObtainRooms(() => FillRoomsContainer(_roomFilters));
			}
			else
			{
				FillRoomsContainer(_roomFilters);
			}
		}

		private async void ObtainRooms(Action callback)
		{
			_roomsCollection = await _playFabServerRequester.GetAllRoomsData();
			callback?.Invoke();
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

			_roomsContainer.InitializeRooms(filteredRoomsCollection);
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