using PagedList;
using System.Linq;
using System.Web.Mvc;
using WebDatPhong.Service.Bookings;

namespace WebDatPhong.Web.Areas.Admin.Controllers
{
    public class CheckInBookingController : Controller
    {
        private readonly IBookingService bookingService;

        public CheckInBookingController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        // GET: Admin/CheckInBooking
        public ActionResult Index(string searchString, int page = 1, int pageSize = 8)
        {
            if (Session["RoleId"] == null)
            {
                return Redirect("/Admin/User/Login");
            }
            var bookings = bookingService.GetAll().Where(x => x.Status == 3);
            if (!string.IsNullOrEmpty(searchString))
            {
                bookings = bookings.Where(s => s.Id.ToString().Contains(searchString));
            }
            ViewBag.SearchString = searchString;
            bookings = bookings.OrderByDescending(x => x.CheckIn).ToPagedList(page, pageSize);
            return View(bookings);
        }

        public ActionResult Detail(int Id)
        {
            var booking = this.bookingService.GetBookingById(Id);
            return View(booking);
        }
    }
}