using System;
using System.Collections.Generic;
using LearningAppVR.Room;
using TMPro;
using UnityEngine;

namespace LearningAppVR.UI
{
	public class FiltersContainer : MonoBehaviour
	{
		public event Action<RoomFilters> ChangeFilterEvent;

		[SerializeField]
		private TMP_Dropdown _subjectDropDown;

		[SerializeField]
		private TMP_InputField _roomNameInputField;

		[SerializeField]
		private TMP_InputField _authorNameInputField;
		
		[SerializeField]
		private TMP_Dropdown _accesDropdown;
		
		public void Initialize(IUIManager uiManager)
		{
			_subjectDropDown.ClearOptions();
			_subjectDropDown.AddOptions(new List<string>()
			{
				SubjectType.All.ToString(), 
				SubjectType.Math.ToString(), 
				SubjectType.Physic.ToString()
			});
			_subjectDropDown.onValueChanged.AddListener(_ => OnFilterChange());

			_accesDropdown.ClearOptions();
			_accesDropdown.AddOptions(new List<string>()
			{
				Access.Public.ToString(),
				Access.Private.ToString()
			});
			_accesDropdown.onValueChanged.AddListener(_ => OnFilterChange());
			
			_roomNameInputField.onSelect.AddListener(_ => uiManager.ActivateKeyboard(KeyboardType.Full, (code, value) => AddCharacter(_roomNameInputField, code, value)));
			_authorNameInputField.onSelect.AddListener(_ => uiManager.ActivateKeyboard(KeyboardType.Full, (code, value) => AddCharacter(_authorNameInputField, code, value)));
			
			_roomNameInputField.onValueChanged.AddListener(_ => OnFilterChange());
			_authorNameInputField.onValueChanged.AddListener(_ => OnFilterChange());
		}

		private void OnFilterChange()
		{
			ChangeFilterEvent?.Invoke(new RoomFilters()
			{
				SubjectType = (SubjectType)_subjectDropDown.value,
				RoomName = _roomNameInputField.text,
				AuthorName = _authorNameInputField.text,
				Access = (Access)_accesDropdown.value
			});
		}
		
		private void AddCharacter(TMP_InputField inputField, KeyboardCode keyboardCode, string symbol)
		{
			inputField.OnKeyboardInput(keyboardCode, symbol);
		}
	}
}