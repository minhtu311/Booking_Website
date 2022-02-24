using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Customers;
using ViewModel.Results;
using WebDatPhong.Model.Models;

namespace WebDatPhong.Service.Customers
{
    public interface ICustomerService
    {
        ResponseResult Login(LoginViewModel request);
        ResponseResult Create(CustomerViewModel request);
        ResponseResult Update(EditCustomerViewModel request);
        ResponseResult UpdatePassword(UpdatePasswordViewModel request);
        ResponseResult Delete(int Id);
        IEnumerable<Customer> GetAll();
        CustomerViewModel GetCustomerById(int Id);
    }
}
