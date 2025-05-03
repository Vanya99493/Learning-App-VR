using LearningAppVR.Player;
using LearningAppVR.UI;
using UnityEngine;

namespace LearningAppVR
{
	public class EntryPoint : MonoBehaviour
	{
		[SerializeField]
		private UIManager _uiManager;

		[SerializeField]
		private PlayerInfo _playerInfo;

		[SerializeField]
		private AppSettings _appSettings;

		private void Awake()
		{
			Initialize();
		}

		protected virtual void Initialize()
		{
			_appSettings.Initialize();
			_playerInfo.Initialize();
			_uiManager.Initialize();
		}
	}
}