using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_assignment_ConferenceRoom.Models
{
    public class Reservation
    {
        [Key]
        int Id { get; set; }
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
        int RoomId { get; set; }
        Room? Room { get; set; }

        public virtual List<Room>? Rooms { get; set; }

    }
}
