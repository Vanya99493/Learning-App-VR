using TMPro;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class TimeCountElement : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text _timeLeftText;

		public void Setup(int timeLeftInSeconds)
		{
			_timeLeftText.text = GenerateTimeString(timeLeftInSeconds);
		}

		private string GenerateTimeString(int seconds)
		{
			return (seconds >= 3600 ? $"{Mathf.FloorToInt(seconds / 3600)}:" : "") +
			       (seconds >= 60 ? $"{Mathf.FloorToInt(seconds % 3600 / 60):D2}:" : "") +
			       $"{seconds % 60:D2}";
		}
	}
}