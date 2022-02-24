using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDatPhong.Data.Infrastructures;
using WebDatPhong.Model.Models;

namespace WebDatPhong.Data.IRepositories
{
    public interface IRoomRepository: IGenericRepository<Room>
    {
        IEnumerable<Room> GetRoomByRoomTypeId(int RoomTypeId);

        Room GetRoomByRoomName(string RoomName);

        int SearchRoom(int Id, DateTime DateIn, DateTime DateOut);
    }
}
