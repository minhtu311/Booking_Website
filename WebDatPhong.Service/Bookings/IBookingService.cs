using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Results;
using ViewModel.Rooms;
using ViewModel.Statistics;
using WebDatPhong.Model.Models;

namespace WebDatPhong.Service.Bookings
{
    public interface IBookingService
    {
        ResponseResult Create(List<RoomBookingViewModel> listRoom, DateTime checkIn, DateTime checkOut, string Note, int NumberPerson, int CustomerId/*, int BookingId*/);
        ResponseResult Confirm(int Id);
        ResponseResult Cancel(int Id);
        ResponseResult CheckIn(int Id);
        IEnumerable<Booking> GetAll();
        Booking GetBookingById(int Id);

        IEnumerable<decimal> StatisticsMonthByYear(int Year);
        IEnumerable<StatisticsRoomViewModel> StatisticsRoomByYear(int Year);
        IEnumerable<StatisticsRoomTypeViewModel> StatisticsRoomTypeByYear(int Year);

        IEnumerable<StatisticsRoomViewModel> StatisticsRoomByMonth(int Month,int Year);
        IEnumerable<StatisticsRoomTypeViewModel> StatisticsRoomTypeByMonth(int Month,int Year);
    }
}
