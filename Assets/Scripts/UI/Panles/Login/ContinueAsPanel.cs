using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class ContinueAsPanel : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text _continueAsText;

		[SerializeField]
		private Button _continueButton;

		[SerializeField]
		private Button _reloginButton;

		public void Initialize(Action onContinueButtonClickEvent, Action onReLoginButtonClickEvent)
		{
			_continueButton.onClick.AddListener(() => onContinueButtonClickEvent?.Invoke());
			_reloginButton.onClick.AddListener(() => onReLoginButtonClickEvent?.Invoke());
		}
		
		public void Activate(string username)
		{
			_continueAsText.text = $"Continue as\n\"{username}\"";
			gameObject.SetActive(true);
		}

		public void Deactivate()
		{
			gameObject.SetActive(false);
		}

		public void DisableButtons()
		{
			_continueButton.interactable = false;
			_reloginButton.interactable = false;
		}

		public void EnableButtons()
		{
			_continueButton.interactable = true;
			_reloginButton.interactable = true;
		}
	}
}