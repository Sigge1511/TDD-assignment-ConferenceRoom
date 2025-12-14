using Microsoft.EntityFrameworkCore;
using TDD_assignment_ConferenceRoom.Controllers;
using TDD_assignment_ConferenceRoom.Data;
using TDD_assignment_ConferenceRoom.Models;

namespace ComferenceRoomTest
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
            var roomHandler = new RoomHandler();
            var _reservationhandler = new ReservationHandler();
            var testContext = CreateInMemoryContext();

            //Fixa en bokning som redan finns i "databasen"
            Reservation existingReservation = new Reservation
            {
                RoomId = 1,
                PersonId = 2,
                StartTime = new DateTime(2025 - 12 - 15 - 08 - 00),
                EndTime = new DateTime(2025 - 12 - 15 - 10 - 00)
            };
            //Spara den befintliga bokningen i "databasen"
            testContext.ReservationSet.Add(existingReservation);


            //Fixa en "ny" bokning som jag kan testa sedan         
            Reservation reservationToCheck = new Reservation
            {
                RoomId = 1,
                PersonId = 4,
                StartTime = new DateTime(2025 - 12 - 15 - 09 - 00),
                EndTime = new DateTime(2025 - 12 - 15 - 12 - 00)
            };
            

            // Act
            bool isAvailable = _reservationhandler.SaveReservation(reservationToCheck);

            // Assert
            Assert.False(isAvailable);
        }
    }
}
