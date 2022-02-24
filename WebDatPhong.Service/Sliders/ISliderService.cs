using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Results;
using ViewModel.Sliders;

namespace WebDatPhong.Service.Sliders
{
    public interface ISliderService
    {
        ResponseResult Create(SliderViewModel request);
        ResponseResult Update(SliderViewModel request);
        ResponseResult Delete(int Id);
        IEnumerable<SliderViewModel> GetAll();
        SliderViewModel GetSliderById(int Id);
    }
}
