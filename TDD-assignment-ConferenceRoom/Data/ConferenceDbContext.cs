using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TDD_assignment_ConferenceRoom.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration.Json;

namespace TDD_assignment_ConferenceRoom.Data
{
    public class ConferenceDbContext : DbContext
    {
        public DbSet<Room> RoomSet { get; set; } = default!;
        public DbSet<Reservation> ReservationSet { get; set; } = default!;
        public DbSet<Person> PersonSet { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            optionsBuilder.UseSqlServer(new ConfigurationBuilder()
                                   .AddJsonFile("C:\\Users\\msigf\\source\\repos\\TDD-assignment\\TDD-assignment-ConferenceRoom\\appsettings.json")
                                   .Build()
                                   .GetSection("ConnectionStrings")["DefaultConnection"]);

        }
    }
}
