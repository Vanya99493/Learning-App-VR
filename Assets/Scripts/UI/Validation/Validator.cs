using System.Collections.Generic;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class Validator : MonoBehaviour
	{
		[SerializeField]
		private List<ValidatorElement> _validatorElements = new();

		public bool Validate()
		{
			bool isValid = true;
			
			foreach (var validatorElement in _validatorElements)
			{
				if (!validatorElement.Validate())
				{
					isValid = false;
				}
			}

			return isValid;
		}
	}
}