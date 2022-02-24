using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Rooms
{
    public class RoomSearchViewModel
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public decimal Cost { get; set; }
        public int NumberPerson { get; set; }
        public int NumberEmptyRoom { get; set; }
        public string Image { get; set; }
    }
}
