using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Customers;
using ViewModel.Results;
using ViewModel.Users;
using WebDatPhong.Data.Infrastructures;
using WebDatPhong.Model.Models;

namespace WebDatPhong.Service.Users
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ResponseResult Create(UserViewModel request)
        {
            try
            {
                var searchUser = GetAll().Where(s => s.UserName == request.UserName).FirstOrDefault();
                if (searchUser != null)
                {
                    throw new Exception("Tên đăng nhập đã được sử dụng trước!");
                }
                else
                {
                    var userNew = Mapper.Map<User>(request);
                    this.unitOfWork.UserRepository.Add(userNew);
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
                this.unitOfWork.UserRepository.Delete(Id);
                this.unitOfWork.SaveChange();
                return new ResponseResult();
            }
            catch (Exception ex)
            {
                return new ResponseResult(ex.Message);
            }
        }

        public IEnumerable<User> GetAll()
        {
            return this.unitOfWork.UserRepository.GetAll();
        }

        public UserViewModel GetUserById(int Id)
        {
            var user = this.unitOfWork.UserRepository.GetById(Id);
            return Mapper.Map<UserViewModel>(user);
        }

        public ResponseResult Login(LoginViewModel request)
        {
            try
            {
                var user = GetAll().Where(s => s.UserName == request.UserName && s.Password == request.Password).FirstOrDefault();
                if (user != null)
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
                var user = this.unitOfWork.UserRepository.GetById(request.Id);
                user.Name = request.CustomerName;
                user.Phone = request.Phone;
                user.Email = request.Email;
                user.Gender = request.Gender;
                this.unitOfWork.UserRepository.Update(user);
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
                var searchUser = this.unitOfWork.UserRepository.GetById(request.Id);
                if (searchUser.Password == request.OldPassword)
                {
                    var user = this.unitOfWork.UserRepository.GetById(request.Id);
                    user.Password = request.NewPassword;
                    this.unitOfWork.UserRepository.Update(user);
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
