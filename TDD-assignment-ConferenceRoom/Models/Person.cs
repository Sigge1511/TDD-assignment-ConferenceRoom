using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_assignment_ConferenceRoom.Models
{
    public class Person
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public virtual List<Reservation>? Reservations { get; set; }
    }
}
