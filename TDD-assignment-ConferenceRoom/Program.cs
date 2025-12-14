using Microsoft.Extensions.DependencyInjection;
using TDD_assignment_ConferenceRoom.Controllers;

namespace TDD_assignment_ConferenceRoom
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new Data.ConferenceDbContext();
            var menu = new MenuService();
            menu.ShowMainMenu();
        }
    }
}
