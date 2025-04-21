using LearningAppVR.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class ProfilePanel : BasePanel
	{
		[SerializeField]
		private PlayerInfo _playerInfo;
		
		[Space(10)]
		[Header("Components")]
		[SerializeField]
		private TMP_Text _usernameText;

		[SerializeField]
		private TMP_Text _userRoleText;

		[SerializeField]
		private Button _closeButton;

		[SerializeField]
		private RequestRoleContainer _requestRoleContainer;

		public override void Initialize(IUIManager uiManager)
		{
			base.Initialize(uiManager);
			
			//_closeButton.onClick.AddListener(_uiManager.CloseCurrentPanel);
			_closeButton.onClick.AddListener(_uiManager.OpenMainMenuPanel);
			_playerInfo.SubscribeOnFinishInitialization(UpdateAfterUserInfoInitialization);
		}

		private void UpdateAfterUserInfoInitialization()
		{
			_usernameText.text = _playerInfo.UserName;
			_userRoleText.text = _playerInfo.UserRole.ToString();
			
			if (_playerInfo.UserRole == UserRole.Teacher)
			{
				_requestRoleContainer.gameObject.SetActive(false);
			}
			else
			{
				_requestRoleContainer.Initialize(OnSendRequestButtonClick);
			}
		}

		private void OnSendRequestButtonClick()
		{
			_playerInfo.SendTeacherRoleRequest();
		}
	}
}