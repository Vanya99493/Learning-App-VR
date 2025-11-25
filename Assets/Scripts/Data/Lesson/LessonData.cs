using System;
using System.Collections.Generic;
using System.Linq;

namespace LearningAppVR
{
	[Serializable]
	public class LessonData
	{
		public string Id = "";
		public string LessonName = "";
		public int LessonTime = 0;
		public bool EnableRandomQuestionsPool = false;
		public int RandomQuestionsPoolCount = 0;
		public List<QuestionData> QuestionsData = new();

		public LessonData(string id)
		{
			Id = id;
		}

		public int GetGlobalPoints()
		{
			return QuestionsData.Sum(questionData => questionData.Points);
		}
		
		public LessonData Clone()
		{
			LessonData clone = new LessonData(Id)
			{
				Id = Id,
				LessonName = LessonName,
				LessonTime = LessonTime,
				EnableRandomQuestionsPool = EnableRandomQuestionsPool,
				RandomQuestionsPoolCount = RandomQuestionsPoolCount
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