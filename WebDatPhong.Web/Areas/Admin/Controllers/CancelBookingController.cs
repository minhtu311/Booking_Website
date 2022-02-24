using PagedList;
using System.Linq;
using System.Web.Mvc;
using WebDatPhong.Service.Bookings;

namespace WebDatPhong.Web.Areas.Admin.Controllers
{
    public class CancelBookingController : Controller
    {
        private readonly IBookingService bookingService;

        public CancelBookingController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        // GET: Admin/CancelBooking
        public ActionResult Index(string searchString, int page = 1, int pageSize = 8)
        {
            if (Session["RoleId"] == null)
            {
                return Redirect("/Admin/User/Login");
            }
            var bookings = bookingService.GetAll().Where(x => x.Status == 0);
            if (!string.IsNullOrEmpty(searchString))
            {
                bookings = bookings.Where(s => s.Id.ToString().Contains(searchString));
            }
            ViewBag.SearchString = searchString;
            bookings = bookings.OrderByDescending(x => x.BookingDate).ToPagedList(page, pageSize);
            return View(bookings);
        }

        public ActionResult Detail(int Id)
        {
            var booking = this.bookingService.GetBookingById(Id);
            return View(booking);
        }
    }
}