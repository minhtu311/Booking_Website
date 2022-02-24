using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDatPhong.Service.Introes;

namespace WebDatPhong.Web.Controllers
{
    public class IntroController : Controller
    {
        private readonly IIntroService introService;
        public IntroController(IIntroService introService)
        {
            this.introService = introService;
        }

        // GET: Intro
        public ActionResult Index()
        {
            var intro = introService.GetIntroById(1);
            return View(intro);
        }
    }
}