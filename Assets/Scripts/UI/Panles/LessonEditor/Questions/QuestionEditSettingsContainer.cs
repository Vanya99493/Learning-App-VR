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

		private QuestionData _questionData;
		
		public void Initialize()
		{
			_answerSelectionContainer.Initialize();
			
			_questionInputField.onDeselect.AddListener(OnQuestionInputFieldValueChanged);
		}

		public void Setup(QuestionData questionData)
		{
			_questionData = questionData;
			_questionInputField.text = _questionData.Question;
			_answerSelectionContainer.Setup(_questionData);
		}

		private void OnQuestionInputFieldValueChanged(string value)
		{
			_questionData.Question = value;
		}
	}
}