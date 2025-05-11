using LearningAppVR.UI.Validators;
using TMPro;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class InputFieldWrapper : MonoBehaviour
	{
		public TMP_Text Text;
		public TMP_InputField InputField;
		public InputFieldValidatorElement ValidatorElement;

		public int GetInputFieldValueInInt()
		{
			if (ValidatorElement.Validate())
			{
				return InputField.text.ToInt();
			}

			return 0;
		}
	}
}