using System.Collections.Generic;
using UnityEngine;

namespace LearningAppVR
{
	public static class ListExtensions
	{
		public static List<T> Clone<T>(this List<T> listToClone)
		{
			List<T> newList = new();
			foreach (var listElement in listToClone)
			{
				newList.Add(listElement);
			}

			return newList;
		}

		public static void Shuffle<T>(this List<T> list)
		{
			int n = list.Count;

			while (n > 1)
			{
				n--;
				int k = Random.Range(0, n + 1);
				(list[k], list[n]) = (list[n], list[k]);
			}
		} 
	}
}