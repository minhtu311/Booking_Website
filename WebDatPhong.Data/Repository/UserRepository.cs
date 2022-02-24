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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(WebDatPhongDbContext context) : base(context)
        {
        }
    }
}
