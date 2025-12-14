using TDD_assignment_ConferenceRoom.Controllers;
using TDD_assignment_ConferenceRoom.Models;

namespace ComferenceRoomTest
{
    public class UnitTests
    {
        //Kollar om rum är tillgängligt och ska returnerar true
        //eftersom jag testar med mockdata i dåtiden där det saknas bokningar
        [Fact]
        public void CheckRoomAvailabilityTestTrue()
        {
            // Arrange
            var roomHandler = new RoomHandler();

            //Fixa en "ny" bokning som jag kan testa sedan         
            var _reservationhandler = new ReservationHandler();
            Reservation reservationToCheck = new Reservation
            {
                RoomId = 1,
                PersonId = 4,
                StartTime = new DateTime(2025 - 12 - 18 - 08 - 00),
                EndTime = new DateTime(2025 - 12 - 18 - 17 - 00)
            };

            // Act
            bool isAvailable = roomHandler.CheckRoomAvailability(
                            reservationToCheck.RoomId,
                            reservationToCheck.StartTime,
                            reservationToCheck.EndTime);

            // Assert
            Assert.True(isAvailable);
        }


        //Testar om reservation bara kan skapas med giltig data
        //genom att skicka in ogiltig/felaktig data
        [Fact]
        public void CreateReservationFalseTest()
        {
            // Arrange
            var reservationHandler = new ReservationHandler();
            var resToTest = new Reservation
            {
                RoomId = 2,
                PersonId = -684132547,
                StartTime = new DateTime(2025 - 11 - 20 - 09 - 00),
                EndTime = new DateTime(7654168824542)
            };

            // Act
            bool success =reservationHandler.SaveReservation(resToTest);

            // Assert
            Assert.False(success);
        }

    }
}
