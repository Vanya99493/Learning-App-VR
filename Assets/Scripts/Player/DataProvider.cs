using System.Threading.Tasks;
using PlayFab;
using UnityEngine;

namespace LearningAppVR.Player
{
	public class DataProvider : MonoBehaviour
	{
		private PlayFabServerRequester _playFabServerRequester;

		public void Initialize()
		{
			_playFabServerRequester = new();
		}

		public async Task<RoomsCollection> GetRoomsCollection()
		{
			return await _playFabServerRequester.GetAllRoomsData();
		}

		public async Task DeleteRoomData(RoomData roomToDelete)
		{
			var roomsCollection = await GetRoomsCollection();

			int index = 0;
			foreach (var roomData in roomsCollection.Rooms)
			{
				if (roomData.Id == roomToDelete.Id)
				{
					roomsCollection.Rooms.RemoveAt(index);
					break;
				}

				index++;
			}
			
			await _playFabServerRequester.UpdateRoomsData(roomsCollection);
		}

		public async Task SaveRoomData(RoomData roomData)
		{
			var roomsCollection = await GetRoomsCollection();
			int index = 0;
			bool findRoom = false;
			foreach (var room in roomsCollection.Rooms)
			{
				if (room.Id == roomData.Id)
				{
					findRoom = true;
					break;
				}
				index++;
			}

			if (findRoom)
			{
				roomsCollection.Rooms[index] = roomData.Clone();
			}
			else
			{
				roomsCollection.Rooms.Add(roomData.Clone());
			}
			
			await _playFabServerRequester.UpdateRoomsData(roomsCollection);
		}

		public async Task<RoomLeaderboardData> GetLeaderboardData(string roomId)
		{
			return await _playFabServerRequester.GetLeaderboard(roomId);
		}
	}
}