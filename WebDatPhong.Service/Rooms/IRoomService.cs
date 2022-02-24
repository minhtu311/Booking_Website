using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;
using ViewModel.Results;
using ViewModel.Rooms;
using WebDatPhong.Model.Models;

namespace WebDatPhong.Service.Rooms
{
    public interface IRoomService
    {
        ResponseResult Create(RoomViewModel request);
        ResponseResult Update(RoomViewModel request);
        ResponseResult Delete(int Id);
        IEnumerable<Room> GetAll();
        IEnumerable<Room> GetRoomByRoomTypeId(int Id);
        RoomViewModel GetRoomById(int Id);

        IEnumerable<RoomSearchViewModel> SearchEmptyRoom(DateTime DateIn, DateTime DateOut);
    }
}
