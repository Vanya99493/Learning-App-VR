using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LearningAppVR.UI
{
	public class RoomElement : MonoBehaviour, IDestroyable
	{
		public event Action<IDestroyable> DestroyEvent;

		[SerializeField]
		private Button _roomElementButton;
		
		[Space(10)]
		[SerializeField]
		private TMP_Text _subjectText;

		[SerializeField]
		private TMP_Text _nameText;

		[SerializeField]
		private TMP_Text _authorText;

		[SerializeField]
		private TMP_Text _accessText;

		public void Initialize(RoomData roomData, Action<RoomData> onRoomSelect)
		{
			_subjectText.text = roomData.SubjectType.ToString();
			_nameText.text = roomData.RoomName;
			_authorText.text = roomData.Author;
			_accessText.text = roomData.Access.ToString();
			
			_roomElementButton.onClick.AddListener(() => onRoomSelect?.Invoke(roomData));
		}

		public void Destroy()
		{
			DestroyEvent?.Invoke(this);
		}
	}
}