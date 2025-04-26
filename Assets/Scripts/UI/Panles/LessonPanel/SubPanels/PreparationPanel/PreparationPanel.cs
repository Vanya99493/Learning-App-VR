using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class PreparationPanel : LessonSubPanel
	{
		[SerializeField]
		private TMP_Text _lessonNameText;

		[SerializeField]
		private Button _startButton;

		[SerializeField]
		private Button _closeButton;

		public override void Initialize(ILessonPanel lessonPanel)
		{
			base.Initialize(lessonPanel);
			
			_closeButton.onClick.AddListener(_lessonPanel.CloseLessonPanel);
			_startButton.onClick.AddListener(_lessonPanel.OpenCountDownPanel);
		}

		public void Open(string lessonName)
		{
			if (lessonName != "")
			{
				_lessonNameText.text = $"\"{lessonName}\"";
			}
			
			base.Open();
		}
	}
}