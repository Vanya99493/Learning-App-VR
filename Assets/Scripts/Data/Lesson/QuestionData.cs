using System;
using System.Collections.Generic;

namespace LearningAppVR
{
	[Serializable]
	public class QuestionData
	{
		public string Question = "";
		public string CorrectAnswer = "";
		public int Points = 0;
		public List<string> Answers = new();
		public QuestionType QuestionType;
	}
}