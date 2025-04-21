using LearningAppVR.Player;
using LearningAppVR.UI;
using UnityEngine;

namespace LearningAppVR
{
	public class GameSceneEntry : MonoBehaviour
	{
		[SerializeField]
		private PlayerInfo _playerInfo;

		[SerializeField]
		private UIManager _uiManager;

		private void Awake()
		{
			_playerInfo.Initialize();
			_uiManager.Initialize();
		}
	}
}