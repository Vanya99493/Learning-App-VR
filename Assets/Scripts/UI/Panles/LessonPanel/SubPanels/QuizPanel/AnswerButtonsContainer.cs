using System.Collections.Generic;

namespace LearningAppVR.UI
{
	public class AnswerButtonsContainer : ButtonsWrappersContainer<string>
	{
		public void FillContainer(List<string> dataCollection, string previousAnswer = null)
		{
			base.FillContainer(dataCollection);

			if (previousAnswer is not null)
			{
				foreach (var buttonWrapper in _elements)
				{
					if (buttonWrapper.Text.text == previousAnswer)
					{
						buttonWrapper.Button.onClick.Invoke();
					}
				}
			}
		}

		public override ButtonWrapper AddElement(string dataElement)
		{
			var element = base.AddElement(dataElement);
			element.Text.text = dataElement;
			return element;
		}
	}
}