using System;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class LessonPanel : BasePanel, ILessonPanel
	{
		public event Action<string, LessonData> StartLessonEvent;
		public event Action EndCountdownEvent;
		
		[SerializeField]
		private PreparationPanel _preparationPanel;

		[SerializeField]
		private CountDownPanel _countDownPanel;

		[SerializeField]
		private QuizPanel _quizPanel;

		[SerializeField]
		private ResultsPanel _resultsPanel;

		private LessonData _lessonData;
		private LessonSubPanel _currentActivePanel;

		public IUIManager UIManager => _uiManager;
		public QuizPanel QuizPanel => _quizPanel;
		public ResultsPanel ResultsPanel => _resultsPanel;

		public override void Initialize(IUIManager uiManager)
		{
			base.Initialize(uiManager);
			
			_preparationPanel.Initialize(this);
			_countDownPanel.Initialize(this);
			_quizPanel.Initialize(this);
			_resultsPanel.Initialize(this);

			_countDownPanel.EndCountdownEvent += OnEndCountdownEventHandler;
		}

		public void Open(string roomId, LessonData lessonData)
		{
			base.Open();
			OpenPreparationPanel(lessonData.LessonName);
			StartLessonEvent?.Invoke(roomId, lessonData);
		}

		public void CloseLessonPanel()
		{
			_uiManager.OpenMainMenuPanel();
		}

		public void OpenPreparationPanel()
		{
			OpenNewPanel(_preparationPanel);
		}
		
		public void OpenPreparationPanel(string lessonName)
		{
			CloseCurrentPanel();
			_currentActivePanel = _preparationPanel;
			_preparationPanel.Open(lessonName);
		}

		public void OpenCountDownPanel()
		{
			OpenNewPanel(_countDownPanel);
		}

		public void OpenQuizPanel(int timeInSeconds)
		{
			CloseCurrentPanel();
			_currentActivePanel = _quizPanel;
			_quizPanel.Open(timeInSeconds);
		}

		public void OpenResultsPanel(int earnedPoint, int maxPoints = -1)
		{
			CloseCurrentPanel();
			_currentActivePanel = _resultsPanel;
			_resultsPanel.Open(earnedPoint, maxPoints);
		}

		public void CloseCurrentPanel()
		{
			_currentActivePanel?.Close();
			_currentActivePanel = null;
		}

		private void OpenNewPanel(LessonSubPanel newPanel)
		{
			_currentActivePanel?.Close();
			_currentActivePanel = newPanel;
			_currentActivePanel.Open();
		}

		private void OnEndCountdownEventHandler()
		{
			EndCountdownEvent?.Invoke();
		}
	}
}