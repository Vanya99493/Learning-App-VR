using System;
using System.Collections.Generic;

namespace LearningAppVR
{
	[Serializable]
	public class QuestionData
	{
		public string Id = "";
		public string Question = "";
		public string CorrectAnswer = "";
		public int Points = 0;
		public List<string> Answers = new();
		public QuestionType QuestionType;

		public QuestionData(string id)
		{
			Id = id;
		}
		
		public QuestionData Clone()
		{
			QuestionData clone = new QuestionData(Id)
			{
				Question = Question,
				CorrectAnswer = CorrectAnswer,
				Points = Points,
				QuestionType = QuestionType
			};
			
			var answersClone = new List<string>();
			foreach (var answer in Answers)
			{
				answersClone.Add(answer);
			}
			clone.Answers = answersClone;

			return clone;
		}
	}
}