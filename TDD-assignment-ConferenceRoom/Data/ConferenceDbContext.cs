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
        //Vanlig ctor
        public ConferenceDbContext(){ }
        // Ctor för att kunna skicka in options för InMemoryDatabase
        public ConferenceDbContext(DbContextOptions<ConferenceDbContext> options)
            : base(options){}
        
        public DbSet<Room> RoomSet { get; set; } = default!;
        public DbSet<Reservation> ReservationSet { get; set; } = default!;
        public DbSet<Person> PersonSet { get; set; } = default!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // KONTROLLEN ÄR KRITISK: Kör BARA om optionsBuilder INTE har konfigurerats!
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(new ConfigurationBuilder()
                              .AddJsonFile("C:\\Users\\msigf\\source\\repos\\TDD-assignment\\TDD-assignment-ConferenceRoom\\appsettings.json")
                              .Build()
                              .GetSection("ConnectionStrings")["DefaultConnection"]);
            }   
        }
        
    }
}
