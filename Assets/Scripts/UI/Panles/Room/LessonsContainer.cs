namespace LearningAppVR.UI
{
	public class LessonsContainer : ButtonsWrappersContainer<LessonData>
	{
		protected override ButtonWrapper InstantiateElement(LessonData dataCollectionElement)
		{
			var buttonWrapper = base.InstantiateElement(dataCollectionElement);
			buttonWrapper.Text.text = dataCollectionElement.LessonName;
			return buttonWrapper;
		}
	}
}