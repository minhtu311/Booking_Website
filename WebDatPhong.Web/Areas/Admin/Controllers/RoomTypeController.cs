using PagedList;
using System.Linq;
using System.Web.Mvc;
using ViewModel.RoomTypes;
using WebDatPhong.Service.RoomTypes;

namespace WebDatPhong.Web.Areas.Admin.Controllers
{
    public class RoomTypeController : Controller
    {
        private readonly IRoomTypeService roomTypeService;

        public RoomTypeController(IRoomTypeService roomTypeService)
        {
            this.roomTypeService = roomTypeService;
        }

        // GET: RoomType
        public ActionResult Index(string searchString, int page = 1, int pageSize = 3)
        {
            if (Session["RoleId"] == null)
            {
                return Redirect("/Admin/User/Login");
            }

            var typeRooms = roomTypeService.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                typeRooms = typeRooms.Where(s => s.Name.ToLower().Contains(searchString.ToLower()));
            }
            ViewBag.SearchString = searchString;
            typeRooms = typeRooms.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
            return View(typeRooms);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(CreateRoomTypeViewModel request)
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
                string UploadPath = Server.MapPath("~/Areas/Admin/wwwroot/ImageRoom/" + FileName);
                f.SaveAs(UploadPath);
                request.Image = FileName;
            }

            var response = this.roomTypeService.Create(request);
            if (response.IsSuccessed)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            return View(request);
        }

        public ActionResult Edit(int Id)
        {
            var roomType = this.roomTypeService.GetRoomTypeById(Id);

            return View(roomType);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(DetailRoomTypeViewModel request)
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
                string UploadPath = Server.MapPath("~/Areas/Admin/wwwroot/ImageRoom/" + FileName);
                f.SaveAs(UploadPath);
                request.Image = FileName;
            }

            var response = this.roomTypeService.Update(request);
            if (response.IsSuccessed)
            {
                return Redirect("/admin/roomtype/index");
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            return View(request);
        }

        public ActionResult Delete(int Id)
        {
            var roomType = this.roomTypeService.GetRoomTypeById(Id);
            if (roomType == null)
            {
                return HttpNotFound();
            }
            return View(roomType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Id)
        {
            var response = this.roomTypeService.Delete(Id);
            if (response.IsSuccessed)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            return View();
        }
    }
}