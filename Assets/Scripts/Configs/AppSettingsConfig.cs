using UnityEngine;

namespace LearningAppVR
{
	[CreateAssetMenu(fileName = "AppSettingsConfig", menuName = "Learning App VR/Configs/App Settings/App Settings Config")]
	public class AppSettingsConfig : ScriptableObject
	{
		[field: SerializeField]
		public DeviceType DeviceType { get; private set; }
		
		[field: SerializeField]
		public bool BlockLessonExperienceOnPC { get; private set; }
	}
}