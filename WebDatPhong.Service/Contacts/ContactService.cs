using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Contacts;
using ViewModel.Results;
using WebDatPhong.Data.Infrastructures;
using WebDatPhong.Model.Models;

namespace WebDatPhong.Service.Contacts
{
    public class ContactService: IContactService
    {
        private readonly IUnitOfWork unitOfWork;
        public ContactService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ResponseResult Confirm(int Id)
        {
            try
            {
                var contact = GetContactById(Id);
                contact.Status = true;
                this.unitOfWork.ContactRepository.Update(contact);
                this.unitOfWork.SaveChange();
                return new ResponseResult();
            }
            catch (Exception ex)
            {
                return new ResponseResult(ex.Message);
            }
        }

        public ResponseResult Create(ContactViewModel request)
        {
            try
            {
                var contact = new Contact()
                {
                    CustomerName = request.CustomerName,
                    Email = request.Email,
                    Phone = request.Phone,
                    Subject = request.Subject,
                    Detail = request.Detail,
                    Status = false
                };
                this.unitOfWork.ContactRepository.Add(contact);
                this.unitOfWork.SaveChange();
                return new ResponseResult();
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
                this.unitOfWork.ContactRepository.Delete(Id);
                this.unitOfWork.SaveChange();
                return new ResponseResult();
            }
            catch (Exception ex)
            {
                return new ResponseResult(ex.Message);
            }
        }

        public IEnumerable<Contact> GetAll()
        {
            return this.unitOfWork.ContactRepository.GetAll();
        }

        public Contact GetContactById(int Id)
        {
            return this.unitOfWork.ContactRepository.GetById(Id);
        }
    }
}
