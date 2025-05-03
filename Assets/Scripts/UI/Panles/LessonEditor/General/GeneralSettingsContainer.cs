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

		public void Initialize()
		{
			_randomPoolToggle.onValueChanged.AddListener(ChangeRandomPoolVisibility);
			_difficultyDropdown.ClearOptions();
			_difficultyDropdown.AddOptions(GenerateDifficultiesList(10));
		}
		
		public void SetupSettings(LessonData lessonData)
		{
			_lessonNameInputField.text = lessonData.LessonName;
			_difficultyDropdown.value = lessonData.Difficulty;
			_timeInputField.text = lessonData.LessonTime.ToBaseTimeString();
			_randomPoolToggle.isOn = lessonData.EnableRandomQuestionsPool;
			_randomPoolInputFieldWrapper.InputField.text = lessonData.RandomQuestionsPoolCount.ToString();
			ChangeRandomPoolVisibility(_randomPoolToggle.isOn);
			_blockAnswersAfterTimeoutToggle.isOn = lessonData.BlockAnswersAfterTimeOut;
			_increasePointsBeforeTimeoutToggle.isOn = lessonData.IncreasePointsBeforeTimeOut;
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
	}
}