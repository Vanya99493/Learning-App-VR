using System;
using System.Collections.Generic;

namespace LearningAppVR
{
	[Serializable]
	public class LessonData
	{
		public string LessonName = "";
		public int LessonTime = -1;
		public bool BlockAnswersAfterTimeOut = false;
		public bool IncreasePointsBeforeTimeOut = false;
		public List<QuestionData> QuestionsData = new();
	}
}