using System;
using TMPro;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class InputAnswerContainer : MonoBehaviour
	{
		public event Action<string> UpdateCorrectAnswerEvent; 

		[SerializeField]
		private TMP_InputField _correctAnswerInputField;

		public void Initialize()
		{
			_correctAnswerInputField.onValueChanged.AddListener(value => UpdateCorrectAnswerEvent?.Invoke(value));
		}
		
		public void SetupAnswer(string correctAnswer)
		{
			_correctAnswerInputField.text = correctAnswer;
		}
	}
}