using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDatPhong.Data.Infrastructures;
using WebDatPhong.Data.IRepositories;
using WebDatPhong.Model.Models;

namespace WebDatPhong.Data.Repository
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(WebDatPhongDbContext context) : base(context)
        {
        }

        public IEnumerable<Booking> GetBookingByCustomerId(int CustomerId)
        {
            var list = context.Bookings.Where(s => s.CustomerId == CustomerId).ToList();
            return list;
        }

        public decimal StatisticsMonthByYear(int Month, int Year)
        {
            DateTime startDay = new DateTime(Year, Month, 1);
            DateTime endDay = startDay.AddMonths(1).AddDays(-1);

            var list = from Booking in context.Bookings
                         where Booking.Status == 3
                         && Booking.CheckIn >= startDay
                         && Booking.CheckIn <= endDay
                         select Booking;

            //int a = 1; 
            decimal count = 0;
            if (list.Count() == 0) count = 0;
            else count = list.Sum(s => s.BookingCost);
            
            return count;
        }

        public decimal StatisticsRoomByMonth(int Id, int Month, int Year)
        {
            DateTime startDay = new DateTime(Year, Month, 1);
            DateTime endDay = startDay.AddMonths(1).AddDays(-1);
            var list = (from BookingDetail in context.BookingDetails
                        join Booking in context.Bookings
                        on BookingDetail.BookingId equals Booking.Id
                        where BookingDetail.RoomId == Id
                        && Booking.Status == 3
                        && Booking.CheckIn >= startDay
                        && Booking.CheckIn <= endDay
                        select BookingDetail);
            decimal count = 0;
            if (list.Count() == 0) count = 0;
            else count = list.Sum(s => s.Cost);
            return count;
        }

        public decimal StatisticsRoomByYear(int Id, int Year)
        {
            DateTime startDay = new DateTime(Year,1,1);
            DateTime endDay = new DateTime(Year,12,31);
            var list = (from BookingDetail in context.BookingDetails
                         join Booking in context.Bookings
                         on BookingDetail.BookingId equals Booking.Id
                         where BookingDetail.RoomId == Id
                         && Booking.Status == 3
                         && Booking.CheckIn >= startDay
                         && Booking.CheckIn <= endDay
                         select BookingDetail);
            decimal count = 0;
            if (list.Count() == 0) count = 0;
            else count = list.Sum(s => s.Cost);
            return count;
        }

        public decimal StatisticsRoomTypeByMonth(int Id, int Month, int Year)
        {
            DateTime startDay = new DateTime(Year, Month, 1);
            DateTime endDay = startDay.AddMonths(1).AddDays(-1);
            var list = (from BookingDetail in context.BookingDetails
                        join Booking in context.Bookings
                        on BookingDetail.BookingId equals Booking.Id
                        join Room in context.Rooms
                        on BookingDetail.RoomId equals Room.Id
                        join RoomType in context.RoomTypes
                        on Room.RoomTypeId equals RoomType.Id
                        where RoomType.Id == Id
                        && Booking.Status == 3
                        && Booking.CheckIn >= startDay
                        && Booking.CheckIn <= endDay
                        select BookingDetail);
            decimal count = 0;
            if (list.Count() == 0) count = 0;
            else count = list.Sum(s => s.Cost);
            return count;
        }

        public decimal StatisticsRoomTypeByYear(int Id, int Year)
        {
            DateTime startDay = new DateTime(Year, 1, 1);
            DateTime endDay = new DateTime(Year, 12, 31);
            var list = (from BookingDetail in context.BookingDetails
                        join Booking in context.Bookings
                        on BookingDetail.BookingId equals Booking.Id
                        join Room in context.Rooms
                        on BookingDetail.RoomId equals Room.Id
                        join RoomType in context.RoomTypes
                        on Room.RoomTypeId equals RoomType.Id
                        where RoomType.Id == Id
                        && Booking.Status == 3
                        && Booking.CheckIn >= startDay
                        && Booking.CheckIn <= endDay
                        select BookingDetail);
            decimal count = 0;
            if (list.Count() == 0) count = 0;
            else count = list.Sum(s => s.Cost);
            return count;
        }
    }
}
