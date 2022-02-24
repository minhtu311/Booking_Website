using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;
using ViewModel.Results;
using ViewModel.Rooms;
using WebDatPhong.Data.Infrastructures;
using WebDatPhong.Model.Models;

namespace WebDatPhong.Service.Rooms
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork unitOfWork;
        public RoomService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ResponseResult Create(RoomViewModel request)
        {
            try
            {
                var roomName = this.unitOfWork.RoomRepository.GetRoomByRoomName(request.RoomName);
                if (roomName != null)
                {
                    throw new Exception("Tên phòng không được trùng ");
                }

                var convenients = request.Convenients.Substring(0, request.Convenients.Length - 1).Split(';');
                var roomConvenients = new List<RoomConvenient>();
                if (convenients != null)
                {
                    foreach (var item in convenients)
                    {
                        var roomConvenient = new RoomConvenient
                        {
                            ConvenientId = int.Parse(item)
                        };
                        roomConvenients.Add(roomConvenient);
                    }
                }
                var room = new Room()
                {
                    RoomName = request.RoomName,
                    RoomTypeId = request.RoomTypeId,
                    BedId = request.BedId,
                    NumberBed = request.NumberBed,
                    Description = request.Description,
                    NumberRoom = request.NumberRoom,
                    NumberPerson = request.NumberPerson,
                    Area = request.Area,
                    Cost = request.Cost,
                    Status = true,
                    Image = request.Image,
                    RoomConvenients = roomConvenients
                };
                this.unitOfWork.RoomRepository.Add(room);
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
                this.unitOfWork.RoomRepository.Delete(Id);
                this.unitOfWork.SaveChange();
                return new ResponseResult();
            }
            catch (Exception ex)
            {
                return new ResponseResult(ex.Message);
            }
        }

        public IEnumerable<Room> GetAll()
        {
            return this.unitOfWork.RoomRepository.GetAll();
        }

        public RoomViewModel GetRoomById(int Id)
        {
            var room = this.unitOfWork.RoomRepository.GetById(Id);
            return Mapper.Map<RoomViewModel>(room);
        }

        public IEnumerable<Room> GetRoomByRoomTypeId(int Id)
        {
            return this.unitOfWork.RoomRepository.GetRoomByRoomTypeId(Id);
        }

        public IEnumerable<RoomSearchViewModel> SearchEmptyRoom(DateTime DateIn, DateTime DateOut)
        {
            var listRoom = GetAll();
            var result = new List<RoomSearchViewModel>();
            foreach (var item in listRoom)
            {
                var room = this.unitOfWork.RoomRepository.SearchRoom(item.Id, DateIn, DateOut);
                var NumberEmptyRoom = item.NumberRoom - room;
                if (NumberEmptyRoom > 0)
                {
                    var ResultRoom = new RoomSearchViewModel();
                    ResultRoom.Id = item.Id;
                    ResultRoom.RoomName = item.RoomName;
                    ResultRoom.Cost = item.Cost;
                    ResultRoom.NumberEmptyRoom = NumberEmptyRoom;
                    ResultRoom.Image = item.Image;
                    ResultRoom.NumberPerson = item.NumberPerson;
                    result.Add(ResultRoom);
                }
            }
            return result;
        }

        public ResponseResult Update(RoomViewModel request)
        {
            try
            {
                //0. Delete convenient in room
                this.unitOfWork.RoomConvenientRepository.DeleteRoomConvenientByRoomId(request.Id);
                this.unitOfWork.SaveChange();

                //1. add convenient moi

                var convenients = request.Convenients.Substring(0, request.Convenients.Length - 1).Split(';');

                // 2. create roomConvenient
                var roomConvenients = new List<RoomConvenient>();
                if (convenients != null)
                {
                    foreach (var item in convenients)
                    {
                        var roomConvenient = new RoomConvenient
                        {
                            ConvenientId = int.Parse(item)
                        };
                        roomConvenients.Add(roomConvenient);
                    }
                }
                var room = unitOfWork.RoomRepository.GetById(request.Id);
                room.RoomName = request.RoomName;
                room.RoomTypeId = request.RoomTypeId;
                room.BedId = request.BedId;
                room.NumberBed = request.NumberBed;
                room.Description = request.Description;
                room.NumberRoom = request.NumberRoom;
                room.NumberPerson = request.NumberPerson;
                room.Area = request.Area;
                room.Cost = request.Cost;
                room.Status = request.Status;
                room.Image = request.Image;
                room.RoomConvenients = roomConvenients;

                this.unitOfWork.RoomRepository.Update(room);
                unitOfWork.SaveChange();
                return new ResponseResult();
            }
            catch (Exception ex)
            {
                return new ResponseResult(ex.Message);
            }
        }
    }
}
