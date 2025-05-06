using System;
using TMPro;
using UnityEngine;

namespace LearningAppVR.UI.Validators
{
	public class InputFieldValidatorElement : ValidatorElement
	{
		[SerializeField]
		private TMP_InputField _inputField;
		
		protected override bool CheckValidation()
		{
			foreach (var validationType in _validationTypes)
			{
				switch (validationType)
				{
					case ValidationType.IsNumber:
						if (!Int32.TryParse(_inputField.text, out var value))
						{
							return false;
						}
						break;
					case ValidationType.NotEmpty:
						if (_inputField.text.Length <= 0)
						{
							return false;
						} 
						break;
					default:
						Debug.LogError($"[Validator] Input field validator does not support {validationType}");
						return false;
				}
			}

			return true;
		}
	}
}