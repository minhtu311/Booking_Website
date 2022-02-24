using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebDatPhong.Service.Bookings;
using WebDatPhong.Service.Contacts;

namespace WebDatPhong.Web.Areas.Admin.Controllers
{
    public class DataboardController : Controller
    {
        private readonly IBookingService bookingService;
        private readonly IContactService contactService;
        public DataboardController(IBookingService bookingService, IContactService contactService)
        {
            this.bookingService = bookingService;
            this.contactService = contactService;
        }

        // GET: Admin/Databoard
        public ActionResult Index()
        {
            if (Session["RoleId"] == null)
            {
                return Redirect("/Admin/User/Login");
            }

            var bookingUnconfirmed = bookingService.GetAll().Where(x => x.Status == 1).Count();
            ViewBag.BookingUnconfirmed = bookingUnconfirmed;

            var bookingConfirmed = bookingService.GetAll().Where(x => x.Status == 2 && x.CheckIn.Date>=DateTime.Now.Date).Count();
            ViewBag.BookingConfirmed = bookingConfirmed;

            decimal sumMonth = 0;
            var listRoomMonth = bookingService.StatisticsRoomByMonth(DateTime.Now.Month, DateTime.Now.Year);
            foreach (var item in listRoomMonth)
            {
                sumMonth += item.Money;
            }
            ViewBag.Sum = sumMonth;

            var contact = contactService.GetAll().Where(x => x.Status == false).Count();
            ViewBag.Contact = contact;

            return View();
        }

        public ActionResult StatisticsByYear(int? Year)
        {
            decimal sum = 0;
            if (Year != null)
            {
                //tháng
                ViewBag.TestMonth = bookingService.StatisticsMonthByYear((int)Year);
                var list = bookingService.StatisticsMonthByYear((int)Year);
                foreach (var item in list)
                {
                    sum += item;
                }
                ViewBag.SearchString = Year;
                ViewBag.Sum = sum;

                //phòng
                var listRoom = bookingService.StatisticsRoomByYear((int)Year);
                List<string> listRoomName = new List<string>();
                foreach (var item in listRoom)
                {
                    string roomName = item.RoomName;
                    listRoomName.Add(roomName);
                }
                ViewBag.ListRoomName = listRoomName;
                List<decimal> listMoney = new List<decimal>();
                foreach (var item in listRoom)
                {
                    decimal tien = item.Money;
                    listMoney.Add(tien);
                }
                ViewBag.ListMoney = listMoney;

                //loại phòng
                var listRoomType = bookingService.StatisticsRoomTypeByYear((int)Year);
                List<string> listRoomTypeName = new List<string>();
                foreach (var item in listRoomType)
                {
                    string roomTypeName = item.RoomTypeName;
                    listRoomTypeName.Add(roomTypeName);
                }
                ViewBag.ListRoomTypeName = listRoomTypeName;
                List<decimal> listRoomTypeMoney = new List<decimal>();
                foreach (var item in listRoomType)
                {
                    decimal tien = item.Money;
                    listRoomTypeMoney.Add(tien);
                }
                ViewBag.ListRoomTypeMoney = listRoomTypeMoney;
            }
            else
            {
                //tháng
                ViewBag.TestMonth = bookingService.StatisticsMonthByYear(DateTime.Now.Year);
                var list = bookingService.StatisticsMonthByYear(DateTime.Now.Year);
                foreach (var item in list)
                {
                    sum += item;
                }
                ViewBag.SearchString = DateTime.Now.Year;
                ViewBag.Sum = sum;

                //phòng
                var listRoom = bookingService.StatisticsRoomByYear(DateTime.Now.Year);
                List<string> listRoomName = new List<string>();
                foreach (var item in listRoom)
                {
                    string roomName = item.RoomName;
                    listRoomName.Add(roomName);
                }
                ViewBag.ListRoomName = listRoomName;
                List<decimal> listMoney = new List<decimal>();
                foreach (var item in listRoom)
                {
                    decimal tien = item.Money;
                    listMoney.Add(tien);
                }
                ViewBag.ListMoney = listMoney;

                //loại phòng
                var listRoomType = bookingService.StatisticsRoomTypeByYear(DateTime.Now.Year);
                List<string> listRoomTypeName = new List<string>();
                foreach (var item in listRoomType)
                {
                    string roomTypeName = item.RoomTypeName;
                    listRoomTypeName.Add(roomTypeName);
                }
                ViewBag.ListRoomTypeName = listRoomTypeName;
                List<decimal> listRoomTypeMoney = new List<decimal>();
                foreach (var item in listRoomType)
                {
                    decimal tien = item.Money;
                    listRoomTypeMoney.Add(tien);
                }
                ViewBag.ListRoomTypeMoney = listRoomTypeMoney;
            }

            return View();
        }

        public ActionResult StatisticsByMonth(int? Month, int? Year)
        {
            decimal sumMonth = 0;
            if (Month == null && Year == null)
            {
                ViewBag.MonthYear = DateTime.Now.Month + "/" + DateTime.Now.Year;
                //phòng
                var listRoomMonth = bookingService.StatisticsRoomByMonth(DateTime.Now.Month, DateTime.Now.Year);
                List<string> listRoomNameMonth = new List<string>();
                foreach (var item in listRoomMonth)
                {
                    string roomName = item.RoomName;
                    listRoomNameMonth.Add(roomName);
                }
                ViewBag.ListRoomNameMonth = listRoomNameMonth;
                List<decimal> listMoneyMonth = new List<decimal>();
                foreach (var item in listRoomMonth)
                {
                    decimal tien = item.Money;
                    listMoneyMonth.Add(tien);
                    sumMonth += tien;
                }
                ViewBag.ListMoneyMonth = listMoneyMonth;
                ViewBag.Sum = sumMonth;

                //loại phòng
                var listRoomTypeMonth = bookingService.StatisticsRoomTypeByMonth(DateTime.Now.Month, DateTime.Now.Year);
                List<string> listRoomTypeNameMonth = new List<string>();
                foreach (var item in listRoomTypeMonth)
                {
                    string roomTypeNameMonth = item.RoomTypeName;
                    listRoomTypeNameMonth.Add(roomTypeNameMonth);
                }
                ViewBag.ListRoomTypeNameMonth = listRoomTypeNameMonth;
                List<decimal> listRoomTypeMoneyMonth = new List<decimal>();
                foreach (var item in listRoomTypeMonth)
                {
                    decimal tien = item.Money;
                    listRoomTypeMoneyMonth.Add(tien);
                }
                ViewBag.ListRoomTypeMoneyMonth = listRoomTypeMoneyMonth;
            }
            else
            {
                ViewBag.MonthYear = Month + "/" + Year;
                //phòng
                var listRoomMonth = bookingService.StatisticsRoomByMonth((int)Month, (int)Year);
                List<string> listRoomNameMonth = new List<string>();
                foreach (var item in listRoomMonth)
                {
                    string roomName = item.RoomName;
                    listRoomNameMonth.Add(roomName);
                }
                ViewBag.ListRoomNameMonth = listRoomNameMonth;
                List<decimal> listMoneyMonth = new List<decimal>();
                foreach (var item in listRoomMonth)
                {
                    decimal tien = item.Money;
                    listMoneyMonth.Add(tien);
                    sumMonth += tien;
                }
                ViewBag.ListMoneyMonth = listMoneyMonth;
                ViewBag.Sum = sumMonth;

                //loại phòng
                var listRoomTypeMonth = bookingService.StatisticsRoomTypeByMonth((int)Month, (int)Year);
                List<string> listRoomTypeNameMonth = new List<string>();
                foreach (var item in listRoomTypeMonth)
                {
                    string roomTypeNameMonth = item.RoomTypeName;
                    listRoomTypeNameMonth.Add(roomTypeNameMonth);
                }
                ViewBag.ListRoomTypeNameMonth = listRoomTypeNameMonth;
                List<decimal> listRoomTypeMoneyMonth = new List<decimal>();
                foreach (var item in listRoomTypeMonth)
                {
                    decimal tien = item.Money;
                    listRoomTypeMoneyMonth.Add(tien);
                }
                ViewBag.ListRoomTypeMoneyMonth = listRoomTypeMoneyMonth;
            }
            return View();
        }
    }
}