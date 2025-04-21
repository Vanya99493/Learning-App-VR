using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class RequestRoleContainer : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text _norifyMessage;

		[SerializeField]
		private Button _sendRequestButton;

		public void Initialize(Action onSendRequestButtonCLickEvent)
		{
			_sendRequestButton.onClick.AddListener(() =>
			{
				_norifyMessage.gameObject.SetActive(true);
				onSendRequestButtonCLickEvent?.Invoke();
			});
			
			_norifyMessage.gameObject.SetActive(false);
		}
	}
}