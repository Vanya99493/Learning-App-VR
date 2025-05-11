using LearningAppVR.UI.Validators;
using TMPro;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class PointsElement : MonoBehaviour
	{
		[SerializeField]
		private TMP_InputField _pointsInputField;

		[SerializeField]
		private InputFieldValidatorElement _validetorElement;

		private QuestionData _questionData;

		public void Setup(QuestionData questionData)
		{
			_questionData = questionData;
			
			_pointsInputField.onValueChanged.RemoveAllListeners();
			_pointsInputField.text = _questionData.Points.ToString();
			_validetorElement.Deactivate();
			_pointsInputField.onValueChanged.AddListener(OnPointsValueChanged);
		}

		private void OnPointsValueChanged(string value)
		{
			if (_validetorElement.Validate())
			{
				_questionData.Points = int.Parse(value);
			}
		}
	}
}