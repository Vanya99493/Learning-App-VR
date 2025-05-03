using System;
using System.Collections.Generic;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class EditQuestionsContainer : ButtonsWrappersContainer<QuestionData>
	{
		[SerializeField]
		private AdditionalAddButtonController _addButtonController;

		public void Initialize(Action onAddButtonClick)
		{
			_addButtonController.Initialize(onAddButtonClick);
		}
		
		public override void FillContainer(List<QuestionData> questionsCollection)
		{
			base.FillContainer(questionsCollection);
			_addButtonController.UpdateAddButtonPosition(_parent);
		}

		public override void AddElement(QuestionData dataElement)
		{
			base.AddElement(dataElement);
			_addButtonController.UpdateAddButtonPosition(_parent);
		}

		protected override ButtonWrapper InstantiateButtonWrapper(QuestionData dataCollectionElement)
		{
			var buttonWrapper = base.InstantiateButtonWrapper(dataCollectionElement);
			buttonWrapper.Text.text = (_buttonsWrappers.Count + 1).ToString();
			return buttonWrapper;
		}
	}
}