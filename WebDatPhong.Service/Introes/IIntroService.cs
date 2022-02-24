using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Results;
using WebDatPhong.Model.Models;

namespace WebDatPhong.Service.Introes
{
    public interface IIntroService
    {
        ResponseResult Update(Intro request);
        Intro GetIntroById(int Id);
    }
}
