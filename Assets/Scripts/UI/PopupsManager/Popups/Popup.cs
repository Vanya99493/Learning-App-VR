using TMPro;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class Popup : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text _infoText;

		[SerializeField]
		private ButtonWrapper _firstButton;

		[SerializeField]
		private ButtonWrapper _secondButton;

		public void Activate(PopupData popupData)
		{
			_infoText.text = popupData.PopupTextInfo;

			_firstButton.Text.text = popupData.FirstButtonData.ButtonText;
			_firstButton.Button.onClick.AddListener(() => popupData.FirstButtonData.ButtonCallback?.Invoke());
			_firstButton.gameObject.SetActive(true);

			if (popupData.SecondButtonData is not null)
			{
				_secondButton.Text.text = popupData.SecondButtonData.ButtonText;
				_secondButton.Button.onClick.AddListener(() => popupData.SecondButtonData.ButtonCallback?.Invoke());
				_secondButton.gameObject.SetActive(true);
			}
			else
			{
				_secondButton.gameObject.SetActive(false);
			}

			gameObject.SetActive(true);
		}

		public void Deactivate()
		{
			_firstButton.Button.onClick.RemoveAllListeners();
			_secondButton.Button.onClick.RemoveAllListeners();
		}
	}
}