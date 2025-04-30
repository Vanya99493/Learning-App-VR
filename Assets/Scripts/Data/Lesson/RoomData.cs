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
		public RoomLeaderboardData RoomLeaderboard = new();
	}
}