using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDD_assignment_ConferenceRoom.Models;

namespace TDD_assignment_ConferenceRoom.Data
{
    public class ConferenceDbContext 
    {
        public ConferenceDbContext(DbContextOptions<ConferenceDbContext> options)
            : base(options) { }


        public DbSet<Room> RoomSet { get; set; } = default!;
        public DbSet<Reservation> ReservationSet { get; set; } = default!;
    }
}
