using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDatPhong.Data.Infrastructures;
using WebDatPhong.Model.Models;

namespace WebDatPhong.Service.Roles
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork unitOfWork;
        public RoleService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Role> GetAll()
        {
            return this.unitOfWork.RoleRepository.GetAll();
        }

        public Role GetRoleById(int Id)
        {
            return this.unitOfWork.RoleRepository.GetById(Id);
        }
    }
}
