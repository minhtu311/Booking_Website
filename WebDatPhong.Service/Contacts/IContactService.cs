using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Contacts;
using ViewModel.Results;
using WebDatPhong.Model.Models;

namespace WebDatPhong.Service.Contacts
{
    public interface IContactService
    {
        ResponseResult Create(ContactViewModel request);
        ResponseResult Confirm(int Id);
        ResponseResult Delete(int Id);
        IEnumerable<Contact> GetAll();
        Contact GetContactById(int Id);
    }
}
