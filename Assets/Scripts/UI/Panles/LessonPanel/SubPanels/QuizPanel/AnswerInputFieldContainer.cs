using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class AnswerInputFieldContainer : MonoBehaviour
	{
		[SerializeField]
		private TMP_InputField _answerInputField;

		[SerializeField]
		private Button _confirmButton;

		public void Initialize(Action<string> onSelectAnswer)
		{
			_confirmButton.onClick.AddListener(() => onSelectAnswer?.Invoke(_answerInputField.text));
		}

		public void Setup(string previousAnswer = null)
		{
			_answerInputField.text = previousAnswer ?? "";
		}
	}
}