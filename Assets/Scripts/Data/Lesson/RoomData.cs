using System;
using System.Collections.Generic;

namespace LearningAppVR
{
	[Serializable]
	public class RoomData
	{
		public string RoomName = "";
		public string Author = "";
		public string Id = "";
		public SubjectType SubjectType;
		public Access Access;
		public List<LessonData> Lessons = new();

		public RoomData(string id)
		{
			Id = id;
		}

		public RoomData Clone()
		{
			RoomData clone = new RoomData(Id)
			{
				RoomName = RoomName,
				Author = Author,
				Id = Id,
				SubjectType = SubjectType,
				Access = Access
			};

			var lessonsClone = new List<LessonData>();
			foreach (var lesson in Lessons)
			{
				lessonsClone.Add(lesson.Clone());
			}
			clone.Lessons = lessonsClone;
			
			return clone;
		}
	}
}