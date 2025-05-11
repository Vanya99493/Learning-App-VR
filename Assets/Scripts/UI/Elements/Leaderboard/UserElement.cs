using TMPro;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class UserElement : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text _positionText;

		[SerializeField]
		private TMP_Text _usernameText;

		[SerializeField]
		private TMP_Text _earnedPointsText;

		public void Setup(UserResult userResult)
		{
			_positionText.text = userResult.Position;
			_usernameText.text = userResult.UserName;
			_earnedPointsText.text = userResult.Score.ToString();
		}
	}
}