using System;
using System.Collections.Generic;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class EditRoomsContainer : ButtonsWrappersContainer<RoomData>
	{
		[SerializeField]
		private AdditionalButtonController _additionalButtonController;

		public void Initialize(Action onAddButtonClick)
		{
			_additionalButtonController.Initialize(onAddButtonClick);
		}
		
		public override void FillContainer(List<RoomData> dataCollection)
		{
			base.FillContainer(dataCollection);
			_additionalButtonController.UpdateAddButtonPosition(_parent);
		}
	}
}