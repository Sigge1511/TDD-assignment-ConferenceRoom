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


        public async Task<bool> CheckRoomAvailability(int roomId) 
        { 
        
        
            return true;
        }
        public void GetRoomById() { }
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
