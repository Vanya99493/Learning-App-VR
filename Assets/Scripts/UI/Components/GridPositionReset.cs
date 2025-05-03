using UnityEngine;

namespace LearningAppVR.UI
{
	public class GridPositionReset : MonoBehaviour
	{
		[SerializeField]
		private Transform _temporarlyParent;

		public void ResetPosition(GameObject objectToReset, Transform containerParent)
		{
			objectToReset.transform.SetParent(_temporarlyParent);
			objectToReset.transform.SetParent(containerParent);
		}
	}
}