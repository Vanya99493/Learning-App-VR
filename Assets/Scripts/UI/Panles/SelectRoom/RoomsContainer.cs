using System;
using System.Collections.Generic;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class RoomsContainer : ElementsContainer<RoomElement, RoomData>
	{
		public event Action<RoomData> SelectRoomEvent;

		[SerializeField]
		private GameObject _emptyTitle;

		public override void FillContainer(List<RoomData> dataCollection)
		{
			base.FillContainer(dataCollection);
			if (dataCollection.Count > 0)
			{
				_emptyTitle.gameObject.SetActive(false);
			}
		}

		public void ClearRoomsContainer()
		{
			ResetElements();
		}
		
		protected override void ResetElements()
		{
			base.ResetElements();
			_emptyTitle.gameObject.SetActive(true);
		}

		protected override RoomElement InstantiateElement(RoomData dataElement)
		{
			var element = base.InstantiateElement(dataElement);
			element.DestroyEvent += DestroyRoomElement;
			element.Initialize(dataElement, OnSelectRoom);
			return element;
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