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
    public class ConvenientRepository : GenericRepository<Convenient>, IConvenientRepository
    {
        public ConvenientRepository(WebDatPhongDbContext context) : base(context)
        {
        }

        public IEnumerable<Convenient> GetConvenientByRoomId(int RoomId)
        {
            var convenients = from Convenient in context.Convenients
                              join RoomConvenient in context.RoomConvenients
                              on Convenient.Id equals RoomConvenient.ConvenientId
                              where RoomConvenient.RoomId == RoomId
                              select Convenient;
            return convenients.ToList();
        }
    }
}
