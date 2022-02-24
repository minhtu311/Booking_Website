using PagedList;
using System.Linq;
using System.Web.Mvc;
using ViewModel.Beds;
using WebDatPhong.Service.Beds;

namespace WebDatPhong.Web.Areas.Admin.Controllers
{
    public class BedController : Controller
    {
        private readonly IBedService bedService;

        public BedController(IBedService bedService)
        {
            this.bedService = bedService;
        }

        // GET: Admin/Bed
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            if (Session["RoleId"] == null)
            {
                return Redirect("/Admin/User/Login");
            }
            var beds = bedService.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                beds = beds.Where(s => s.Name.ToLower().Contains(searchString.ToLower()));
            }
            ViewBag.SearchString = searchString;
            beds = beds.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
            return View(beds);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(BedViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var response = this.bedService.Create(request);
            if (response.IsSuccessed)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            ViewBag.Message = response.ErrorMessage;
            return View(request);
        }

        public ActionResult Edit(int Id)
        {
            var bed = this.bedService.GetBedById(Id);

            return View(bed);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(BedViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var response = this.bedService.Update(request);
            if (response.IsSuccessed)
            {
                return Redirect("/admin/bed/index");
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            return View(request);
        }

        public ActionResult Delete(int Id)
        {
            var bed = this.bedService.GetBedById(Id);
            if (bed == null)
            {
                return HttpNotFound();
            }
            return View(bed);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Id)
        {
            var response = this.bedService.Delete(Id);
            if (response.IsSuccessed)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            return View();
        }
    }
}