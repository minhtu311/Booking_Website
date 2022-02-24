using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using ViewModel.Rooms;
using WebDatPhong.Service.Beds;
using WebDatPhong.Service.Convenients;
using WebDatPhong.Service.Rooms;

namespace WebDatPhong.Web.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomService roomService;
        private readonly IConvenientService convenientService;
        private readonly IBedService bedService;

        public RoomController(IRoomService roomService, IConvenientService convenientService, IBedService bedService)
        {
            this.roomService = roomService;
            this.convenientService = convenientService;
            this.bedService = bedService;
        }

        // GET: Room
        public ActionResult Index()
        {
            var listRoom = roomService.GetAll();
            return View(listRoom);
        }

        //[HttpPost]
        public ActionResult SearchRoom(string bookingdate, int? bookingnumberperson)
        {
            DateTime dateIn = DateTime.ParseExact(bookingdate.Trim().Substring(0, 10), "dd-MM-yyyy", CultureInfo.InvariantCulture);
            DateTime dateOut = DateTime.ParseExact(bookingdate.Trim().Substring(13, 10), "dd-MM-yyyy", CultureInfo.InvariantCulture);
            //TempData["DateIn"] = bookingdate.Trim().Substring(0, 10);
            //TempData["DateOut"] = bookingdate.Trim().Substring(13, 10);
            TempData["DateIn"] = dateIn.Date;
            TempData["DateOut"] = dateOut.Date;
            ViewBag.DisplayDateIn = dateIn.ToString("dd-MM-yyyy");
            ViewBag.DisplayDateOut = dateOut.ToString("dd-MM-yyyy");
            if (bookingnumberperson != null)
            {
                ViewBag.BookingNumberPerson = bookingnumberperson;
            }
            var listEmptyRoom = roomService.SearchEmptyRoom(dateIn, dateOut);
            return View(listEmptyRoom);
        }

        public ActionResult DetailRoom(int id)
        {
            var room = roomService.GetRoomById(id);
            var convenients = convenientService.GetConvenientByRoomId(id);
            TempData["Convenients"] = convenients;
            var bed = bedService.GetBedById(room.BedId);
            TempData["Bed"] = bed;
            return View(room);
        }

        [HttpPost]
        public ActionResult Cart(int bookingnumberperson, string listRoomId, string listNumberRoom, DateTime checkIn, DateTime checkOut)
        {
            if (checkIn.Date == checkOut.Date)
            {
                checkOut = checkIn.AddDays(1);
            }
            Session["CheckIn"] = checkIn;
            Session["CheckOut"] = checkOut;
            Session["BookingNumberPerson"] = bookingnumberperson;
            var listRoom = listRoomId.Substring(0, listRoomId.Length - 1).Split(';');
            var listNumberRoomAdd = listNumberRoom.Substring(0, listNumberRoom.Length - 1).Split(';');
            TimeSpan numberDayBooking =checkOut - checkIn;
            int NumberDayBooking = numberDayBooking.Days;
            var listRoomAdd = new List<RoomBookingViewModel>();
            for (int i = 0; i < listRoom.Length; i++)
            {
                var RoomSearch = roomService.GetRoomById(int.Parse(listRoom[i]));
                var RoomAdd = new RoomBookingViewModel
                {
                    Id = int.Parse(listRoom[i]),
                    RoomName = RoomSearch.RoomName,
                    NumberRoomBooking = int.Parse(listNumberRoomAdd[i]),
                    CostRoom = RoomSearch.Cost,
                    Cost = int.Parse(listNumberRoomAdd[i]) * RoomSearch.Cost * NumberDayBooking
                };
                listRoomAdd.Add(RoomAdd);
            }
            Session["Cart"] = listRoomAdd;
            if (Session["CustomerId"] == null)
            {
                return RedirectToAction("Login", "Customer");
            }
            return RedirectToAction("Create", "Booking");
        }
    }
}