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
    public class RoomConvenientRepository : GenericRepository<RoomConvenient>, IRoomConvenientRepository
    {
        public RoomConvenientRepository(WebDatPhongDbContext context) : base(context)
        {
        }

        public void DeleteRoomConvenientByRoomId(int RoomId)
        {
            var room = context.Rooms.Find(RoomId);
            var roomConvenients = GetConvenientByRoom(RoomId);
            foreach (var roomConvenient in roomConvenients)
            {
                room.RoomConvenients.Remove(roomConvenient);
            }
        }

        public IEnumerable<RoomConvenient> GetConvenientByRoom(int RoomId)
        {
            var result = from Room in context.Rooms
                         join RoomConvenient in context.RoomConvenients
                         on Room.Id equals RoomConvenient.RoomId
                         where Room.Id == RoomId
                         select RoomConvenient;
            return result.ToList();
        }
    }
}
