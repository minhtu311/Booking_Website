using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel.Contacts;
using WebDatPhong.Service.Contacts;

namespace WebDatPhong.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService contactService;

        public ContactController(IContactService contactService)
        {
            this.contactService = contactService;
        }
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ContactViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var response = this.contactService.Create(request);
            if (response.IsSuccessed)
            {
                TempData["Message"] = "Quý khách đã gửi thư liên hệ thành công";
                return Redirect(nameof(Create));
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            ViewBag.Message = response.ErrorMessage;
            return View(request);
        }
    }
}