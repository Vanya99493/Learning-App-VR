using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class GeneralSettingsContainer : MonoBehaviour
	{
		[SerializeField]
		private TMP_InputField _lessonNameInputField;

		[SerializeField]
		private TMP_InputField _timeInputField;

		[SerializeField]
		private Toggle _randomPoolToggle;

		[SerializeField]
		private InputFieldWrapper _randomPoolInputFieldWrapper;

		public LessonData LessonData;
		public bool IsChanged;
		
		public void Initialize()
		{
			_randomPoolToggle.onValueChanged.AddListener(ChangeRandomPoolVisibility);
			InitSettingsEvents();
		}
		
		public void SetupSettings(LessonData lessonData)
		{
			LessonData = lessonData.Clone();
			
			_lessonNameInputField.text = LessonData.LessonName;
			_timeInputField.text = LessonData.LessonTime.ToBaseTimeString();
			_randomPoolToggle.isOn = LessonData.EnableRandomQuestionsPool;
			_randomPoolInputFieldWrapper.InputField.text = LessonData.RandomQuestionsPoolCount.ToString();
			ChangeRandomPoolVisibility(_randomPoolToggle.isOn);
		}

		private void ChangeRandomPoolVisibility(bool isOn)
		{
			_randomPoolInputFieldWrapper.gameObject.SetActive(isOn);
		}

		private List<string> GenerateDifficultiesList(int difficultiesCount)
		{
			List<string> difficulties = new();
			for (int i = 1; i <= difficultiesCount; i++)
			{
				difficulties.Add($"{i}");
			}
			return difficulties;
		}

		private void InitSettingsEvents()
		{
			_lessonNameInputField.onDeselect.AddListener(value =>
			{
				LessonData.LessonName = value;
				IsChanged = true;
			});
			
			_timeInputField.onDeselect.AddListener(value =>
			{
				LessonData.LessonTime = value.FromBaseTimeStringToInt();
				IsChanged = true;
			});
			
			_randomPoolToggle.onValueChanged.AddListener(value =>
			{
				LessonData.EnableRandomQuestionsPool = value;
				IsChanged = true;
			});
			
			_randomPoolInputFieldWrapper.InputField.onDeselect.AddListener(value =>
			{
				LessonData.RandomQuestionsPoolCount = _randomPoolInputFieldWrapper.GetInputFieldValueInInt();
				IsChanged = true;
			});
		}
	}
}