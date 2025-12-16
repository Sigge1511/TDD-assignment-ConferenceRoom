using TDD_assignment_ConferenceRoom.Controllers;
using TDD_assignment_ConferenceRoom.Models;

namespace ConferenceRoom.Test
{
    public class UnitTests
    {
        //Kollar om rum är tillgängligt och ska returnerar true
        //eftersom jag testar med mockdata i dåtiden där det saknas bokningar
        //Är eg ett integrationstest
        [Fact]
        public void IsAvailableNowTest()
        {
            // Arrange
            var roomHandler = new RoomHandler();
            var roomId = 1;
            var resList = new List<Reservation>
            {
                new Reservation
                {
                    RoomId = 2,
                    PersonId = 1,
                    StartTime = DateTime.UtcNow,
                    EndTime = DateTime.UtcNow.AddHours(3)
                },
                new Reservation
                {
                    RoomId = 3,
                    PersonId = 2,
                    StartTime = new DateTime(2025 - 10 - 15 - 11 - 00),
                    EndTime = new DateTime(2025 - 10 - 15 - 12 - 00)
                },
                new Reservation
                {
                    RoomId = 1,
                    PersonId = 4,
                    StartTime = DateTime.UtcNow,
                    EndTime = DateTime.UtcNow.AddHours(5)
                }
            };

            // Act
            //Skickar med min egenbyggda testlista med mockdata + förvalt rum
            bool availableNow = roomHandler.IsAvailableNow(resList, roomId);

            // Assert
            //Ska bli false eftersom rummet är bokat i mockdatan
            Assert.False(availableNow);
        }

        //Denna ska vara ett ordentligt enhetstest nu hoppas jag
        [Fact]
        public void isAvailableNowUnitTest()
        {
            // Arrange
            var roomHandler = new RoomHandler();
            var roomId = 1;
            var resList = new List<Reservation>
            {
                new Reservation
                {
                    RoomId = 2,
                    PersonId = 1,
                    StartTime = DateTime.UtcNow,
                    EndTime = DateTime.UtcNow.AddHours(3)
                },
                new Reservation
                {
                    RoomId = 3,
                    PersonId = 2,
                    StartTime = new DateTime(2025 - 10 - 15 - 11 - 00),
                    EndTime = new DateTime(2025 - 10 - 15 - 12 - 00)
                },
                new Reservation
                {
                    RoomId = 1,
                    PersonId = 4,
                    StartTime = DateTime.UtcNow,
                    EndTime = DateTime.UtcNow.AddHours(5)
                }
            };

            // Act
            //Skickar med min egenbyggda testlista med mockdata + förvalt rum
            bool availableNow = roomHandler.IsAvailableNow(resList, roomId);

            // Assert
            //Ska bli false eftersom rummet är bokat i mockdatan
            Assert.False(availableNow);
        }


        //Detta ska vara ett enhetstest nu tror jag :)
        [Theory]
        // 1: En bokning som INTE krockar
        [InlineData("2025-10-18 08:00", "2025-10-18 15:00", true)]
        // 2: En bokning som SKA krocka 
        [InlineData("2025-12-18 08:00", "2025-12-18 09:30", false)]
        public void CheckRoomAvailabilityTest(string startStr, string endStr, bool expectedResult)
        {
            // Arrange
            var roomHandler = new RoomHandler();
            var startTime = DateTime.Parse(startStr);
            var endTime = DateTime.Parse(endStr);
            int testRoomId = 1;
            
            // Här skapar vi vår "mockade" lista (vår låtsas-databas)
            var mockReservationList = new List<Reservation>
            {
                new Reservation
                {
                    RoomId = 1,
                    PersonId = 4,
                    StartTime = new DateTime(2025, 12, 18, 08, 00, 00),
                    EndTime = new DateTime(2025, 12, 18, 09, 00, 00)
                },
                new Reservation
                {
                    RoomId = 3,
                    PersonId = 5,
                    StartTime = new DateTime(2025, 12, 18, 12, 00, 00),
                    EndTime = new DateTime(2025, 12, 18, 13, 00, 00)
                },
                new Reservation
                {
                    RoomId = 2,
                    PersonId = 1,
                    StartTime = new DateTime(2025, 12, 18, 09, 00, 00),
                    EndTime = new DateTime(2025, 12, 18, 10, 00, 00)
                }

            };
  
            // Act
            bool result = roomHandler.CheckRoomAvailability(mockReservationList, testRoomId, startTime, endTime);

            // Assert
            // Nu passar denna assert oavsett om det förväntas bli true eller false!
            Assert.Equal(expectedResult, result);
        }


        //Detta är ett integrationstest
        [Fact]
        public void CheckRoomAvailabilityTestTrue()
        {
            // Arrange
            var roomHandler = new RoomHandler();

            //Fixa en "ny" bokning som jag kan testa sedan         
            var _reservationhandler = new ReservationHandler();
            
            //bryt ut detta och lägg i inline
            //Kan även göra en bokning till så tex en ska bli false och en true
            Reservation reservationToCheck = new Reservation
            {
                RoomId = 1,
                PersonId = 4,
                StartTime = new DateTime(2025 - 12 - 18 - 08 - 00),
                EndTime = new DateTime(2025 - 12 - 18 - 17 - 00)
            };

            List<Reservation> reservationList = _reservationhandler.GetAllReservationsToList();
            // Act
            bool isAvailable = roomHandler.CheckRoomAvailability(
                            reservationList,
                            reservationToCheck.RoomId,
                            reservationToCheck.StartTime,
                            reservationToCheck.EndTime);

            //// Assert
            //Assert.Equal(); //Ska vara antingen true eller false
                            //beroende på infon som skickas in

            //Assert.True(isAvailable);
        }


        //Testar om reservation bara kan skapas med giltig data
        //genom att skicka in ogiltig/felaktig data
        //Också mer ett integrationstest
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
            bool success = reservationHandler.SaveReservation(resToTest);

            // Assert
            Assert.False(success);
        }


        

    }
}
