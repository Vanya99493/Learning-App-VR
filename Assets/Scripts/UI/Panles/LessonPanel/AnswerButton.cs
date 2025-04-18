using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class AnswerButton : MonoBehaviour
	{
		[SerializeField]
		private Button _button;

		[SerializeField]
		private TMP_Text _buttonText;

		public void Initialize(string buttonText, Action onButtonClick)
		{
			_buttonText.text = buttonText;
			_button.onClick.RemoveAllListeners();
			_button.onClick.AddListener(() => onButtonClick?.Invoke());
			gameObject.SetActive(true);
		}

		public void DeInitialize()
		{
			gameObject.SetActive(false);
			_button.onClick.RemoveAllListeners();
		}
	}
}