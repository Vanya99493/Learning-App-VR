using System;
using System.Collections.Generic;
using LearningAppVR.Configs;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class ButtonsWrappersContainer<T> : MonoBehaviour
	{
		public event Action<T> SelectEvent;

		[SerializeField]
		protected ButtonStatesConfig _buttonStatesConfig;
		
		[SerializeField]
		protected bool _holdActiveButton = true;
		
		[Space(10)]
		[SerializeField]
		protected ButtonWrapper _buttonWrapperPrefab;

		[SerializeField]
		protected Transform _parent;

		protected List<ButtonWrapper> _buttonsWrappers = new();
		protected ButtonWrapper _lastActiveButtonWrapper;

		public virtual void FillContainer(List<T> dataCollection)
		{
			ResetLessons();
			foreach (var dataCollectionElement in dataCollection)
			{
				_buttonsWrappers.Add(InstantiateButtonWrapper(dataCollectionElement));
			}
		}

		public virtual void AddElement(T dataElement)
		{
			var element = InstantiateButtonWrapper(dataElement);
			_buttonsWrappers.Add(element);
			element.Button.onClick.Invoke();
		}

		protected void ResetLessons()
		{
			foreach (var buttonWrapper in _buttonsWrappers)
			{
				Destroy(buttonWrapper.gameObject);
			}
			_buttonsWrappers.Clear();
			_lastActiveButtonWrapper = null;
		}

		protected virtual ButtonWrapper InstantiateButtonWrapper(T dataCollectionElement)
		{
			var buttonWrapper = Instantiate(_buttonWrapperPrefab, _parent);
			buttonWrapper.Button.onClick.AddListener(() => OnButtonWrapperButtonClickEventHandler(buttonWrapper, dataCollectionElement));
			return buttonWrapper;
		}

		protected void OnButtonWrapperButtonClickEventHandler(ButtonWrapper buttonWrapper, T dataCollectionElement)
		{
			if (_holdActiveButton)
			{
				if (_lastActiveButtonWrapper is not null)
				{
					_lastActiveButtonWrapper.Image.color = _buttonStatesConfig.PassiveColor;
				}
				_lastActiveButtonWrapper = buttonWrapper;
				_lastActiveButtonWrapper.Image.color = _buttonStatesConfig.ActiveColor;
			}
			
			SelectEvent?.Invoke(dataCollectionElement);
		}
	}
}