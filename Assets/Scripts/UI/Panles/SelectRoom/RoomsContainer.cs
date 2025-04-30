using System;
using System.Collections.Generic;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class RoomsContainer : MonoBehaviour
	{
		public event Action<RoomData> SelectRoomEvent;

		[SerializeField]
		private RoomElement _roomElementPrefab;

		[SerializeField]
		private Transform _roomElementsParent;

		[SerializeField]
		private GameObject _emptyTitle;
		
		private List<RoomElement> _spawnedRoomButtons = new();
		
		public void ClearRoomsContainer()
		{
			foreach (var spawnedRoomButton in _spawnedRoomButtons)
			{
				spawnedRoomButton.Destroy();
			}
			_spawnedRoomButtons.Clear();
			
			_emptyTitle.gameObject.SetActive(true);
		}

		public void InitializeRooms(List<RoomData> rooms)
		{
			if (rooms.Count > 0)
			{
				_emptyTitle.gameObject.SetActive(false);
			}
			
			foreach (var roomData in rooms)
			{
				var room = InstantiateRoom(roomData);
				_spawnedRoomButtons.Add(room);
			}
		}

		private RoomElement InstantiateRoom(RoomData roomData)
		{
			var roomElement = Instantiate(_roomElementPrefab, _roomElementsParent);
			roomElement.DestroyEvent += DestroyRoomElement;
			roomElement.Initialize(roomData, OnSelectRoom);
			return roomElement;
		}

		private void OnSelectRoom(RoomData roomData)
		{
			SelectRoomEvent?.Invoke(roomData);
		}

		private void DestroyRoomElement(IDestroyable roomElement)
		{
			Destroy(((RoomElement)roomElement).gameObject);
		}
	}
}