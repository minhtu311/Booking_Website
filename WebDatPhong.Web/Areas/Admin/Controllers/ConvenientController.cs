using PagedList;
using System.Linq;
using System.Web.Mvc;
using ViewModel.Convenients;
using WebDatPhong.Service.Convenients;

namespace WebDatPhong.Web.Areas.Admin.Controllers
{
    public class ConvenientController : Controller
    {
        private readonly IConvenientService convenientService;

        public ConvenientController(IConvenientService convenientService)
        {
            this.convenientService = convenientService;
        }

        // GET: Admin/Convenient
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            if (Session["RoleId"] == null)
            {
                return Redirect("/Admin/User/Login");
            }
            var convenients = convenientService.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                convenients = convenients.Where(s => s.Name.ToLower().Contains(searchString.ToLower()));
            }
            ViewBag.SearchString = searchString;
            convenients = convenients.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
            return View(convenients);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ConvenientViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var response = this.convenientService.Create(request);
            if (response.IsSuccessed)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            return View(request);
        }

        public ActionResult Edit(int Id)
        {
            var convenient = this.convenientService.GetBedById(Id);

            return View(convenient);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(ConvenientViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var response = this.convenientService.Update(request);
            if (response.IsSuccessed)
            {
                return Redirect("/admin/convenient/index");
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            return View(request);
        }

        public ActionResult Delete(int Id)
        {
            var convenient = this.convenientService.GetBedById(Id);
            if (convenient == null)
            {
                return HttpNotFound();
            }
            return View(convenient);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Id)
        {
            var response = this.convenientService.Delete(Id);
            if (response.IsSuccessed)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            return View();
        }
    }
}