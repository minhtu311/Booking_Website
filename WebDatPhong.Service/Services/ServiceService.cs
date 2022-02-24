using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Results;
using ViewModel.Services;
using WebDatPhong.Data.Infrastructures;
using WebDatPhong.Model.Models;

namespace WebDatPhong.Service.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork unitOfWork;
        public ServiceService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ResponseResult Create(ServiceViewModel request)
        {
            try
            {
                var service = new ServiceNews() 
                {
                    Name = request.Name,
                    Detail = request.Detail,
                    Summary = request.Summary,
                    Thumbnail = request.Thumbnail
                };
                this.unitOfWork.ServiceRepository.Add(service);
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
                this.unitOfWork.ServiceRepository.Delete(Id);
                this.unitOfWork.SaveChange();
                return new ResponseResult();
            }
            catch (Exception ex)
            {
                return new ResponseResult(ex.Message);
            }
        }

        public IEnumerable<ServiceViewModel> Get4Service(int top)
        {
            var services = this.unitOfWork.ServiceRepository.Get4Service(top);
            return Mapper.Map<IEnumerable<ServiceViewModel>>(services);
        }

        public IEnumerable<ServiceViewModel> GetAll()
        {
            var services = this.unitOfWork.ServiceRepository.GetAll();
            return Mapper.Map<IEnumerable<ServiceViewModel>>(services);
        }

        public ServiceViewModel GetServiceById(int Id)
        {
            var service = this.unitOfWork.ServiceRepository.GetById(Id);
            return Mapper.Map<ServiceViewModel>(service);
        }

        public ResponseResult Update(ServiceViewModel request)
        {
            try
            {
                var service = Mapper.Map<ServiceNews>(request);
                this.unitOfWork.ServiceRepository.Update(service);
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
