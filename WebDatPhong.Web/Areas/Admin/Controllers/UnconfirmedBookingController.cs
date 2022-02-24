using PagedList;
using System.Linq;
using System.Web.Mvc;
using WebDatPhong.Service.Bookings;

namespace WebDatPhong.Web.Areas.Admin.Controllers
{
    public class UnconfirmedBookingController : Controller
    {
        private readonly IBookingService bookingService;

        public UnconfirmedBookingController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        // GET: Admin/UnconfirmedBooking
        public ActionResult Index(string searchString, int page = 1, int pageSize = 8)
        {
            if (Session["RoleId"] == null)
            {
                return Redirect("/Admin/User/Login");
            }

            var bookings = bookingService.GetAll().Where(x => x.Status == 1);
            if (!string.IsNullOrEmpty(searchString))
            {
                bookings = bookings.Where(s => s.Id.ToString().Contains(searchString));
            }
            ViewBag.SearchString = searchString;
            bookings = bookings.OrderBy(x => x.BookingDate).ToPagedList(page, pageSize);
            return View(bookings);
        }

        public ActionResult Detail(int Id)
        {
            var booking = this.bookingService.GetBookingById(Id);
            return View(booking);
        }

        public ActionResult Confirm(int Id)
        {
            var response = this.bookingService.Confirm(Id);
            if (response.IsSuccessed)
            {
                TempData["Message"] = "Xác nhận thành công đơn đặt phòng mã "+Id;
                return RedirectToAction("Index", "ConfirmedBooking");
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
                return RedirectToAction("Index","CancelBooking");
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            return View();
        }
    }
}