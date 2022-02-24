using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ViewModel.Rooms;
using WebDatPhong.Service.Bookings;
using WebDatPhong.Service.Customers;

namespace WebDatPhong.Web.Controllers
{
    public class BookingController : Controller
    {
        private readonly ICustomerService customerService;
        private readonly IBookingService bookingService;
        public BookingController(ICustomerService customerService, IBookingService bookingService)
        {
            this.customerService = customerService;
            this.bookingService = bookingService;
        }

        // GET: Booking
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var customer = customerService.GetCustomerById((int)Session["CustomerId"]);
            ViewBag.CustonmerName = customer.CustomerName;
            ViewBag.CustomerPhone = customer.Phone;

            ViewBag.CheckIn = (DateTime)Session["CheckIn"];
            ViewBag.CheckOut = (DateTime)Session["CheckOut"];

            var cart = Session["Cart"];
            var listBookingRoom = new List<RoomBookingViewModel>();
            listBookingRoom = (List<RoomBookingViewModel>)cart;
            decimal SumCost = 0;
            foreach (var item in listBookingRoom)
            {
                SumCost += item.Cost;
            }
            ViewBag.SumCost = SumCost;
            return View(listBookingRoom);
        }

        [HttpPost]
        public ActionResult Create(string Note)
        {
            var customer = customerService.GetCustomerById((int)Session["CustomerId"]);
            ViewBag.CustonmerName = customer.CustomerName;
            ViewBag.CustomerPhone = customer.Phone;

            ViewBag.CheckIn = (DateTime)Session["CheckIn"];
            ViewBag.CheckOut = (DateTime)Session["CheckOut"];

            var cart = Session["Cart"];
            var listBookingRoom = new List<RoomBookingViewModel>();
            listBookingRoom = (List<RoomBookingViewModel>)cart;
            decimal SumCost = 0;
            foreach (var item in listBookingRoom)
            {
                SumCost += item.Cost;
            }
            ViewBag.SumCost = SumCost;

            //Random r = new Random();
            //int num;
            //do
            //{
            //    num = r.Next(10000000, 99999999);
            //} while (bookingService.GetBookingById(num) != null);

            var response = this.bookingService.Create(listBookingRoom, (DateTime)Session["CheckIn"], (DateTime)Session["CheckOut"], Note, (int)Session["BookingNumberPerson"], (int)Session["CustomerId"]/*, num*/);
            if (response.IsSuccessed)
            {
                return Redirect("/Booking/Finish");
            }
            ModelState.AddModelError(string.Empty, response.ErrorMessage);
            ViewBag.Message = response.ErrorMessage;
            return View(listBookingRoom);
        }

        public ActionResult BookingClient(int? Id, DateTime? searchString, int page = 1, int pageSize = 5)
        {
            if (Id == null)
            {
                return RedirectToAction("Login","Customer");
            }
            var listBoooking = bookingService.GetAll().Where(x => x.CustomerId == Id);
            if (searchString != null)
            {
                listBoooking = listBoooking.Where(s => s.CheckIn == searchString);
            }
            ViewBag.SearchString = searchString;
            listBoooking = listBoooking.OrderByDescending(x => x.BookingDate).ToPagedList(page, pageSize);
            return View(listBoooking);
        }

        public ActionResult Detail(int Id)
        {
            var booking = this.bookingService.GetBookingById(Id);
            return View(booking);
        }

        public ActionResult CancelBooking()
        {
            Session.Remove("CheckIn");
            Session.Remove("CheckOut");
            Session.Remove("BookingNumberPerson");
            Session.Remove("Cart");
            return Redirect("/");
        }

        public ActionResult Finish()
        {
            Session.Remove("CheckIn");
            Session.Remove("CheckOut");
            Session.Remove("BookingNumberPerson");
            Session.Remove("Cart");
            return View();
        }
    }
}