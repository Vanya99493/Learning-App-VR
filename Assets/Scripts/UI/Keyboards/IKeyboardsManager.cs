using System;

namespace LearningAppVR.UI
{
	public interface IKeyboardsManager
	{
		void ActivateKeyboard(KeyboardType keyboardType, Action<KeyboardCode, string> onKeyButtonCLick);
		void DeactivateKeyboard();
	}
}