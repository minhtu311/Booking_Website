using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Results;
using ViewModel.RoomTypes;
using WebDatPhong.Data.Infrastructures;
using WebDatPhong.Model.Models;

namespace WebDatPhong.Service.RoomTypes
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IUnitOfWork unitOfWork;

        public RoomTypeService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public ResponseResult Create(CreateRoomTypeViewModel request)
        {
            try
            {
                var roomType = new RoomType()
                {
                    Name = request.Name,
                    Image = request.Image,
                    Detail = request.Detail,
                    Status = true
                };
                this.unitOfWork.RoomTypeRepository.Add(roomType);
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
                this.unitOfWork.RoomTypeRepository.Delete(Id);
                this.unitOfWork.SaveChange();
                return new ResponseResult();
            }
            catch (Exception ex)
            {
                return new ResponseResult(ex.Message);
            }
        }

        public IEnumerable<RoomType> GetAll()
        {
            return this.unitOfWork.RoomTypeRepository.GetAll();
        }

        public DetailRoomTypeViewModel GetRoomTypeById(int Id)
        {
            var roomType = this.unitOfWork.RoomTypeRepository.GetById(Id);
            return Mapper.Map<DetailRoomTypeViewModel>(roomType);
        }

        public ResponseResult Update(DetailRoomTypeViewModel request)
        {
            try
            {
                var roomType = Mapper.Map<RoomType>(request);
                var rooms = this.unitOfWork.RoomRepository.GetRoomByRoomTypeId(roomType.Id);
                if (roomType.Status == false)
                {
                    foreach (var item in rooms)
                    {
                        item.Status = false;
                        this.unitOfWork.RoomRepository.Update(item);
                    }
                }
                else
                {
                    foreach (var item in rooms)
                    {
                        item.Status = true;
                        this.unitOfWork.RoomRepository.Update(item);
                    }
                }
                this.unitOfWork.RoomTypeRepository.Update(roomType);
                this.unitOfWork.SaveChange();
                return new ResponseResult();
            }
            catch (Exception ex)
            {
                return new ResponseResult(ex.Message);
            }
        }
    }
}
