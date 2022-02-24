using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDatPhong.Service.Bookings;

namespace WebDatPhong.Web.Areas.Admin.Controllers
{
    public class ConfirmedBookingController : Controller
    {
        private readonly IBookingService bookingService;

        public ConfirmedBookingController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        // GET: Admin/Confirmed
        public ActionResult Index(string searchString, int page = 1, int pageSize = 8)
        {
            if (Session["RoleId"] == null)
            {
                return Redirect("/Admin/User/Login");
            }
            var bookings = bookingService.GetAll().Where(x => x.Status == 2);
            if (!string.IsNullOrEmpty(searchString))
            {
                bookings = bookings.Where(s => s.Id.ToString().Contains(searchString));
            }
            ViewBag.SearchString = searchString;
            bookings = bookings.OrderBy(x => x.CheckIn).ToPagedList(page, pageSize);
            return View(bookings);
        }

        public ActionResult Detail(int Id)
        {
            var booking = this.bookingService.GetBookingById(Id);
            return View(booking);
        }

        public ActionResult CheckIn(int Id)
        {
            var response = this.bookingService.CheckIn(Id);
            if (response.IsSuccessed)
            {
                TempData["Message"] = "Nhận phòng thành công đơn đặt phòng mã " + Id;
                return RedirectToAction("Index", "CheckInBooking");
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            return View();
        }

        public ActionResult Cancel(int Id)
        {
            var response = this.bookingService.Cancel(Id);
            if (response.IsSuccessed)
            {
                TempData["Message"] = "Hủy thành công đơn đặt phòng mã " + Id;
                return RedirectToAction("Index", "CancelBooking");
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            return View();
        }
    }
}