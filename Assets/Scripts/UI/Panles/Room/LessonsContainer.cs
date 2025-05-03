namespace LearningAppVR.UI
{
	public class LessonsContainer : ButtonsWrappersContainer<LessonData>
	{
		protected override ButtonWrapper InstantiateButtonWrapper(LessonData dataCollectionElement)
		{
			var buttonWrapper = base.InstantiateButtonWrapper(dataCollectionElement);
			buttonWrapper.Text.text = dataCollectionElement.LessonName;
			return buttonWrapper;
		}
	}
}