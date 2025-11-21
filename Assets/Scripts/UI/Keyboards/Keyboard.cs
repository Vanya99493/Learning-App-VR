using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class Keyboard : MonoBehaviour
	{
		[SerializeField]
		protected Button _closeButton;
		
		[SerializeField]
		protected List<ButtonWrapper> _buttonWrappers = new();

		protected IKeyboardsManager _keyboardsManager;
		
		protected Action<KeyboardCode, string> _buttonClickEvent;
		
		public virtual void Initialize(IKeyboardsManager keyboardsManager)
		{
			_keyboardsManager = keyboardsManager;
			
			foreach (var buttonWrapper in _buttonWrappers)
			{
				buttonWrapper.Button.onClick.AddListener(() => OnButtonClick(buttonWrapper));
			}
			_closeButton.onClick.AddListener(_keyboardsManager.DeactivateKeyboard);
		}
		
		public void Activate(Action<KeyboardCode, string> onButtonClick)
		{
			_buttonClickEvent = onButtonClick;
			gameObject.SetActive(true);
		}

		public void Deactivate()
		{
			_buttonClickEvent = null;
			gameObject.SetActive(false);
		}

		private void OnButtonClick(ButtonWrapper sender)
		{
			KeyboardCode code;
			if (sender.Text.text == "[A]-[a]")
			{
				code = KeyboardCode.ChangeCase;
			}
			else if (sender.Text.text == "<-")
			{
				code = KeyboardCode.Backspace;
			}
			else
			{
				code = KeyboardCode.Symbol;
			}
			
			_buttonClickEvent?.Invoke(code, sender.Text.text);
		}
	}
}