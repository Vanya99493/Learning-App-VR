using System;

namespace LearningAppVR.UI
{
	public interface IRoomEditor
	{
		public event Action<RoomData> DeleteRoomEvent;
	}
}