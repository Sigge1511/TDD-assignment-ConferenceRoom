using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDD_assignment_ConferenceRoom.Data;
using TDD_assignment_ConferenceRoom.Models;

namespace TDD_assignment_ConferenceRoom.Controllers
{
    public class RoomHandler
    {
        readonly ConferenceDbContext _confContext = new ConferenceDbContext();
        public RoomHandler()
        {
        }
        public RoomHandler(ConferenceDbContext confDbContext)
        {
            _confContext = confDbContext;
        }




        public bool CheckRoomAvailability(int roomId, DateTime start, DateTime end)
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


        //Ta in en lista med alla reservations mha hjälpmetod
        //- då kan jag göra ett enhetstest.
        //Ta bort contextanrop från denna metod. Ha kvar att rumid följer med
        public bool IsAvailableNow(List<Reservation> reservations,int roomId) 
        { 
            bool availableNow = true;
            DateTime now = DateTime.UtcNow;
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

        public List<Room> GetAllRoomsToList() 
        { 
            var roomsList = _confContext.RoomSet.ToList();
            return roomsList;
        }

        public Room GetRoomById(int id)
        {
            var room = _confContext.RoomSet.Find(id);
            return room!;
        }




        //public void GetAvailableRooms() { }



        //public void AddRoom()
        //{
        //    try
        //    {
        //        Console.WriteLine("Enter room name: ");
        //        string roomName = Console.ReadLine() ?? string.Empty;
        //        Console.WriteLine("Enter how many the room seats: ");
        //        int roomCapacity = int.Parse(Console.ReadLine() ?? "0");
        //        bool roomAvailable = true;
        //        Room newRoom = new Room
        //        {
        //            Name = roomName,
        //            Capacity = roomCapacity,
        //            Available = roomAvailable
        //        };
        //        _confContext.RoomSet.Add(newRoom);
        //        _confContext.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"An error occurred while adding the room: {ex.Message}");
        //    }
        //}
        //public void RemoveRoom() { }
        //public void UpdateRoom() { }
        //public void GetAllRooms() { }



    }
}
