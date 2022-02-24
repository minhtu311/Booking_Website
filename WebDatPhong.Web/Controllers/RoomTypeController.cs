using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDatPhong.Service.Rooms;
using WebDatPhong.Service.RoomTypes;

namespace WebDatPhong.Web.Controllers
{
    public class RoomTypeController : Controller
    {
        private readonly IRoomTypeService roomTypeService;
        private readonly IRoomService roomService;
        public RoomTypeController(IRoomTypeService roomTypeService, IRoomService roomService)
        {
            this.roomTypeService = roomTypeService;
            this.roomService = roomService;
        }

        // GET: RoomType
        public ActionResult Index(int Id)
        {
            var listRoom = roomService.GetRoomByRoomTypeId(Id).ToList();
            ViewBag.RoomTypeName = roomTypeService.GetRoomTypeById(Id).Name;
            return View(listRoom);
        }

        public ActionResult MenuRoomType()
        {
            var roomType = roomTypeService.GetAll().Where(s => s.Status == true).ToList();
            return PartialView("_MenuRoomType", roomType);
        }
    }
}