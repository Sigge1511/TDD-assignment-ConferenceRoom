using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDD_assignment_ConferenceRoom.Data;

namespace TDD_assignment_ConferenceRoom.Controllers
{
    internal class ReservationHandler
    {
        private readonly ConferenceDbContext _confContext;
        private readonly RoomHandler _roomHandler;

        public ReservationHandler(ConferenceDbContext confContext, RoomHandler roomHandler)
        {
            _confContext = confContext;
            _roomHandler = roomHandler;
        }


        public void CreateReservation() { }
        //public void CancelReservation() { }
        //public void UpdateReservation() { }
        //public void GetAllReservations() { }


    }
}
