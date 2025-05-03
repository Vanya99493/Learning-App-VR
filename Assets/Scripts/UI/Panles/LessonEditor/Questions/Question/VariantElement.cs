using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class VariantElement : MonoBehaviour
	{
		public event Action<VariantElement> RemoveAction;
		public event Action<VariantElement> ChangeCorrectStateEvent;
		
		[SerializeField]
		private Toggle _isCorrectAnswerToggle;

		[SerializeField]
		private TMP_InputField _variantInputField;
		
		[SerializeField]
		private Button _removeButton;

		public bool IsActive { get; private set; }
		public bool IsCorrect => _isCorrectAnswerToggle.isOn;

		public void Initialize()
		{
			_removeButton.onClick.AddListener(OnRemoveButtonClick);
			_isCorrectAnswerToggle.onValueChanged.AddListener(OnIsCorrectValueChangedEvent);
		}
		
		public void Setup(string answer, bool isCorrect = false)
		{
			SetCorrect(isCorrect);
			_variantInputField.text = answer;
			
			gameObject.SetActive(true);
			IsActive = true;
		}

		public void SetCorrect(bool isCorrect)
		{
			_isCorrectAnswerToggle.onValueChanged.RemoveListener(OnIsCorrectValueChangedEvent);
			_isCorrectAnswerToggle.isOn = isCorrect;
			_isCorrectAnswerToggle.onValueChanged.AddListener(OnIsCorrectValueChangedEvent);
		}

		public Pair<string, bool> GetAnswer()
		{
			return new Pair<string, bool>(_variantInputField.text, _isCorrectAnswerToggle.isOn);
		}

		private void OnRemoveButtonClick()
		{
			IsActive = false;
			RemoveAction?.Invoke(this);
		}

		private void OnIsCorrectValueChangedEvent(bool isOn)
		{
			ChangeCorrectStateEvent?.Invoke(this);
		}
	}
}