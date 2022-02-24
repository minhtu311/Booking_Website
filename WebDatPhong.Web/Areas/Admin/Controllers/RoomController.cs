using PagedList;
using System.Linq;
using System.Web.Mvc;
using ViewModel.Rooms;
using WebDatPhong.Service.Beds;
using WebDatPhong.Service.Convenients;
using WebDatPhong.Service.Rooms;
using WebDatPhong.Service.RoomTypes;

namespace WebDatPhong.Web.Areas.Admin.Controllers
{
    public class RoomController : Controller
    {
        private readonly IBedService bedService;
        private readonly IRoomTypeService roomTypeService;
        private readonly IRoomService roomService;
        private readonly IConvenientService convenientService;

        public RoomController(IBedService bedService, IRoomTypeService roomTypeService, IRoomService roomService, IConvenientService convenientService)
        {
            this.bedService = bedService;
            this.roomTypeService = roomTypeService;
            this.roomService = roomService;
            this.convenientService = convenientService;
        }

        // GET: Admin/Room
        public ActionResult Index(string searchString, int page = 1, int pageSize = 3)
        {
            if (Session["RoleId"] == null)
            {
                return Redirect("/Admin/User/Login");
            }

            var rooms = roomService.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                rooms = rooms.Where(s => s.RoomName.ToLower().Contains(searchString.ToLower()));
            }
            ViewBag.SearchString = searchString;
            rooms = rooms.OrderByDescending(x => x.Id).ToPagedList(page, pageSize);
            return View(rooms);
        }

        public ActionResult Create()
        {
            var beds = bedService.GetAll();
            TempData["Beds"] = beds;
            var roomTypes = roomTypeService.GetAll();
            TempData["RoomTypes"] = roomTypes;
            var convenients = convenientService.GetAll();
            TempData["Convenients"] = convenients;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(RoomViewModel request)
        {
            var beds = bedService.GetAll();
            TempData["Beds"] = beds;
            var roomTypes = roomTypeService.GetAll();
            TempData["RoomTypes"] = roomTypes;
            var convenients = convenientService.GetAll();
            TempData["Convenients"] = convenients;

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

            var response = this.roomService.Create(request);
            if (response.IsSuccessed)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            return View(request);
        }

        public ActionResult Edit(int Id)
        {
            var room = this.roomService.GetRoomById(Id);

            var beds = bedService.GetAll();
            TempData["Beds"] = beds;
            var roomTypes = roomTypeService.GetAll();
            TempData["RoomTypes"] = roomTypes;
            var convenients = convenientService.GetAll();
            TempData["Convenients"] = convenients;

            var oldConvenients = this.convenientService.GetConvenientByRoomId(Id);
            TempData["OldConvenients"] = oldConvenients;
            var oldRoomType = roomTypeService.GetRoomTypeById(room.RoomTypeId);
            TempData["OldRoomType"] = oldRoomType;
            var oldBed = bedService.GetBedById(room.BedId);
            TempData["OldBed"] = oldBed;

            return View(room);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(RoomViewModel request)
        {
            var room = this.roomService.GetRoomById(request.Id);

            var beds = bedService.GetAll();
            TempData["Beds"] = beds;
            var roomTypes = roomTypeService.GetAll();
            TempData["RoomTypes"] = roomTypes;
            var convenients = convenientService.GetAll();
            TempData["Convenients"] = convenients;

            var oldConvenients = this.convenientService.GetConvenientByRoomId(request.Id);
            TempData["OldConvenients"] = oldConvenients;
            var oldRoomType = roomTypeService.GetRoomTypeById(room.RoomTypeId);
            TempData["OldRoomType"] = oldRoomType;
            var oldBed = bedService.GetBedById(room.BedId);
            TempData["OldBed"] = oldBed;

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

            var response = this.roomService.Update(request);
            if (response.IsSuccessed)
            {
                return Redirect("/admin/room/index");
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            return View(request);
        }

        public ActionResult Delete(int Id)
        {
            var room = this.roomService.GetRoomById(Id);
            var oldConvenients = this.convenientService.GetConvenientByRoomId(Id);
            TempData["OldConvenients"] = oldConvenients;
            var oldRoomType = roomTypeService.GetRoomTypeById(room.RoomTypeId);
            TempData["OldRoomType"] = oldRoomType;
            var oldBed = bedService.GetBedById(room.BedId);
            TempData["OldBed"] = oldBed;

            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Id)
        {
            var response = this.roomService.Delete(Id);
            if (response.IsSuccessed)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            return View();
        }
    }
}