using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_assignment_ConferenceRoom.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        public required DateTime StartTime { get; set; }
        public required DateTime EndTime { get; set; }
        public required int RoomId { get; set; }

        [NotMapped]
        public Room? ChosenRoom { get; set; }

        public virtual List<Room>? Rooms { get; set; }

    }
}
