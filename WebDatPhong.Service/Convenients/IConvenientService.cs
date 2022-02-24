using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Convenients;
using ViewModel.Results;
using WebDatPhong.Model.Models;

namespace WebDatPhong.Service.Convenients
{
    public interface IConvenientService
    {
        ResponseResult Create(ConvenientViewModel request);
        ResponseResult Update(ConvenientViewModel request);
        ResponseResult Delete(int Id);
        IEnumerable<Convenient> GetAll();

        IEnumerable<Convenient> GetConvenientByRoomId(int RoomId);

        ConvenientViewModel GetBedById(int Id);
    }
}
