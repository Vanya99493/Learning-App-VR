using UnityEngine;

namespace LearningAppVR
{
	public static class IntExtensions
	{
		public static string ToTimerString(this int timeInSeconds)
		{
			return (timeInSeconds >= 3600 ? $"{Mathf.FloorToInt(timeInSeconds / 3600)}:" : "") +
			       (timeInSeconds >= 60 ? $"{Mathf.FloorToInt(timeInSeconds % 3600 / 60):D2}:" : "") +
			       $"{timeInSeconds % 60:D2}";
		}

		public static string ToBaseTimeString(this int timeInSeconds)
		{
			return (timeInSeconds >= 3600 ? $"{Mathf.FloorToInt(timeInSeconds / 3600)}h " : "") +
				(timeInSeconds >= 60 ? $"{Mathf.FloorToInt(timeInSeconds % 3600 / 60)}m " : "") +
				(timeInSeconds % 60 != 0 ?  $"{timeInSeconds % 60}s" : "");
		}
	}
}