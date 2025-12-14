using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_assignment_ConferenceRoom.Controllers
{
    public class MenuService
    {
        readonly ReservationHandler _resHandler = new ReservationHandler();
        readonly RoomHandler _roomHandler = new RoomHandler();

        public MenuService(){   }

        public void ShowMainMenu()
        {
            int choice = 0;

            while (choice != 3)
            {
                Console.Clear();
                Console.WriteLine("Conference Room Reservation System");
                Console.WriteLine("1. Manage Rooms");
                Console.WriteLine("2. Manage Reservations");
                Console.WriteLine("3. Exit");
                Console.Write("Select an option: ");

                choice = GetUserChoice();
                MenuMainSwitch(choice);
            }
        }
        public void ShowRoomMenu()
        {
            Console.Clear();
            Console.WriteLine("Room Management");
            Console.WriteLine("1. Add Room");
            Console.WriteLine("2. Check if a room is available the coming hour.");
            Console.WriteLine("3. Back to Main Menu");
            Console.Write("Select an option: ");

            int choice = GetUserChoice();

        }
        public void ShowReservationMenu()
        {
            Console.Clear();
            Console.WriteLine("Reservation Management");
            Console.WriteLine("1. Create Reservation");
            Console.WriteLine("2. View All Reservations");
            Console.WriteLine("3. Back to Main Menu");
            Console.Write("Select an option: ");

            int choice = GetUserChoice();
            MenuReservationSwitch(choice);

        }
        public int GetUserChoice()
        {
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                return choice;
            }
            return 0; // Invalid choice
        }

        public void MenuMainSwitch(int choice)
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

        public void MenuRoomSwitch(int choice)
        {
            Console.Clear();
            switch (choice)
            {
                case 1:
                    _roomHandler.AddRoom();
                    break;
                case 2:
                    Console.WriteLine("Enter the Room ID to check availability:");
                    _roomHandler.PrintAllRoomsAsync();
                    int roomId = int.Parse(Console.ReadLine() ?? "0");
                    bool isAvailable = _roomHandler.IsAvailableNow(roomId);
                    if (isAvailable)
                    {
                        Console.WriteLine("The room is available for the coming hour.");
                    }
                    else
                    {
                        Console.WriteLine("The room is NOT available for the coming hour.");
                    }
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 3:
                    ShowMainMenu();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        public void MenuReservationSwitch(int choice)
        {
            Console.Clear();
            switch (choice)
            {
                case 1:
                    _resHandler.CreateReservation();
                    break;
                case 2:
                    Console.Clear();
                    _resHandler.PrintAllReservations();
                    Console.WriteLine("\nPress any key to continue.");
                    Console.ReadKey();
                    break;
                case 3:
                    ShowMainMenu();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
