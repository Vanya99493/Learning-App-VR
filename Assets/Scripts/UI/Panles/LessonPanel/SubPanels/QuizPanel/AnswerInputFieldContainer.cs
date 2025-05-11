using System;
using TMPro;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class AnswerInputFieldContainer : MonoBehaviour
	{
		[SerializeField]
		private TMP_InputField _answerInputField;

		public void Initialize(Action<string> onSelectAnswer)
		{
			_answerInputField.onValueChanged.AddListener(value => onSelectAnswer?.Invoke(value));
		}

		public void Setup(string previousAnswer = null)
		{
			_answerInputField.text = previousAnswer ?? "";
		}
	}
}