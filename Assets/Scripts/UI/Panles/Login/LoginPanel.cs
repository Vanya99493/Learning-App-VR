using LearningAppVR.Player;
using NaughtyAttributes;
using PlayFab;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class LoginPanel : BasePanel
	{
		[SerializeField]
		private DataProvider _dataProvider;
		
		[SerializeField]
		private PlayFabLogin _playFabLogin;

		[Space(10)]
		[Header("Components")]
		[SerializeField]
		private ContinueAsPanel _continueAsPanel;

		[SerializeField]
		private EnterCredentialsPanel _enterCredentialsPanel;

		public override void Initialize(IUIManager uiManager)
		{
			base.Initialize(uiManager);

			_continueAsPanel.Initialize(OnContinueButtonClickEventHandler, ActivateEnterCredentialsPanel);
			_enterCredentialsPanel.Initialize(OnLoginButtonClickEventHandler, ActivateContinueAsPanel);

			if (_dataProvider.PlayerInfo.HasRememberedInfo)
			{
				ActivateContinueAsPanel();
			}
			else
			{
				ActivateEnterCredentialsPanel();
			}
		}

		public override void Close()
		{
			_enterCredentialsPanel.EnableButtons();
			_continueAsPanel.EnableButtons();
			base.Close();
		}

		[Button("Login")]
		private void OnContinueButtonClickEventHandler()
		{
			_continueAsPanel.DisableButtons();
			TryLogin(_dataProvider.PlayerInfo.UserName, _dataProvider.PlayerInfo.Password);
		}

		private void OnLoginButtonClickEventHandler(string username, string password)
		{
			_enterCredentialsPanel.DisableButtons();
			TryLogin(username, password);
		}

		private void ActivateEnterCredentialsPanel()
		{
			_continueAsPanel.Deactivate();
			_enterCredentialsPanel.Activate(_dataProvider.PlayerInfo.HasRememberedInfo);
		}

		private void ActivateContinueAsPanel()
		{
			_enterCredentialsPanel.Deactivate();
			_continueAsPanel.Activate(_dataProvider.PlayerInfo.UserName);
		}

		private void TryLogin(string username, string password)
		{
			_playFabLogin.CheckAccountExists(username, password, (isSuccess, playFabErrorCode) => LoginCallback(isSuccess, playFabErrorCode, username, password));
		}

		private void LoginCallback(bool isSuccess, PlayFabErrorCode playFabErrorCode, string username, string password)
		{
			if (isSuccess)
			{
				_dataProvider.PlayerInfo.SetupUserInfo(username, password);
				_uiManager.DeactivatePopup();
				_uiManager.OpenMainMenuPanel();
			}
			else
			{
				switch (playFabErrorCode)
				{
					case PlayFabErrorCode.InvalidUsernameOrPassword:
						_uiManager.ActivatePopup(new PopupData()
						{
							PopupTextInfo = "Invalid password",
							FirstButtonData = new ButtonData()
							{
								ButtonText = "Ok",
								ButtonCallback = () =>
								{
									_uiManager.DeactivatePopup();
									_enterCredentialsPanel.EnableButtons();
									_continueAsPanel.EnableButtons();
								}
							},
							SecondButtonData = null
						});
						break;
					case PlayFabErrorCode.AccountNotFound:
						_uiManager.ActivatePopup(new PopupData()
						{
							PopupTextInfo = "Account does not exists.\nDo you want to create it?",
							FirstButtonData = new ButtonData()
							{
								ButtonText = "Ok",
								ButtonCallback = () =>
								{
									_uiManager.DeactivatePopup();
									_playFabLogin.RegisterAccount(username, password, isSuccess =>
									{
										if (isSuccess)
										{
											_dataProvider.PlayerInfo.SetupUserInfo(username, password);
											_uiManager.OpenMainMenuPanel();
										}
									});
								}
							},
							SecondButtonData = new ButtonData()
							{
								ButtonText = "Cancel",
								ButtonCallback = () =>
								{
									_uiManager.DeactivatePopup();
									_enterCredentialsPanel.EnableButtons();
									_continueAsPanel.EnableButtons();
								}
							}
						});
						break;
					default:
						_uiManager.ActivatePopup(new PopupData()
						{
							PopupTextInfo = "Undefined error. Try again",
							FirstButtonData = new ButtonData()
							{
								ButtonText = "Ok",
								ButtonCallback = () =>
								{
									_uiManager.DeactivatePopup();
									_enterCredentialsPanel.EnableButtons();
									_continueAsPanel.EnableButtons();
								}
							},
							SecondButtonData = null
						});
						break;
				}
			}
		}
	}
}