using TMPro;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class InputAnswerContainer : MonoBehaviour
	{
		[SerializeField]
		private TMP_InputField _correctAnswerInputField;

		public void SetupAnswer(string correctAnswer)
		{
			_correctAnswerInputField.text = correctAnswer;
		}

		public string GetCorrectAnswer()
		{
			return _correctAnswerInputField.text;
		}
	}
}