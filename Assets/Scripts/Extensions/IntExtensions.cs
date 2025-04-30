using UnityEngine;

namespace LearningAppVR
{
	public static class IntExtensions
	{
		public static string ToTimeString(this int timeInSeconds)
		{
			return (timeInSeconds >= 3600 ? $"{Mathf.FloorToInt(timeInSeconds / 3600)}:" : "") +
			       (timeInSeconds >= 60 ? $"{Mathf.FloorToInt(timeInSeconds % 3600 / 60):D2}:" : "") +
			       $"{timeInSeconds % 60:D2}";
		}
	}
}