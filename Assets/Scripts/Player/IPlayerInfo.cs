using System;

namespace LearningAppVR.Player
{
	public interface IPlayerInfo
	{
		public bool HasRememberedInfo { get; }
		public string UserName { get; }
		public string Password { get; }
		public UserRole UserRole { get; }

		void SetupUserInfo(string username, string password);
		public void SubscribeOnFinishInitialization(Action subscriber);
		public void SendTeacherRoleRequest();
		public void SaveResult(GlobalResultData resultData);
	}
}