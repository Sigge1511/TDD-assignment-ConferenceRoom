using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_assignment_ConferenceRoom.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Capacity { get; set; }
        public required bool Available { get; set; } = true;

        public virtual List<Reservation>? Reservations { get; set; }
    }
}
