namespace LearningAppVR.UI
{
	public interface ILessonPanel
	{
		void CloseLessonPanel();

		void OpenPreparationPanel();
		void OpenPreparationPanel(string lessonName);
		void OpenCountDownPanel();
		void OpenQuizPanel();
		void OpenResultsPanel(int earnedPoint, int maxPoints);

		void OnEndCountdown();

		void CloseCurrentPanel();
	}
}