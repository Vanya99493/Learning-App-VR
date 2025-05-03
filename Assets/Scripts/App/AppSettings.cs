using UnityEngine;

namespace LearningAppVR
{
	public class AppSettings : MonoBehaviour
	{
		[SerializeField]
		private DeviceType _deviceType;
		
		public static DeviceType DeviceType { get; private set; }

		public void Initialize()
		{
			DeviceType = _deviceType;
		}
	}
}