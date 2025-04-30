using System;
using System.Collections.Generic;
using LearningAppVR.Configs;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class LessonsContainer : MonoBehaviour
	{
		public event Action<LessonData> SelectLevelEvent;

		[SerializeField]
		private ButtonWrapper _buttonWrapperPrefab;

		[SerializeField]
		private Transform _parent;

		[SerializeField]
		private ButtonStatesConfig _buttonStatesConfig;

		private List<ButtonWrapper> _lessonButtonsWrappers;
		private ButtonWrapper _lastActiveButtonWrapper;

		public void FillLessons(List<LessonData> lessonsCollection)
		{
			ResetLessons();
			foreach (var lessonData in lessonsCollection)
			{
				InstantiateButtonWrapper(lessonData);
			}
		}

		private void ResetLessons()
		{
			foreach (var lessonButtonsWrapper in _lessonButtonsWrappers)
			{
				Destroy(lessonButtonsWrapper.gameObject);
			}
			_lessonButtonsWrappers.Clear();
		}

		private void InstantiateButtonWrapper(LessonData lessonData)
		{
			var lessonButtonWrapper = Instantiate(_buttonWrapperPrefab, _parent);
			lessonButtonWrapper.Button.onClick.AddListener(() => OnButtonWrapperButtonClickEventHandler(lessonButtonWrapper, lessonData));
			lessonButtonWrapper.Text.text = lessonData.LessonName;
			_lessonButtonsWrappers.Add(lessonButtonWrapper);
		}

		private void OnButtonWrapperButtonClickEventHandler(ButtonWrapper buttonWrapper, LessonData lessonData)
		{
			if (_lastActiveButtonWrapper is not null)
			{
				_lastActiveButtonWrapper.Image.color = _buttonStatesConfig.PassiveColor;
			}
			_lastActiveButtonWrapper = buttonWrapper;
			_lastActiveButtonWrapper.Image.color = _buttonStatesConfig.ActiveColor;
			SelectLevelEvent?.Invoke(lessonData);
		}
	}
}