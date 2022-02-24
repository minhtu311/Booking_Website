using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDatPhong.Data.Infrastructures;
using WebDatPhong.Data.IRepositories;
using WebDatPhong.Model.Models;

namespace WebDatPhong.Data.Repository
{
    class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(WebDatPhongDbContext context) : base(context)
        {
        }

        public Room GetRoomByRoomName(string RoomName)
        {
            var room = context.Rooms.Where(r => r.RoomName == RoomName).FirstOrDefault();
            return room;
        }

        public IEnumerable<Room> GetRoomByRoomTypeId(int RoomTypeId)
        {
            var rooms = from Room in context.Rooms
                        where Room.RoomTypeId == RoomTypeId
                        select Room;
            return rooms;
        }

        public int SearchRoom(int Id, DateTime DateIn, DateTime DateOut)
        {
            var list = (from BookingDetail in context.BookingDetails
                         join Booking in context.Bookings
                         on BookingDetail.BookingId equals Booking.Id
                         where BookingDetail.RoomId == Id
                         && Booking.Status != 0
                         && ((DateIn <= Booking.CheckIn && DateOut >= Booking.CheckIn)
                         || (DateIn <= Booking.CheckOut && DateOut >= Booking.CheckOut)
                         || (DateIn >= Booking.CheckIn && DateOut <= Booking.CheckOut))
                         select BookingDetail);
            int count = 0;
            if (list.Count() == 0) count = 0;
            else count = list.Sum(x=>x.NumberRoomBooking);
            return count;
        }
    }
}
                         //&& ((Booking.CheckIn <= DateIn && Booking.CheckOut >= DateIn)
                         //|| (Booking.CheckIn <= DateOut && Booking.CheckOut >= DateOut)
                         //|| (Booking.CheckIn >= DateIn && Booking.CheckOut <= DateOut))