using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class FastLogin : MonoBehaviour
	{
		[SerializeField]
		private TMP_InputField _usernameInputField;

		[SerializeField]
		private TMP_InputField _userpassInputField;
		
		[Space(10)]
		[Header("Login buttons")]
		[SerializeField]
		private Button _loginAs123Button;
		
		[SerializeField]
		private Button _loginAsNikoButton;
		
		[SerializeField]
		private Button _loginAsAbobaButton;

		private void Awake()
		{
			_loginAs123Button.onClick.AddListener(() => Fill("123", "12345678"));
			_loginAsNikoButton.onClick.AddListener(() => Fill("Niko", "12345678"));
			_loginAsAbobaButton.onClick.AddListener(() => Fill("Aboba", "12345678"));
		}

		private void Fill(string username, string userpass)
		{
			_usernameInputField.text = username;
			_userpassInputField.text = userpass;
		}
	}
}