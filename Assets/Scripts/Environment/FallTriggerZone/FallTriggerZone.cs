using UnityEngine;

namespace LearningAppVR.Environment
{
	public class FallTriggerZone : MonoBehaviour
	{
		[SerializeField]
		private Vector3 _spawnPosition;

		public void OnTriggerEnter(Collider other)
		{
			other.gameObject.transform.position = _spawnPosition;
		}
	}
}