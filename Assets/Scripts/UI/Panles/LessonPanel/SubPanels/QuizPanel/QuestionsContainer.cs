using System.Collections.Generic;

namespace LearningAppVR.UI
{
	public class QuestionsContainer : ButtonsWrappersContainer<int>
	{
		public override void FillContainer(List<int> dataCollection)
		{
			base.FillContainer(dataCollection);
			_elements[0].Button.onClick.Invoke();
		}

		public override ButtonWrapper AddElement(int dataElement)
		{
			var element = base.AddElement(dataElement);
			int buttonNumber = _elements.Count;
			element.Text.text = buttonNumber.ToString();
			return element;
		}
	}
}