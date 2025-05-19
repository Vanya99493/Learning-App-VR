using TMPro;

namespace LearningAppVR
{
	public static class TMP_InputFieldExtensions
	{
		public static void OnKeyboardInput(this TMP_InputField inputField, KeyboardCode keyboardCode, string inputValue)
		{
			switch (keyboardCode)
			{
				case KeyboardCode.Backspace:
					if (inputField.text.Length > 0)
					{
						inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
					}
					break;
				case KeyboardCode.Symbol:
					inputField.text += inputValue;
					break;
			}
		}
	}
}