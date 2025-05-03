using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class LeaderboardPanel : BasePanel
	{
		[SerializeField]
		private Button _closeButton;

		[SerializeField]
		private LeaderboardElement _leaderboardElement;

		public override void Initialize(IUIManager uiManager)
		{
			base.Initialize(uiManager);
			
			_closeButton.onClick.AddListener(() => _uiManager.OpenRoomEditorPanel(null));
		}

		public void Open(RoomLeaderboardData roomLeaderboardData)
		{
			
			
			base.Open();
		}
	}
}