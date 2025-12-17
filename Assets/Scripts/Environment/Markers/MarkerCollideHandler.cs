using System;
using UnityEngine;

namespace Environment.Markers
{
	public class MarkerCollideHandler : MonoBehaviour
	{
		[SerializeField]
		private Transform _target;
		
		[SerializeField]
		private LayerMask _layerMask;

		private bool _isColliding;
		private Quaternion _previousRotation;

		private void Update()
		{
			if (_isColliding)
			{
				_target.rotation = _previousRotation;
			}
		}
		
		private void OnCollisionEnter(Collision collision)
		{
			if (((1 << collision.gameObject.layer) & _layerMask) != 0)
			{
				_previousRotation = _target.rotation;
				_isColliding = true;
			}
		}

		private void OnCollisionExit(Collision collision)
		{
			if (((1 << collision.gameObject.layer) & _layerMask) != 0)
			{
				_isColliding = false;
			}
		}
	}
}