using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;
using Microsoft.Identity.Client;
using System;
using TDD_assignment_ConferenceRoom.Controllers;
using TDD_assignment_ConferenceRoom.Data;
using TDD_assignment_ConferenceRoom.Models;

namespace ConferenceRoom.Test
{
    public class IntegrationTests
    {
        private ConferenceDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<ConferenceDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var TestdbCntxt = new ConferenceDbContext(options);
            TestdbCntxt.Database.EnsureDeleted(); //För att verkligen rensa gammalt
            TestdbCntxt.Database.EnsureCreated(); //Skapar nytt & rent
            return TestdbCntxt;
        }


        //Testar om jag kan skapa en ny bokning i db öht
        [Fact]
        public void SaveReservationTest()
        {
            // Arrange
            using var testContext = CreateInMemoryContext();
            var reservationHandler = new ReservationHandler(testContext);

            var newReservation = new Reservation
            {
                RoomId = 1,
                PersonId = 3,
                StartTime = DateTime.Now.AddHours(1),
                EndTime = DateTime.Now.AddHours(2)
            };

            // Act
            reservationHandler.SaveReservation(newReservation);

            // Assert
            Assert.Equal(1, testContext.ReservationSet.Count());

        }

        //Testar om jag kan skapa en ny bokning i db
        //om det redan finns en bokning som överlappar
        [Fact]
        public void CheckRoomAvailabilityTestFalse()
        {
            // Arrange
            var testContext = CreateInMemoryContext();
            var roomHandler = new RoomHandler(testContext);

            //Fixa en bokning som redan finns i "databasen"
            Reservation existingReservation = new Reservation
            {
                RoomId = 1,
                PersonId = 2,
                StartTime = new DateTime(2025, 12, 15, 8, 0, 0),
                EndTime = new DateTime(2025, 12, 15, 18, 0, 0)
            };
            //Spara den befintliga bokningen i "databasen"
            testContext.ReservationSet.Add(existingReservation);
            testContext.SaveChanges();

            //Fixa en "ny" bokning som jag kan testa sedan         
            Reservation reservationToCheck = new Reservation
            {
                RoomId = 1,
                PersonId = 4,
                StartTime = new DateTime(2025, 12, 15, 9, 0,0),
                EndTime = new DateTime(2025, 12, 15, 12, 0, 0)
            };
            

            // Act
            bool isAvailable = roomHandler.CheckRoomAvailability(reservationToCheck.RoomId, 
                                                                 reservationToCheck.StartTime, 
                                                                 reservationToCheck.EndTime);

            // Assert
            Assert.False(isAvailable);
        }

        //Metod för att kolla utskrift av alla bokningar
        [Fact]
        public void PrintAllReservationsTest()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            var reservationHandler = new ReservationHandler(context);

            //Skapar testdata i "db",
            //just den db vi använder för test NU,
            //annars kommer den inte kunna jämföra när vi kör Assert
            SeedTestData(context); 

            // För att kunna testa CW i
            // en printmetod så behövs Stringwriter
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw); //Ändra outputstyp till StringWriter

                // Act
                reservationHandler.PrintAllReservations(); //Anropa metod

                //Fixa StreamWriter för att återställa sen??
                var standardOutput = new StreamWriter(Console.OpenStandardOutput());
                standardOutput.AutoFlush = true; //??
                Console.SetOut(standardOutput); //??

                //Assert
                //Hämta det som skrevs ut i metoden
                var testOutput = sw.ToString();
                //Hämta bokningen från "db" för att jämföra
                var reservation = context.ReservationSet.FirstOrDefault();
                Person? person = context.PersonSet.FirstOrDefault(p => p.Id == reservation.PersonId);
                Room? room = context.RoomSet.FirstOrDefault(r => r.Id == reservation.RoomId);
                var starttime = reservation.StartTime.ToString("dddd d MMM kl. HH:mm");
                var endtime = reservation.EndTime.ToString("dddd d MMM kl. HH:mm");

                //Bygg upp den förväntade strängen
                //som den ska vara enligt metoden
                string expOutput = $"* Room: {room.Name}, \nReserved by: {person.Name}, " + // <-- FIX: BARA \n
                   $"\nStarts: {starttime},\n" +
                   $"Ends: {endtime},\n" +
                   $"Capacity: {room.Capacity} people.\n\n"; 

                //JÄMFÖR NU
                Assert.Contains(expOutput, testOutput);
            }
        }


        //Hjälpmetod för att spara utrymme i testmetod
        internal void SeedTestData(ConferenceDbContext context)
        {
            var reservationHandler = new ReservationHandler(context);

            //Skapa testdata som sen ska skrivas ut
            var testPerson = new Person { Id = 2, Name = "Fredrik" };
            var testRoom = new Room { Id = 5, Name = "Baphomet", Capacity = 10, Available = true };
            context.PersonSet.Add(testPerson);
            context.RoomSet.Add(testRoom);
            context.SaveChanges();
            var reservation1 = new Reservation
            {
                RoomId = 5,
                PersonId = 2,
                StartTime = new DateTime(2026, 6, 15, 14, 0, 0),
                EndTime = new DateTime(2026, 6, 15, 15, 0, 0)
            };
            context.ReservationSet.Add(reservation1);
            context.SaveChanges();
        }
    }    
}
