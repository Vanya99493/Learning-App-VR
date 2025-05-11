using System;
using LearningAppVR.Configs;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class ButtonsWrappersContainer<T> : ElementsContainer<ButtonWrapper, T>
	{
		public event Action<T> SelectEvent;

		[Space(10)]
		[SerializeField]
		protected ButtonStatesConfig _buttonStatesConfig;
		
		[SerializeField]
		protected bool _holdActiveButton = true;

		[SerializeField]
		protected bool _activateAfterAdd = false;

		protected ButtonWrapper _lastActiveButtonWrapper;

		public override ButtonWrapper AddElement(T dataElement)
		{
			var element = base.AddElement(dataElement);
			if (_activateAfterAdd)
			{
				element.Button.onClick.Invoke();
			}
			return element;
		}

		protected override void ResetElements()
		{
			base.ResetElements();
			_lastActiveButtonWrapper = null;
		}

		protected override ButtonWrapper InstantiateElement(T dataCollectionElement)
		{
			var buttonWrapper = base.InstantiateElement(dataCollectionElement);
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