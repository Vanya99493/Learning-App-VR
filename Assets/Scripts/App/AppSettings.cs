using UnityEngine;

namespace LearningAppVR
{
	public class AppSettings : MonoBehaviour
	{
		[SerializeField]
		private AppSettingsConfig _appSettingsConfig;
		
		public static DeviceType DeviceType { get; private set; }
		public static bool BlockLessonsExperienceOnPC { get; private set; }

		public void Initialize()
		{
			DeviceType = _appSettingsConfig.DeviceType;
			BlockLessonsExperienceOnPC = _appSettingsConfig.BlockLessonExperienceOnPC;
		}

		public void SwitchDeviceType()
		{
			DeviceType = DeviceType == DeviceType.PC ? DeviceType.VR : DeviceType.PC;
		}
	}
}