namespace LearningAppVR.UI
{
	public class LeaderboardElement : ElementsContainer<UserElement, UserResult>
	{
		public override UserElement AddElement(UserResult dataElement)
		{
			var element = base.AddElement(dataElement);
			element.Setup(dataElement);
			return element;
		}
	}
}