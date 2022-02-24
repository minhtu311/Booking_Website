using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Rooms
{
    public class RoomBookingViewModel
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public int NumberRoomBooking { get; set; }
        public decimal CostRoom { get; set; }
        public decimal Cost { get; set; }

    }
}
