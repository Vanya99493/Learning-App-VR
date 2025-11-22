using System;
using System.Collections.Generic;
using LearningAppVR.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class RoomEditorPanel : BasePanel, IRoomEditor
	{
		public event Action<RoomData> DeleteRoomEvent;

		[SerializeField]
		private DataProvider _dataProvider;
		
		[Space(10)]
		[SerializeField]
		private TMP_InputField _roomNameInputField;

		[SerializeField]
		private TMP_Dropdown _roomSubjectDropdown;

		[SerializeField]
		private Button _deleteRoomButton;

		[SerializeField]
		private EditLessonsContainer _editLessonsContainer;
		
		[SerializeField]
		private Button _leaderboardButton;

		[SerializeField]
		private Button _returnButton;

		[Space(10)]
		[SerializeField]
		private Validator _validator;

		private RoomData _roomData;

		public override void Initialize(IUIManager uiManager)
		{
			base.Initialize(uiManager);

			_uiManager.LessonEditor.SaveLessonEvent += OnSaveLessonEventHandler;
			_uiManager.LessonEditor.DeleteLessonEvent += OnDeleteLessonEventHandler;
			
			_roomNameInputField.onDeselect.AddListener(OnRoomNameChangedEventHandler);
			_roomSubjectDropdown.onValueChanged.AddListener(OnRoomSubjectChangedEventHandler);
			
			_deleteRoomButton.onClick.AddListener(DeleteRoom);
			
			_editLessonsContainer.Initialize(OnAddLessonEventHandler);
			_editLessonsContainer.SelectEvent += OnSelectLevelEventHandler;
			
			_returnButton.onClick.AddListener(OnReturnButtonClick);
			_roomSubjectDropdown.ClearOptions();
			_roomSubjectDropdown.AddOptions(new List<string>()
			{
				SubjectType.All.ToString(),
				SubjectType.Math.ToString(),
				SubjectType.Physic.ToString()
			});
		}

		public void Open(RoomData roomData)
		{
			if (roomData != null)
			{
				_roomData = roomData.Clone();
			}

			if (_roomData != null)
			{
				_leaderboardButton.onClick.RemoveAllListeners();
				_leaderboardButton.onClick.AddListener(() => _uiManager.OpenLeaderboardPanel(_roomData.Id));
				_roomNameInputField.text = _roomData.RoomName;
			
				_editLessonsContainer.FillContainer(_roomData.Lessons);
			}

			base.Open();
		}

		private void OnSelectLevelEventHandler(LessonData lessonData)
		{
			_uiManager.OpenLessonEditorPanel(lessonData);
		}

		private void OnAddLessonEventHandler()
		{
			var lessonData = new LessonData(LocalIdGenerator.GetId());
			_uiManager.OpenLessonEditorPanel(lessonData);
		}

		private void OnReturnButtonClick()
		{
			_uiManager.ActivatePopup(new PopupData()
			{
				PopupTextInfo = "Do you want to save edited room?",
				FirstButtonData = new ButtonData()
				{
					ButtonText = "Save",
					ButtonCallback = SaveRoom
				},
				SecondButtonData = new ButtonData()
				{
					ButtonText = "Don't save",
					ButtonCallback = () =>
					{
						_uiManager.DeactivatePopup();
						_uiManager.OpenOwnedRoomsPanel(false);
					}
				}
			});
		}

		private void OnRoomNameChangedEventHandler(string value)
		{
			_roomData.RoomName = value;
		}

		private void OnRoomSubjectChangedEventHandler(int valueIndex)
		{
			_roomData.SubjectType = (SubjectType)valueIndex;
		}

		private async void SaveRoom()
		{
			if (_validator.Validate())
			{
				await _dataProvider.RemoteSaveRoomData(_roomData);
				_uiManager.DeactivatePopup();
				_uiManager.OpenOwnedRoomsPanel(true);
			}
			else
			{
				_uiManager.ActivatePopup(new PopupData()
				{
					PopupTextInfo = "Validation failed",
					FirstButtonData = new ButtonData()
					{
						ButtonText = "Ok",
						ButtonCallback = () =>
						{
							_uiManager.DeactivatePopup();
						}
					}
				});
			}
		}

		private void DeleteRoom()
		{
			_uiManager.ActivatePopup(new PopupData()
			{
				PopupTextInfo = "Do you want to delete the room?",
				FirstButtonData = new ButtonData()
				{
					ButtonText = "Confirm",
					ButtonCallback = () =>
					{
						_uiManager.DeactivatePopup();
						DeleteRoomEvent?.Invoke(_roomData);
						_uiManager.OpenOwnedRoomsPanel(false);
					}
				},
				SecondButtonData = new ButtonData()
				{
					ButtonText = "Cancel",
					ButtonCallback = () =>
					{
						_uiManager.DeactivatePopup();
					}
				}
			});
		}

		private void OnSaveLessonEventHandler(LessonData lessonToSave)
		{
			int index = 0;
			bool findLesson = false;
			foreach (var lessonData in _roomData.Lessons)
			{
				if (lessonData.Id == lessonToSave.Id)
				{
					findLesson = true;
					break;
				}

				index++;
			}

			if (findLesson)
			{
				_roomData.Lessons[index] = lessonToSave.Clone();
			}
			else
			{
				_roomData.Lessons.Add(lessonToSave.Clone());
			}
		}

		private void OnDeleteLessonEventHandler(LessonData lessonToDelete)
		{
			int index = 0;
			bool findLesson = false;
			foreach (var lessonData in _roomData.Lessons)
			{
				if (lessonData.Id == lessonToDelete.Id)
				{
					findLesson = true;
					break;
				}

				index++;
			}

			if (findLesson)
			{
				_roomData.Lessons.RemoveAt(index);
			}
			else
			{
				Debug.LogError($"Cannot find the lesson {lessonToDelete.Id}");
			}
		}
	}
}