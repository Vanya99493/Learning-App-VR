using System.Collections.Generic;
using UnityEngine;

namespace LearningAppVR.DataAssets
{
	[CreateAssetMenu(fileName = "LessonDataAsset", menuName = "Learning App VR/Data Assets/Lesson Data Asset")]
	public class LessonDataAsset : ScriptableObject
	{
		public string LessonName;
		public List<QuestionData> Questions;
	}
}