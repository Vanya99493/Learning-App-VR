using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LearningAppVR.UI
{
	public abstract class ValidatorElement : MonoBehaviour
	{
		[SerializeField]
		protected List<ValidationType> _validationTypes;

		[Space(20)]
		[SerializeField]
		protected UnityEvent _passValidationUnityEvent;

		[SerializeField]
		protected UnityEvent _deniedValidationmUnityEvent;

		public bool Validate()
		{
			if (CheckValidation())
			{
				_passValidationUnityEvent?.Invoke();
				return true;
			}
			else
			{
				_deniedValidationmUnityEvent?.Invoke();
				return false;
			}
		}

		protected abstract bool CheckValidation();
	}
}