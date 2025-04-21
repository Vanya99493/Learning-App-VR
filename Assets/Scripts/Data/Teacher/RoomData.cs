using System;
using System.Collections.Generic;

namespace LearningAppVR
{
	[Serializable]
	public class RoomData
	{
		public string RoomName = "";
		public string Id = "";
		public SubjectType SubjectType;
		public List<LessonData> Lessons = new();
	}
}