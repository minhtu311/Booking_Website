using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDatPhong.Model.Models;

namespace WebDatPhong.Service.Roles
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAll();
        Role GetRoleById(int Id);
    }
}
