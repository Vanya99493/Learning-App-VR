using System;
using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class TeacherToolsContainer : MonoBehaviour
	{
		[SerializeField]
		private Button _editButton;

		[SerializeField]
		private Button _statisticsButton;

		public void Initialize(Action onEditButtonClick, Action onStatisticsButtonClick)
		{
			_editButton.onClick.AddListener(() => onEditButtonClick?.Invoke());
			_statisticsButton.onClick.AddListener(() => onStatisticsButtonClick?.Invoke());
		}
	}
}