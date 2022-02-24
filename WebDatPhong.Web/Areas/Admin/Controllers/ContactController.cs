using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDatPhong.Service.Contacts;

namespace WebDatPhong.Web.Areas.Admin.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService contactService;

        public ContactController(IContactService contactService)
        {
            this.contactService = contactService;
        }
        // GET: Admin/Contact
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            if (Session["RoleId"] == null)
            {
                return Redirect("/Admin/User/Login");
            }
            var contactUnconfirms = contactService.GetAll().Where(x => x.Status == false);
            if (!string.IsNullOrEmpty(searchString))
            {
                contactUnconfirms = contactUnconfirms.Where(s => s.Subject.ToLower().Contains(searchString.ToLower()));
            }
            ViewBag.SearchString = searchString;
            contactUnconfirms = contactUnconfirms.OrderBy(x => x.Id).ToPagedList(page, pageSize);
            return View(contactUnconfirms);
        }

        public ActionResult ListConfirmed(string searchString, int page = 1, int pageSize = 5)
        {
            var contactUnconfirms = contactService.GetAll().Where(x => x.Status == true);
            if (!string.IsNullOrEmpty(searchString))
            {
                contactUnconfirms = contactUnconfirms.Where(s => s.Subject.ToLower().Contains(searchString.ToLower()));
            }
            ViewBag.SearchString = searchString;
            contactUnconfirms = contactUnconfirms.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
            return View(contactUnconfirms);
        }

        public ActionResult DetailUnconfirmed(int Id)
        {
            var contact = contactService.GetContactById(Id);
            return View(contact);
        }

        public ActionResult Confirm(int Id)
        {
            var response = this.contactService.Confirm(Id);
            if (response.IsSuccessed)
            {
                return RedirectToAction("ListConfirmed", "Contact");
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            return View();
        }

        public ActionResult Delete(int Id)
        {
            var contact = this.contactService.GetContactById(Id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Id)
        {
            var response = this.contactService.Delete(Id);
            if (response.IsSuccessed)
            {
                return RedirectToAction("ListConfirmed", "Contact");
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            return View();
        }
    }
}