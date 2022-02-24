using PagedList;
using System.Linq;
using System.Web.Mvc;
using ViewModel.Services;
using WebDatPhong.Service.Services;

namespace WebDatPhong.Web.Areas.Admin.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IServiceService serviceService;

        public ServiceController(IServiceService serviceService)
        {
            this.serviceService = serviceService;
        }

        // GET: Admin/Service
        public ActionResult Index(string searchString, int page = 1, int pageSize = 3)
        {
            if (Session["RoleId"] == null)
            {
                return Redirect("/Admin/User/Login");
            }

            var services = serviceService.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                services = services.Where(s => s.Name.ToLower().Contains(searchString.ToLower()));
            }
            ViewBag.SearchString = searchString;
            services = services.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
            return View(services);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ServiceViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            request.Thumbnail = "";
            var f = Request.Files["ImageFile"];
            if (f != null && f.ContentLength > 0)
            {
                string FileName = System.IO.Path.GetFileName(f.FileName);
                string UploadPath = Server.MapPath("~/Areas/Admin/wwwroot/ImageService/" + FileName);
                f.SaveAs(UploadPath);
                request.Thumbnail = FileName;
            }

            var response = this.serviceService.Create(request);
            if (response.IsSuccessed)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            return View(request);
        }

        public ActionResult Edit(int Id)
        {
            var service = this.serviceService.GetServiceById(Id);

            return View(service);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(ServiceViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            //request.Image = "";
            var f = Request.Files["ImageFile"];
            if (f != null && f.ContentLength > 0)
            {
                string FileName = System.IO.Path.GetFileName(f.FileName);
                string UploadPath = Server.MapPath("~/Areas/Admin/wwwroot/ImageService/" + FileName);
                f.SaveAs(UploadPath);
                request.Thumbnail = FileName;
            }

            var response = this.serviceService.Update(request);
            if (response.IsSuccessed)
            {
                return Redirect("/admin/service/index");
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            return View(request);
        }

        public ActionResult Delete(int Id)
        {
            var service = this.serviceService.GetServiceById(Id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Id)
        {
            var response = this.serviceService.Delete(Id);
            if (response.IsSuccessed)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            return View();
        }
    }
}