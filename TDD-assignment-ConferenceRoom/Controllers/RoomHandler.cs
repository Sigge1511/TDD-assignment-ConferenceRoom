using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDD_assignment_ConferenceRoom.Data;
using TDD_assignment_ConferenceRoom.Models;

namespace TDD_assignment_ConferenceRoom.Controllers
{
    internal class RoomHandler
    {
        private readonly ConferenceDbContext _confContext;

        public RoomHandler(ConferenceDbContext confContext)
        {
            _confContext = confContext;
        }


        public bool CheckRoomAvailability(int roomId,DateTime start, DateTime end) 
        { 
            bool isAvailable = true;
            var reservations = _confContext.ReservationSet.Where(r => r.RoomId == roomId).ToList();
            foreach (var reservation in reservations)
            {
                if (start < reservation.EndTime && end > reservation.StartTime)
                {
                    isAvailable = false;
                    break;
                }
            }
            return isAvailable;
        }

        public bool IsAvailableNow(int roomId) 
        { 
            bool availableNow = true;
            var reservations = _confContext.ReservationSet.Where(r => r.RoomId == roomId).ToList();
            DateTime now = DateTime.Now;
            DateTime in1Hour = now.AddHours(1);

            foreach (var reservation in reservations)
            {
                if (now < reservation.EndTime && in1Hour > reservation.StartTime)
                {
                    availableNow = false;
                    break;
                }
            }
            return availableNow;
        }


        public void PrintAllRoomsAsync()
        {
            foreach (var room in _confContext.RoomSet)
            {
                Console.WriteLine($"Room ID: {room.Id}, Name: {room.Name}, Capacity: {room.Capacity}.");
            }

            return; 
        }



        public Room GetRoomById(int id) 
        { 
            var room = _confContext.RoomSet.Find(id);
            return room!;
        }
        public void GetAvailableRooms() { }
        public void AddRoom()
        {
            try
            {
                Console.WriteLine("Enter room name: ");
                string roomName = Console.ReadLine() ?? string.Empty;
                Console.WriteLine("Enter how many the room seats: ");
                int roomCapacity = int.Parse(Console.ReadLine() ?? "0");
                bool roomAvailable = true;
                Room newRoom = new Room
                {
                    Name = roomName,
                    Capacity = roomCapacity,
                    Available = roomAvailable
                };
                _confContext.RoomSet.Add(newRoom);
                _confContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding the room: {ex.Message}");
            }
        }
        //public void RemoveRoom() { }
        //public void UpdateRoom() { }
        //public void GetAllRooms() { }



    }
}
