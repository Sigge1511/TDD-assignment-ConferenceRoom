using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_assignment_ConferenceRoom.Controllers
{
    internal class MenuService
    {
        private readonly ReservationHandler _resHandler;
        private readonly RoomHandler _roomHandler;

        public MenuService(ReservationHandler reservationHandler, RoomHandler roomHandler)
        {
            _resHandler = reservationHandler;
            _roomHandler = roomHandler;
        }

        public void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Conference Room Reservation System");
            Console.WriteLine("1. Manage Rooms");
            Console.WriteLine("2. Manage Reservations");
            Console.WriteLine("3. Exit");
            Console.Write("Select an option: ");

            int choice = GetUserChoice();
            MenuSwitch(choice);
        }
        public void ShowRoomMenu()
        {
            Console.Clear();
            Console.WriteLine("Room Management");
            Console.WriteLine("1. Add Room");
            Console.WriteLine("2. Back to Main Menu");
            Console.Write("Select an option: ");

            int choice = GetUserChoice();

        }
        public void ShowReservationMenu()
        {
            Console.Clear();
            Console.WriteLine("Reservation Management");
            Console.WriteLine("1. Create Reservation");
            Console.WriteLine("2. Back to Main Menu");
            Console.Write("Select an option: ");

            int choice = GetUserChoice();

        }
        public int GetUserChoice()
        {
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                return choice;
            }
            return 0; // Invalid choice
        }

        public void MenuSwitch(int choice)
        {
            Console.Clear();
            switch (choice)
            {
                case 1:
                    ShowRoomMenu();
                    break;
                case 2:
                    ShowReservationMenu();
                    break;
                case 3:
                    Console.WriteLine("Exiting the system. Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
