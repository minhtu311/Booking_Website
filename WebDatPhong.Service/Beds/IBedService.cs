using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Beds;
using ViewModel.Results;
using WebDatPhong.Model.Models;

namespace WebDatPhong.Service.Beds
{
    public interface IBedService
    {
        ResponseResult Create(BedViewModel request);
        ResponseResult Update(BedViewModel request);
        ResponseResult Delete(int Id);
        IEnumerable<Bed> GetAll();

        BedViewModel GetBedById(int Id);
    }
}
