using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_assignment_ConferenceRoom.Models
{
    public class Room
    {
        int Id { get; set; }
        string Name { get; set; }
        int Capacity { get; set; }
        bool Available { get; set; }

        public virtual List<Reservation> Reservations { get; set; }
    }
}
