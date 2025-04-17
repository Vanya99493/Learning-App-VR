using System;
using System.Collections.Generic;

namespace LearningAppVR
{
	[Serializable]
	public class QuestionData
	{
		public string Question;
		public string CorrectAnswer;
		public List<string> Answers;
		public QuestionType QuestionType;
	}
}