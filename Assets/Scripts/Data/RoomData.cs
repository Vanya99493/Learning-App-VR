using System;
using System.Collections.Generic;

namespace LearningAppVR
{
	[Serializable]
	public class RoomData
	{
		public string RoomName = "";
		public SubjectType SubjectType;
		public List<LessonData> Lessons = new();
	}
}