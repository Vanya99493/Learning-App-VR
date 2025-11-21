using System;
using TMPro;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class AnswerInputFieldContainer : MonoBehaviour
	{
		[SerializeField]
		private TMP_InputField _answerInputField;

		public void Initialize(ILessonPanel lessonPanel, Action<string> onSelectAnswer)
		{
			_answerInputField.onSelect.AddListener(_ => lessonPanel.ActivateKeyboard(KeyboardType.Full, (code, value) => AddCharacter(_answerInputField, code, value)));
			_answerInputField.onValueChanged.AddListener(value => onSelectAnswer?.Invoke(value));
		}

		public void Setup(string previousAnswer = null)
		{
			_answerInputField.text = previousAnswer ?? "";
		}
		
		private void AddCharacter(TMP_InputField inputField, KeyboardCode keyboardCode, string symbol)
		{
			inputField.OnKeyboardInput(keyboardCode, symbol);
		}
	}
}