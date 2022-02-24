using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDatPhong.Data.Infrastructures;
using WebDatPhong.Model.Models;

namespace WebDatPhong.Data.IRepositories
{
    public interface IBookingRepository: IGenericRepository<Booking>
    {
        IEnumerable<Booking> GetBookingByCustomerId(int CustomerId);
        decimal StatisticsRoomByYear(int Id, int Year);
        decimal StatisticsRoomTypeByYear(int Id, int Year);
        decimal StatisticsMonthByYear(int Month, int Year);
        decimal StatisticsRoomByMonth(int Id,int Month, int Year);
        decimal StatisticsRoomTypeByMonth(int Id,int Month, int Year);
    }
}
