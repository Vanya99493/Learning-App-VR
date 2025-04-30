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
			_timeLeftText.text = timeLeftInSeconds.ToTimeString();
		}
	}
}