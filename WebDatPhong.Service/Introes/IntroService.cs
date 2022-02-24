using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Results;
using WebDatPhong.Data.Infrastructures;
using WebDatPhong.Model.Models;

namespace WebDatPhong.Service.Introes
{
    class IntroService : IIntroService
    {
        private readonly IUnitOfWork unitOfWork;
        public IntroService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Intro GetIntroById(int Id)
        {
            return this.unitOfWork.IntroRepository.GetById(Id);
        }

        public ResponseResult Update(Intro request)
        {
            try
            {
                var intro = GetIntroById(request.Id);
                intro.Detail = request.Detail;
                this.unitOfWork.IntroRepository.Update(intro);
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
