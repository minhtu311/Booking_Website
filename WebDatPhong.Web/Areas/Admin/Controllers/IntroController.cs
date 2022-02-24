using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDatPhong.Model.Models;
using WebDatPhong.Service.Introes;

namespace WebDatPhong.Web.Areas.Admin.Controllers
{
    public class IntroController : Controller
    {
        private readonly IIntroService introService;
        public IntroController(IIntroService introService)
        {
            this.introService = introService;
        }

        // GET: Admin/Intro
        public ActionResult Index()
        {
            if (Session["RoleId"] == null)
            {
                return Redirect("/Admin/User/Login");
            }

            var intro = introService.GetIntroById(1);
            return View(intro);
        }

        public ActionResult Edit(int Id)
        {
            var intro = this.introService.GetIntroById(Id);

            return View(intro);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Intro request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var response = this.introService.Update(request);
            if (response.IsSuccessed)
            {
                return Redirect("/Admin/Intro/Index");
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            return View(request);
        }
    }
}