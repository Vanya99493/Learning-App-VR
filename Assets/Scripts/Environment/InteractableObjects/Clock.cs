using System;
using System.Collections;
using UnityEngine;

namespace LearningAppVR.Environment
{
	public class Clock : MonoBehaviour
	{
		[SerializeField]
		private float _updatesPerSecond = 2f;
		
		[Space(5)]
		[SerializeField]
		private Transform _hourArrow;

		[SerializeField]
		private Transform _minutesArrow;

		[SerializeField]
		private Transform _seccondsArrow;

		private Coroutine _clockCoroutine;

		private void Awake()
		{
			_clockCoroutine = StartCoroutine(UpdateClockCoroutine());
		}

		private void OnDestroy()
		{
			StopCoroutine(_clockCoroutine);
		}

		private IEnumerator UpdateClockCoroutine()
		{
			var delay = 1f / _updatesPerSecond;
			
			while (true)
			{
				var currentTime = DateTime.Now;
				_hourArrow.eulerAngles = new Vector3(
					_hourArrow.eulerAngles.x,
					_hourArrow.eulerAngles.y,
					180f + currentTime.Hour % 12f * 30f);
				_minutesArrow.eulerAngles = new Vector3(
					_minutesArrow.eulerAngles.x,
					_minutesArrow.eulerAngles.y,
					180f + currentTime.Minute * 6f);
				_seccondsArrow.eulerAngles = new Vector3(
					_seccondsArrow.eulerAngles.x,
					_seccondsArrow.eulerAngles.y,
					180f + currentTime.Second * 6f);

				yield return new WaitForSeconds(delay);
			}
		}
	}
}