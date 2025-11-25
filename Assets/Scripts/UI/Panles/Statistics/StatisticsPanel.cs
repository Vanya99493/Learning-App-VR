using System.Collections.Generic;
using LearningAppVR.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class StatisticsPanel : BasePanel
	{
		[SerializeField]
		private Button _closeButton;
		
		[SerializeField]
		private TMP_Dropdown _roomsDropdown;

		[SerializeField]
		private TMP_Dropdown _lessonsDropdown;

		[SerializeField]
		private Transform _elementsContainer;

		[SerializeField]
		private StatisticsElement _statisticsElementPrefab;

		[SerializeField]
		private DataProvider _dataProvider;

		private List<StatisticsElement> _statisticsElements = new();
		private RoomsCollection _roomsCollection;
		private RoomData _selectedRoom;
		
		public override void Initialize(IUIManager uiManager)
		{
			base.Initialize(uiManager);

			_closeButton.onClick.AddListener(() => _uiManager.OpenProfilePanel());
			_roomsDropdown.onValueChanged.AddListener(OnRoomsDropdownValueChanged);
			_lessonsDropdown.onValueChanged.AddListener(OnLessonsDropdownValueChanged);
		}
		
		public override void Open()
		{
			base.Open();

			InitRooms();
		}

		private async void InitRooms()
		{
			_roomsDropdown.options.Clear();
			_lessonsDropdown.options.Clear();
			
			_roomsCollection = await _dataProvider.GetOwnedRoomsCollection();
			List<string> options = new();
			options.Add("");
			foreach (var room in _roomsCollection.Rooms)
			{
				options.Add(room.RoomName);
			}
			_roomsDropdown.AddOptions(options);
		}

		private void OnRoomsDropdownValueChanged(int selectedIndex)
		{
			_lessonsDropdown.value = 0;
			_lessonsDropdown.options.Clear();
			ClearContainer();
			
			if (selectedIndex == 0)
			{
				return;
			}
			selectedIndex--;

			_selectedRoom = _roomsCollection.Rooms[selectedIndex];
			List<string> options = new();
			options.Add("");
			foreach (var room in _selectedRoom.Lessons)
			{
				options.Add(room.LessonName);
			}
			_lessonsDropdown.AddOptions(options);
		}

		private async void OnLessonsDropdownValueChanged(int selectedIndex)
		{
			ClearContainer();

			if (selectedIndex == 0)
			{
				return;
			}
			selectedIndex--;
			
			UserResultsCollectionData results = await _dataProvider.GetUserResultsAsync(_selectedRoom.Lessons[selectedIndex].Id);
			FillContainer(results.Results);
		}

		private void FillContainer(List<UserResultData> userResultsData)
		{
			foreach (var userResultData in userResultsData)
			{
				var newElement = Instantiate(_statisticsElementPrefab, _elementsContainer);
				newElement.Initialize(userResultData.PlayerName, userResultData.EarnedPoints, userResultData.SpentTime);
				_statisticsElements.Add(newElement);
			}
		}

		private void ClearContainer()
		{
			foreach (var element in _statisticsElements)
			{
				Destroy(element.gameObject);
			}
			_statisticsElements.Clear();
		}
	}
}