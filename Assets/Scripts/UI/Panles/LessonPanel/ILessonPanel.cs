namespace LearningAppVR.UI
{
	public interface ILessonPanel
	{
		public IUIManager UIManager { get; }

		void CloseLessonPanel();

		void OpenPreparationPanel();
		void OpenPreparationPanel(string lessonName);
		void OpenCountDownPanel();
		void OpenQuizPanel(int timeInSeconds);
		void OpenResultsPanel(int earnedPoint, int maxPoints);

		void CloseCurrentPanel();
	}
}