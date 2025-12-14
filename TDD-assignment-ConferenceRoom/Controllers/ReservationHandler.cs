using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TDD_assignment_ConferenceRoom.Data;
using TDD_assignment_ConferenceRoom.Models;

namespace TDD_assignment_ConferenceRoom.Controllers
{
    internal class ReservationHandler
    {
        private readonly ConferenceDbContext _confContext;
        private readonly RoomHandler _roomHandler;

        public ReservationHandler() { }


        public void CreateReservation()
        {
            _roomHandler.PrintAllRoomsAsync();
            Console.WriteLine("\nSelect a room by writing its id:");
            int selectedRoomId = int.Parse(Console.ReadLine() ?? "0");
            var selectedRoom = _roomHandler.GetRoomById(selectedRoomId);

            Console.WriteLine("\nWrite your user id from the list below:");
            foreach (var user in _confContext.PersonSet)
            {
                Console.WriteLine($"Id: {user.Id}, Name: {user.Name}");
            }
            int selectedUserId = int.Parse(Console.ReadLine() ?? "0");

            Console.WriteLine("\nWrite the reservation start date and time (yyyy-MM-dd HH:mm):");
            DateTime startDateTime = DateTime.Parse(Console.ReadLine() ?? "");
            Console.WriteLine("\nWrite the reservation end date and time (yyyy-MM-dd HH:mm):");
            DateTime endDateTime = DateTime.Parse(Console.ReadLine() ?? "");

            bool isAvailable = _roomHandler.CheckRoomAvailability(selectedRoomId, startDateTime, endDateTime);
            if (isAvailable)
            {
                var newReservation = new Reservation
                {
                    ChosenRoom = selectedRoom,
                    RoomId = selectedRoomId,
                    PersonId = selectedUserId,
                    StartTime = startDateTime,
                    EndTime = endDateTime
                };
                SaveReservation(newReservation);
            }
            else
            {
                Console.WriteLine("The selected room is not available for the chosen time slot.");
            }



            //public void CancelReservation() { }
            //public void UpdateReservation() { }
            //public void GetAllReservations() { }


        }

        public void SaveReservation (Reservation reservation) 
        {
            _confContext.ReservationSet.Add(reservation);
            _confContext.SaveChanges();
        }
    }
}
