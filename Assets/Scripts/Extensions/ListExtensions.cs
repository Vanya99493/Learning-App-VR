using System.Collections.Generic;

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
	}
}