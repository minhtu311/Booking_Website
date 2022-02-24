using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Customers;
using ViewModel.Results;
using WebDatPhong.Data.Infrastructures;
using WebDatPhong.Model.Models;

namespace WebDatPhong.Service.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork unitOfWork;
        public CustomerService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ResponseResult Create(CustomerViewModel request)
        {
            try
            {
                var searchCustomer = GetAll().Where(s => s.UserName == request.UserName).FirstOrDefault();
                if (searchCustomer != null)
                {
                    throw new Exception("Username đã được đăng ký trước!");
                }
                else
                {
                    var customerNew = Mapper.Map<Customer>(request);
                    this.unitOfWork.CustomerRepository.Add(customerNew);
                    this.unitOfWork.SaveChange();
                    return new ResponseResult();
                }
            }
            catch (Exception ex)
            {
                return new ResponseResult(ex.Message);
            }
        }

        public ResponseResult Delete(int Id)
        {
            try
            {
                var bookingOfCustomer = this.unitOfWork.BookingRepository.GetBookingByCustomerId(Id);
                if (bookingOfCustomer.Count() > 0)
                {
                    throw new Exception("Khách hàng đã có đơn đặt phòng trên hệ thống không thể xóa");
                }
                else
                {
                    this.unitOfWork.CustomerRepository.Delete(Id);
                    this.unitOfWork.SaveChange();
                    return new ResponseResult();
                }
            }
            catch (Exception ex)
            {
                return new ResponseResult(ex.Message);
            }
        }

        public IEnumerable<Customer> GetAll()
        {
            return this.unitOfWork.CustomerRepository.GetAll();
        }

        public CustomerViewModel GetCustomerById(int Id)
        {
            var customer = this.unitOfWork.CustomerRepository.GetById(Id);
            return Mapper.Map<CustomerViewModel>(customer);
        }

        public ResponseResult Login(LoginViewModel request)
        {
            try
            {
                var customer = GetAll().Where(s => s.UserName == request.UserName && s.Password == request.Password).FirstOrDefault();
                if (customer != null)
                {
                    return new ResponseResult();
                }
                else
                {
                    throw new Exception("Tên user hoặc mật khẩu sai");
                }
            }
            catch (Exception ex)
            {
                return new ResponseResult(ex.Message);
            }
        }

        public ResponseResult Update(EditCustomerViewModel request)
        {
            try
            {
                var customer = Mapper.Map<Customer>(this.unitOfWork.CustomerRepository.GetById(request.Id));
                customer.CustomerName = request.CustomerName;
                customer.Phone = request.Phone;
                customer.Email = request.Email;
                customer.Gender = request.Gender;
                this.unitOfWork.CustomerRepository.Update(customer);
                this.unitOfWork.SaveChange();
                return new ResponseResult();
            }
            catch (Exception ex)
            {
                return new ResponseResult(ex.Message);
            }
        }

        public ResponseResult UpdatePassword(UpdatePasswordViewModel request)
        {
            try
            {
                var searchCustomer = this.unitOfWork.CustomerRepository.GetById(request.Id);
                if (searchCustomer.Password == request.OldPassword)
                {
                    var customer = Mapper.Map<Customer>(this.unitOfWork.CustomerRepository.GetById(request.Id));
                    customer.Password = request.NewPassword;
                    this.unitOfWork.CustomerRepository.Update(customer);
                    this.unitOfWork.SaveChange();
                    return new ResponseResult();
                }
                else
                {
                    throw new Exception("Mật khẩu cũ không chính xác");
                }
            }
            catch (Exception ex)
            {
                return new ResponseResult(ex.Message);
            }
        }
    }
}
