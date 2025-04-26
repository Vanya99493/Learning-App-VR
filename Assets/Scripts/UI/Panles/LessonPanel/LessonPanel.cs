using System;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class LessonPanel : BasePanel, ILessonPanel
	{
		public event Action EndCountdownEvent;
		
		[SerializeField]
		private PreparationPanel _preparationPanel;

		[SerializeField]
		private CountDownPanel _countDownPanel;

		[SerializeField]
		private QuizPanel _quizPanel;

		[SerializeField]
		private ResultsPanel _resultsPanel;

		private LessonSubPanel _currentActivePanel;

		public QuizPanel QuizPanel => _quizPanel;

		public override void Initialize(IUIManager uiManager)
		{
			base.Initialize(uiManager);
			
			_preparationPanel.Initialize(this);
			_countDownPanel.Initialize(this);
			_quizPanel.Initialize(this);
			_resultsPanel.Initialize(this);
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
			_preparationPanel.Open(lessonName);
		}

		public void OpenCountDownPanel()
		{
			OpenNewPanel(_countDownPanel);
		}

		public void OpenQuizPanel()
		{
			OpenNewPanel(_quizPanel);
		}

		public void OpenResultsPanel(int earnedPoint, int maxPoints = -1)
		{
			CloseCurrentPanel();
			_resultsPanel.Open(earnedPoint, maxPoints);
		}

		public void OnEndCountdown()
		{
			EndCountdownEvent?.Invoke();
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
	}
}