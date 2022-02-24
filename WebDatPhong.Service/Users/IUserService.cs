using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Customers;
using ViewModel.Results;
using ViewModel.Users;
using WebDatPhong.Model.Models;

namespace WebDatPhong.Service.Users
{
    public interface IUserService
    {
        ResponseResult Create(UserViewModel request);
        ResponseResult Delete(int Id);
        ResponseResult Update(EditCustomerViewModel request);
        ResponseResult UpdatePassword(UpdatePasswordViewModel request);
        IEnumerable<User> GetAll();
        UserViewModel GetUserById(int Id);

        ResponseResult Login(LoginViewModel request);
    }
}
