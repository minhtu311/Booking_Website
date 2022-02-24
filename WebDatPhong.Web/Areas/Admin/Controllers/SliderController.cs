using PagedList;
using System.Linq;
using System.Web.Mvc;
using ViewModel.Sliders;
using WebDatPhong.Service.Sliders;

namespace WebDatPhong.Web.Areas.Admin.Controllers
{
    public class SliderController : Controller
    {
        private readonly ISliderService sliderService;

        public SliderController(ISliderService sliderService)
        {
            this.sliderService = sliderService;
        }

        // GET: Admin/Slider
        public ActionResult Index(string searchString, int page = 1, int pageSize = 3)
        {
            if (Session["RoleId"] == null)
            {
                return Redirect("/Admin/User/Login");
            }

            var sliders = sliderService.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                sliders = sliders.Where(s => s.Name.ToLower().Contains(searchString.ToLower()));
            }
            ViewBag.SearchString = searchString;
            sliders = sliders.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
            return View(sliders);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(SliderViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            request.Image = "";
            var f = Request.Files["ImageFile"];
            if (f != null && f.ContentLength > 0)
            {
                string FileName = System.IO.Path.GetFileName(f.FileName);
                string UploadPath = Server.MapPath("~/Areas/Admin/wwwroot/ImageSlider/" + FileName);
                f.SaveAs(UploadPath);
                request.Image = FileName;
            }

            var response = this.sliderService.Create(request);
            if (response.IsSuccessed)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            return View(request);
        }

        public ActionResult Edit(int Id)
        {
            var slider = this.sliderService.GetSliderById(Id);

            return View(slider);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(SliderViewModel request)
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
                string UploadPath = Server.MapPath("~/Areas/Admin/wwwroot/ImageSlider/" + FileName);
                f.SaveAs(UploadPath);
                request.Image = FileName;
            }

            var response = this.sliderService.Update(request);
            if (response.IsSuccessed)
            {
                return Redirect("/admin/slider/index");
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            return View(request);
        }

        public ActionResult Delete(int Id)
        {
            var slider = this.sliderService.GetSliderById(Id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Id)
        {
            var response = this.sliderService.Delete(Id);
            if (response.IsSuccessed)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            return View();
        }
    }
}