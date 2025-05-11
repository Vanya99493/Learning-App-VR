using System;
using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class TeacherToolsContainer : MonoBehaviour
	{
		[SerializeField]
		private Button _editButton;

		public void Initialize(Action onEditButtonClick)
		{
			_editButton.onClick.AddListener(() => onEditButtonClick?.Invoke());
		}
	}
}