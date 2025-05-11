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
		private TMP_Dropdown _difficultyDropdown;

		[SerializeField]
		private TMP_InputField _timeInputField;

		[SerializeField]
		private Toggle _randomPoolToggle;

		[SerializeField]
		private InputFieldWrapper _randomPoolInputFieldWrapper;

		[SerializeField]
		private Toggle _blockAnswersAfterTimeoutToggle;

		[SerializeField]
		private Toggle _increasePointsBeforeTimeoutToggle;

		public LessonData LessonData;
		public bool IsChanged;
		
		public void Initialize()
		{
			_randomPoolToggle.onValueChanged.AddListener(ChangeRandomPoolVisibility);
			_difficultyDropdown.ClearOptions();
			_difficultyDropdown.AddOptions(GenerateDifficultiesList(10));
			InitSettingsEvents();
		}
		
		public void SetupSettings(LessonData lessonData)
		{
			LessonData = lessonData.Clone();
			
			_lessonNameInputField.text = LessonData.LessonName;
			_difficultyDropdown.value = LessonData.Difficulty;
			_timeInputField.text = LessonData.LessonTime.ToBaseTimeString();
			_randomPoolToggle.isOn = LessonData.EnableRandomQuestionsPool;
			_randomPoolInputFieldWrapper.InputField.text = LessonData.RandomQuestionsPoolCount.ToString();
			ChangeRandomPoolVisibility(_randomPoolToggle.isOn);
			_blockAnswersAfterTimeoutToggle.isOn = LessonData.BlockAnswersAfterTimeOut;
			_increasePointsBeforeTimeoutToggle.isOn = LessonData.IncreasePointsBeforeTimeOut;
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
			
			_difficultyDropdown.onValueChanged.AddListener(value =>
			{
				LessonData.Difficulty = value;
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

			_blockAnswersAfterTimeoutToggle.onValueChanged.AddListener(value =>
			{
				LessonData.BlockAnswersAfterTimeOut = value;
				IsChanged = true;
			});
			
			_increasePointsBeforeTimeoutToggle.onValueChanged.AddListener(value =>
			{
				LessonData.IncreasePointsBeforeTimeOut = value;
				IsChanged = true;
			});
		}
	}
}