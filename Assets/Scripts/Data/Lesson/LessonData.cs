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

		public LessonData Clone()
		{
			LessonData clone = new LessonData()
			{
				Id = Id,
				LessonName = LessonName,
				Difficulty = Difficulty,
				LessonTime = LessonTime,
				EnableRandomQuestionsPool = EnableRandomQuestionsPool,
				RandomQuestionsPoolCount = RandomQuestionsPoolCount,
				BlockAnswersAfterTimeOut = BlockAnswersAfterTimeOut,
				IncreasePointsBeforeTimeOut = IncreasePointsBeforeTimeOut
			};
			
			var questionsClone = new List<QuestionData>();
			foreach (var lesson in QuestionsData)
			{
				questionsClone.Add(lesson.Clone());
			}
			clone.QuestionsData = questionsClone;

			return clone;
		}
	}
}