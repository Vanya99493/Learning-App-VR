namespace LearningAppVR
{
	public static class DataExtensions
	{
		public static void ValidateQuestionsTypes(this RoomsCollection roomsCollection)
		{
			foreach (var room in roomsCollection.Rooms)
			{
				foreach (var lesson in room.Lessons)
				{
					foreach (var question in lesson.QuestionsData)
					{
						bool isClear = true;

						foreach (var answer in question.Answers)
						{
							if (answer != "")
							{
								isClear = false;
							}
						}

						if (isClear)
						{
							question.QuestionType = QuestionType.Input;
						}
					}
				}
			}
		}
	}
}