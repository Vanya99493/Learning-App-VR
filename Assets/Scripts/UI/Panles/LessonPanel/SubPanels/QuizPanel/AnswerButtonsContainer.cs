using System;
using System.Collections.Generic;
using LearningAppVR.Configs;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class AnswerButtonsContainer : MonoBehaviour
	{
		[SerializeField]
		private List<ButtonWrapper> _buttons;

		[Space(5)]
		[SerializeField]
		private ButtonStatesConfig buttonStatesConfig;

		public void Initialize(Action<string> onSelectAnswer)
		{
			foreach (var button in _buttons)
			{
				button.Button.onClick.AddListener(() => onSelectAnswer?.Invoke(button.Text.text));
			}
		}
		
		public void Setup(List<string> answers, string previousAnswer = null)
		{
			int index = 0;
			foreach (var button in _buttons)
			{
				if (index < answers.Count)
				{
					string answer = answers[index];
					button.Text.text = answer;
					button.Image.color = previousAnswer is not null ? buttonStatesConfig.ActiveColor : buttonStatesConfig.PassiveColor;
				}
				else
				{
					button.gameObject.SetActive(false);
				}
			}
		}
	}
}