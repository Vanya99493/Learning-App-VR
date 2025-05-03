using System;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class AdditionalAddButtonController : MonoBehaviour
	{
		[SerializeField]
		private ButtonWrapper _addButtonWrapper;

		[SerializeField]
		private GridPositionReset _gridPositionReset;

		public void Initialize(Action onAddButtonClick)
		{
			_addButtonWrapper.Button.onClick.AddListener(() => onAddButtonClick?.Invoke());
		}

		public void UpdateAddButtonPosition(Transform containerParent)
		{
			_gridPositionReset.ResetPosition(_addButtonWrapper.gameObject, containerParent);
		}

		private void OnDestroy()
		{
			_addButtonWrapper.Button.onClick.RemoveAllListeners();
		}
	}
}