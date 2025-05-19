using System;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class KeyboardsManager : MonoBehaviour, IKeyboardsManager
	{
		[SerializeField]
		private FullKeyboard _fullKeyboard;

		[SerializeField]
		private Keyboard _numbersKeyboard;

		private Keyboard _currentOpenKeyboard;

		public void Initialize()
		{
			_fullKeyboard.Initialize(this);
			_numbersKeyboard.Initialize(this);
		}
		
		public void ActivateKeyboard(KeyboardType keyboardType, Action<KeyboardCode, string> onKeyButtonCLick)
		{
			switch (keyboardType)
			{
				case KeyboardType.Full:
					OpenKeyboard(_fullKeyboard, onKeyButtonCLick);
					break;
				case KeyboardType.Numbers:
					OpenKeyboard(_numbersKeyboard, onKeyButtonCLick);
					break;
			}
		}

		public void DeactivateKeyboard()
		{
			_currentOpenKeyboard?.Deactivate();
			_currentOpenKeyboard = null;
		}

		private void OpenKeyboard(Keyboard keyboard, Action<KeyboardCode, string> onKeyButtonCLick)
		{
			DeactivateKeyboard();
			_currentOpenKeyboard = keyboard;
			_currentOpenKeyboard.Activate(onKeyButtonCLick);
		}
	}
}