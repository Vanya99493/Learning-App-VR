using System;
using System.Collections.Generic;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class EditRoomsContainer : ButtonsWrappersContainer<RoomData>
	{
		[SerializeField]
		private AdditionalAddButtonController _addButtonController;

		public void Initialize(Action onAddButtonClick)
		{
			_addButtonController.Initialize(onAddButtonClick);
		}
		
		public override void FillContainer(List<RoomData> dataCollection)
		{
			base.FillContainer(dataCollection);
			_addButtonController.UpdateAddButtonPosition(_parent);
		}
	}
}