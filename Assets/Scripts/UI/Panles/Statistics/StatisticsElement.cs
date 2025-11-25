using TMPro;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class StatisticsElement : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text _nameText;

		[SerializeField]
		private TMP_Text _earnedPointsText;

		[SerializeField]
		private TMP_Text _spentTimeText;

		public void Initialize(string name, int earnedPoints, int spentTime)
		{
			_nameText.text = name;
			_earnedPointsText.text = $"{earnedPoints} б.";
			_spentTimeText.text = spentTime.ToTimerString();
		}
	}
}