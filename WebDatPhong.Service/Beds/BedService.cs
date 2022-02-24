using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Beds;
using ViewModel.Results;
using WebDatPhong.Data.Infrastructures;
using WebDatPhong.Model.Models;

namespace WebDatPhong.Service.Beds
{
    public class BedService : IBedService
    {
        private readonly IUnitOfWork unitOfWork;

        public BedService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public ResponseResult Create(BedViewModel request)
        {
            try
            {
                var bed = new Bed()
                {
                    Name = request.Name
                };
                this.unitOfWork.BedRepository.Add(bed);
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
                this.unitOfWork.BedRepository.Delete(Id);
                this.unitOfWork.SaveChange();
                return new ResponseResult();
            }
            catch (Exception ex)
            {
                return new ResponseResult(ex.Message);
            }
        }

        public IEnumerable<Bed> GetAll()
        {
            return this.unitOfWork.BedRepository.GetAll();
        }

        public BedViewModel GetBedById(int Id)
        {
            var bed = this.unitOfWork.BedRepository.GetById(Id);
            return Mapper.Map<BedViewModel>(bed);
        }

        public ResponseResult Update(BedViewModel request)
        {
            try
            {
                var bed = Mapper.Map<Bed>(request);
                this.unitOfWork.BedRepository.Update(bed);
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
