using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Results;
using ViewModel.Services;
using WebDatPhong.Model.Models;

namespace WebDatPhong.Service.Services
{
    public interface IServiceService
    {
        ResponseResult Create(ServiceViewModel request);
        ResponseResult Update(ServiceViewModel request);
        ResponseResult Delete(int Id);
        IEnumerable<ServiceViewModel> GetAll();
        IEnumerable<ServiceViewModel> Get4Service(int top);
        ServiceViewModel GetServiceById(int Id);
    }
}
