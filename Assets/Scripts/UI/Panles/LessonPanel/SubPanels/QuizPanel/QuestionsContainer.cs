using System;
using System.Collections.Generic;
using LearningAppVR.Configs;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class QuestionsContainer : MonoBehaviour
	{
		[SerializeField]
		private List<ButtonWrapper> _questionsButtons = new();

		[SerializeField]
		private ButtonWrapper _questionButtonPrefab;

		[Space(5)]
		[SerializeField]
		private ButtonStatesConfig buttonStatesConfig;
		
		public void AddQuestionButton(Action<int> onQuestionButtonClickCallback)
		{
			var buttonWrapper = Instantiate(_questionButtonPrefab, gameObject.transform);

			var buttonNumber = _questionsButtons.Count + 1;
			buttonWrapper.Text.text = buttonNumber.ToString();
			buttonWrapper.Button.onClick.AddListener(() => onQuestionButtonClickCallback?.Invoke(buttonNumber));
			
			_questionsButtons.Add(buttonWrapper);
		}
		
		public void ResetContainer()
		{
			foreach (var questionButton in _questionsButtons)
			{
				Destroy(questionButton.gameObject);
			}
			_questionsButtons.Clear();
		}

		public void SetupButtonWrapperState(int buttonWrapperIndex, ButtonStateType stateType)
		{
			_questionsButtons[buttonWrapperIndex].Image.color = buttonStatesConfig.GetColor(stateType);
		}
	}
}