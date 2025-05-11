using LearningAppVR.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class ProfilePanel : BasePanel
	{
		[SerializeField]
		private DataProvider _dataProvider;
		
		[Space(10)]
		[Header("Components")]
		[SerializeField]
		private TMP_Text _usernameText;

		[SerializeField]
		private TMP_Text _userRoleText;

		[SerializeField]
		private Button _closeButton;

		[Space(10)]
		[SerializeField]
		private RequestRoleContainer _requestRoleContainer;

		[SerializeField]
		private TeacherToolsContainer _teacherToolsContainer;

		public override void Initialize(IUIManager uiManager)
		{
			base.Initialize(uiManager);
			
			_teacherToolsContainer.Initialize(() => _uiManager.OpenOwnedRoomsPanel(true));
			
			_closeButton.onClick.AddListener(_uiManager.OpenMainMenuPanel);
			_dataProvider.PlayerInfo.SubscribeOnFinishInitialization(UpdateAfterUserInfoInitialization);
		}

		private void UpdateAfterUserInfoInitialization()
		{
			_usernameText.text = _dataProvider.PlayerInfo.UserName;
			_userRoleText.text = _dataProvider.PlayerInfo.UserRole.ToString();
			
			if (_dataProvider.PlayerInfo.UserRole == UserRole.Teacher)
			{
				_requestRoleContainer.gameObject.SetActive(false);
				_teacherToolsContainer.gameObject.SetActive(_dataProvider.PlayerInfo.UserRole == UserRole.Teacher && AppSettings.DeviceType == DeviceType.PC);
			}
			else
			{
				_requestRoleContainer.Initialize(OnSendRequestButtonClick);
			}
		}

		private void OnSendRequestButtonClick()
		{
			_dataProvider.PlayerInfo.SendTeacherRoleRequest();
		}
	}
}