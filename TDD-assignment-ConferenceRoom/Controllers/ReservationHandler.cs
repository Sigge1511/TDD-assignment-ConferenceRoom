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
    public class ReservationHandler
    {
        
        readonly RoomHandler _roomHandler = new RoomHandler();
        readonly ConferenceDbContext _confContext =new ConferenceDbContext();

        public ReservationHandler(){}
        public ReservationHandler(ConferenceDbContext confDbContext) 
        { 
            _confContext = confDbContext;
        }        

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
                Console.WriteLine("\nPress any key to continue.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("The selected room is not available for the chosen time slot.");
                Console.WriteLine("\nPress any key to continue.");
                Console.ReadKey();
            }
        }
        public bool SaveReservation (Reservation reservation) 
        {
            try
            {
                _confContext.ReservationSet.Add(reservation);
                _confContext.SaveChanges();
                Console.WriteLine("Reservation successfully made.");
                return true;
            }
            catch 
            {
                Console.WriteLine("Something went wrong. Please try again");
                return false;
            }
        }
        public void PrintAllReservations()
        {
            var reservations = GetAllReservationsToList();
            foreach (var reservation in reservations)
            {
                var person = _confContext.PersonSet.FirstOrDefault(p => p.Id == reservation.PersonId);
                var room = _confContext.RoomSet.FirstOrDefault(r => r.Id == reservation.RoomId);
                var starttime = reservation.StartTime.ToString("dddd d MMM kl. HH:mm");
                var endtime = reservation.EndTime.ToString("dddd d MMM kl. HH:mm");

                Console.WriteLine($"* Room: {room.Name}, \nReserved by: {person.Name}, " +
                    $"\nStarts: {starttime},\n" +
                    $"Ends: {endtime},\n" +
                    $"Capacity: {room.Capacity} people.\n\n");
            }            
        }

        public List<Reservation> GetAllReservationsToList()
        {
            return _confContext.ReservationSet.ToList();
        }
    }

}
