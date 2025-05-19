using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class FullKeyboard : Keyboard
	{
		[SerializeField]
		private Button _changeCaseButton;

		[SerializeField]
		private SymbolCase _symbolCase;
		
		public override void Initialize(IKeyboardsManager keyboardsManager)
		{
			base.Initialize(keyboardsManager);
			_changeCaseButton.onClick.AddListener(OnChangeCaseButtonClick);
			FillKeyboard();
		}

		private void OnChangeCaseButtonClick()
		{
			_symbolCase = _symbolCase == SymbolCase.Lower ? SymbolCase.Upper : SymbolCase.Lower;
			FillKeyboard();
		}

		private void FillKeyboard()
		{
			foreach (var buttonWrapper in _buttonWrappers)
			{
				buttonWrapper.Text.text = buttonWrapper.Text.text.ToCase(_symbolCase);
			}
		}
	}
}