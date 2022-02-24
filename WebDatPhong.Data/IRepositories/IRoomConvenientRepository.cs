using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDatPhong.Data.Infrastructures;
using WebDatPhong.Model.Models;

namespace WebDatPhong.Data.IRepositories
{
    public interface IRoomConvenientRepository : IGenericRepository<RoomConvenient>
    {
        IEnumerable<RoomConvenient> GetConvenientByRoom(int RoomId);
        void DeleteRoomConvenientByRoomId(int RoomId);
    }
}
