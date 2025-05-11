using System;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class EnterCredentialsPanel : MonoBehaviour
	{
		[SerializeField]
		private TMP_InputField _usernameInputFuild;

		[SerializeField]
		private TMP_InputField _passwordInputFuild;

		[SerializeField]
		private Button _loginButton;

		[SerializeField]
		private Button _returnButton;

		public void Initialize(Action<string, string> onLoginButtonClickEvent, Action onReturnButtonClickEvent)
		{
			_loginButton.onClick.AddListener(() => onLoginButtonClickEvent?.Invoke(_usernameInputFuild.text, _passwordInputFuild.text));
			_returnButton.onClick.AddListener(() => onReturnButtonClickEvent?.Invoke());
		}

		[Button]
		public void Login()
		{
			_loginButton.onClick.Invoke();
		}

		public void Activate(bool hasRememberedInfo)
		{
			_returnButton.gameObject.SetActive(hasRememberedInfo);
			
			gameObject.SetActive(true);
		}

		public void Deactivate()
		{
			gameObject.SetActive(false);
		}

		public void DisableButtons()
		{
			_loginButton.interactable = false;
			_returnButton.interactable = false;
		}

		public void EnableButtons()
		{
			_loginButton.interactable = true;
			_returnButton.interactable = true;
		}
	}
}