using System;
using System.Collections.Generic;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class EditQuestionsContainer : ButtonsWrappersContainer<QuestionData>
	{
		[SerializeField]
		private AdditionalButtonController _additionalButtonController;

		public void Initialize(Action onAddButtonClick)
		{
			_additionalButtonController.Initialize(onAddButtonClick);
		}
		
		public override void FillContainer(List<QuestionData> questionsCollection)
		{
			base.FillContainer(questionsCollection);
			_additionalButtonController.UpdateAddButtonPosition(_parent);
		}

		public override void AddElement(QuestionData dataElement)
		{
			base.AddElement(dataElement);
			_additionalButtonController.UpdateAddButtonPosition(_parent);
		}

		protected override ButtonWrapper InstantiateButtonWrapper(QuestionData dataCollectionElement)
		{
			var buttonWrapper = base.InstantiateButtonWrapper(dataCollectionElement);
			buttonWrapper.Text.text = (_buttonsWrappers.Count + 1).ToString();
			return buttonWrapper;
		}
	}
}