using System;
using System.Collections.Generic;

namespace LearningAppVR
{
	[Serializable]
	public class LessonData
	{
		public string Id = "";
		public string LessonName = "";
		public int Difficulty = 0;
		public int LessonTime = 0;
		public bool EnableRandomQuestionsPool = false;
		public int RandomQuestionsPoolCount = 0;
		public bool BlockAnswersAfterTimeOut = false;
		public bool IncreasePointsBeforeTimeOut = false;
		public List<QuestionData> QuestionsData = new();
	}
}