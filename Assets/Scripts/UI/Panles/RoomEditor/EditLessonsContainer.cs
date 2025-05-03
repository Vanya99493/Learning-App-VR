using System;
using System.Collections.Generic;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class EditLessonsContainer : LessonsContainer
	{
		[SerializeField]
		private AdditionalAddButtonController _addButtonController;

		public void Initialize(Action onAddButtonClick)
		{
			_addButtonController.Initialize(onAddButtonClick);
		}
		
		public override void FillContainer(List<LessonData> lessonsCollection)
		{
			base.FillContainer(lessonsCollection);
			_addButtonController.UpdateAddButtonPosition(_parent);
		}
	}
}