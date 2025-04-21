using System;
using System.Collections.Generic;

namespace LearningAppVR
{
	[Serializable]
	public class TeacherData
	{
		public string TeacherName = "";
		public List<RoomData> Rooms = new();
	}
}