using System;
using System.Collections.Generic;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class EditLessonsContainer : LessonsContainer
	{
		[SerializeField]
		private AdditionalButtonController _additionalButtonController;

		public void Initialize(Action onAddButtonClick)
		{
			_additionalButtonController.Initialize(onAddButtonClick);
		}
		
		public override void FillContainer(List<LessonData> lessonsCollection)
		{
			base.FillContainer(lessonsCollection);
			_additionalButtonController.UpdateAddButtonPosition(_parent);
		}
	}
}