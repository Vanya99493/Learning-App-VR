using UnityEngine;
using DG.Tweening;

namespace LearningAppVR
{
	public class ObjectLookAtRotator : MonoBehaviour
	{
		[SerializeField]
		private Transform _target;

		[SerializeField]
		private float _maxAngleDegrees = 10f;

		[SerializeField]
		private float _rotationDuration = 0.3f;

		private Tween _currentTween;

		private void Update()
		{
			if (_target == null)
			{
				return;
			}

			Vector3 forward = transform.forward;
			forward.y = 0;
			forward.Normalize();

			Vector3 toTarget = _target.position - transform.position;
			toTarget.y = 0;
			toTarget.Normalize();

			float angle = Vector3.Angle(-forward, toTarget);

			if (angle > _maxAngleDegrees)
			{
				Quaternion targetRotation = Quaternion.LookRotation(-toTarget, Vector3.up);
				float targetY = targetRotation.eulerAngles.y;

				_currentTween?.Kill();
				_currentTween = transform.DORotate(new Vector3(0, targetY, 0), _rotationDuration).SetEase(Ease.OutSine);
			}
		}
	}
}