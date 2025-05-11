using LearningAppVR.Player;
using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class LeaderboardPanel : BasePanel
	{
		[SerializeField]
		private DataProvider _dataProvider;
		
		[Space(10)]
		[SerializeField]
		private Button _closeButton;

		[SerializeField]
		private LeaderboardElement _leaderboardElement;

		public override void Initialize(IUIManager uiManager)
		{
			base.Initialize(uiManager);
			
			_closeButton.onClick.AddListener(() => _uiManager.OpenRoomEditorPanel(null));
		}

		public void Open(string roomId)
		{
			FillLeaderboard(roomId);
			
			base.Open();
		}

		private async void FillLeaderboard(string roomId)
		{
			var roomLeaderboardData = await _dataProvider.GetLeaderboardData(roomId);
			_leaderboardElement.FillContainer(roomLeaderboardData.UserResults);
		}
	}
}