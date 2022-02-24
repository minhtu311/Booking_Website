using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Convenients;
using ViewModel.Results;
using WebDatPhong.Data.Infrastructures;
using WebDatPhong.Model.Models;

namespace WebDatPhong.Service.Convenients
{
    public class ConvenientService : IConvenientService
    {
        private readonly IUnitOfWork unitOfWork;

        public ConvenientService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public ResponseResult Create(ConvenientViewModel request)
        {
            try
            {
                var convenient = new Convenient()
                {
                    Name = request.Name
                };
                this.unitOfWork.ConvenientRepository.Add(convenient);
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
                this.unitOfWork.ConvenientRepository.Delete(Id);
                this.unitOfWork.SaveChange();
                return new ResponseResult();
            }
            catch (Exception ex)
            {
                return new ResponseResult(ex.Message);
            }
        }

        public IEnumerable<Convenient> GetAll()
        {
            return this.unitOfWork.ConvenientRepository.GetAll();
        }

        public ConvenientViewModel GetBedById(int Id)
        {
            var convenient = this.unitOfWork.ConvenientRepository.GetById(Id);
            return Mapper.Map<ConvenientViewModel>(convenient);
        }

        public IEnumerable<Convenient> GetConvenientByRoomId(int RoomId)
        {
            return this.unitOfWork.ConvenientRepository.GetConvenientByRoomId(RoomId);
        }

        public ResponseResult Update(ConvenientViewModel request)
        {
            try
            {
                var convenient = Mapper.Map<Convenient>(request);
                this.unitOfWork.ConvenientRepository.Update(convenient);
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
