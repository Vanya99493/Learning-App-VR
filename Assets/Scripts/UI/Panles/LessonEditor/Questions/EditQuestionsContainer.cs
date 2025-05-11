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

		public override ButtonWrapper AddElement(QuestionData dataElement)
		{
			var element = base.AddElement(dataElement);
			_additionalButtonController.UpdateAddButtonPosition(_parent);
			return element;
		}

		protected override ButtonWrapper InstantiateElement(QuestionData dataCollectionElement)
		{
			var buttonWrapper = base.InstantiateElement(dataCollectionElement);
			buttonWrapper.Text.text = (_elements.Count + 1).ToString();
			return buttonWrapper;
		}
	}
}