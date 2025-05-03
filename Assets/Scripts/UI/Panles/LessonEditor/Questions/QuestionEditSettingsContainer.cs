using TMPro;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class QuestionEditSettingsContainer : MonoBehaviour
	{
		[SerializeField]
		private TMP_InputField _questionInputField;

		[SerializeField]
		private AnswerSelectionContainer _answerSelectionContainer;

		public void Initialize()
		{
			_answerSelectionContainer.Initialize();
		}

		public void Setup(QuestionData questionData)
		{
			_questionInputField.text = questionData.Question;
			_answerSelectionContainer.Setup(questionData);
		}
	}
}