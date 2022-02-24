using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Results;
using ViewModel.Sliders;
using WebDatPhong.Data.Infrastructures;
using WebDatPhong.Model.Models;

namespace WebDatPhong.Service.Sliders
{
    public class SliderService : ISliderService
    {
        private readonly IUnitOfWork unitOfWork;
        public SliderService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ResponseResult Create(SliderViewModel request)
        {
            try
            {
                var slider = new Slider()
                {
                    Name = request.Name,
                    Image = request.Image
                };
                this.unitOfWork.SliderRepository.Add(slider);
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
                this.unitOfWork.SliderRepository.Delete(Id);
                this.unitOfWork.SaveChange();
                return new ResponseResult();
            }
            catch (Exception ex)
            {
                return new ResponseResult(ex.Message);
            }
        }

        public IEnumerable<SliderViewModel> GetAll()
        {
            var sliders = this.unitOfWork.SliderRepository.GetAll();
            return Mapper.Map<IEnumerable<SliderViewModel>>(sliders);
        }

        public SliderViewModel GetSliderById(int Id)
        {
            var sliders = this.unitOfWork.SliderRepository.GetById(Id);
            return Mapper.Map<SliderViewModel>(sliders);
        }

        public ResponseResult Update(SliderViewModel request)
        {
            try
            {
                var slider = Mapper.Map<Slider>(request);
                this.unitOfWork.SliderRepository.Update(slider);
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
