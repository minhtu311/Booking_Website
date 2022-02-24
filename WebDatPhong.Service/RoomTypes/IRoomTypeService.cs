using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Results;
using ViewModel.RoomTypes;
using WebDatPhong.Model.Models;

namespace WebDatPhong.Service.RoomTypes
{
    public interface IRoomTypeService
    {
        ResponseResult Create(CreateRoomTypeViewModel request);
        ResponseResult Update(DetailRoomTypeViewModel request);
        ResponseResult Delete(int Id);
        IEnumerable<RoomType> GetAll();

        DetailRoomTypeViewModel GetRoomTypeById(int Id);
    }
}
