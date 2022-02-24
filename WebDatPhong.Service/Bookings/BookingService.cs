using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Results;
using ViewModel.Rooms;
using ViewModel.Statistics;
using WebDatPhong.Data.Infrastructures;
using WebDatPhong.Model.Models;

namespace WebDatPhong.Service.Bookings
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork unitOfWork;
        public BookingService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ResponseResult Cancel(int Id)
        {
            try
            {
                var booking = GetBookingById(Id);
                booking.Status = 0;
                this.unitOfWork.BookingRepository.Update(booking);
                this.unitOfWork.SaveChange();
                return new ResponseResult();
            }
            catch (Exception ex)
            {
                return new ResponseResult(ex.Message);
            }
        }

        public ResponseResult CheckIn(int Id)
        {
            try
            {
                var booking = GetBookingById(Id);
                booking.Status = 3;
                this.unitOfWork.BookingRepository.Update(booking);
                this.unitOfWork.SaveChange();
                return new ResponseResult();
            }
            catch (Exception ex)
            {
                return new ResponseResult(ex.Message);
            }
        }

        public ResponseResult Confirm(int Id)
        {
            try
            {
                var booking = GetBookingById(Id);
                booking.Status = 2;
                this.unitOfWork.BookingRepository.Update(booking);
                this.unitOfWork.SaveChange();
                return new ResponseResult();
            }
            catch (Exception ex)
            {
                return new ResponseResult(ex.Message);
            }
        }

        public ResponseResult Create(List<RoomBookingViewModel> listRoom, DateTime checkIn, DateTime checkOut, string Note, int NumberPerson, int CustomerId/*, int BookingId*/)
        {
            try
            {
                foreach (var item in listRoom)
                {
                    var room = this.unitOfWork.RoomRepository.SearchRoom(item.Id, checkIn, checkOut);
                    var NumberEmptyRoom = this.unitOfWork.RoomRepository.GetById(item.Id).NumberRoom - room;
                    if (NumberEmptyRoom < item.NumberRoomBooking)
                    {
                        throw new Exception("Rất xin lỗi quý khách! Hiện tại số lượng phòng mà quý khách đã chọn không đủ để đặt. Quý khách vui lòng chọn lại đơn đặt phòng. ");
                    }
                }
                decimal CostBooking = 0;
                var listBookingDetail = new List<BookingDetail>();
                foreach (var item in listRoom)
                {
                    var bookingDetail = new BookingDetail
                    {
                        RoomId = item.Id,
                        //BookingId = BookingId,
                        NumberRoomBooking = item.NumberRoomBooking,
                        RoomCost = item.CostRoom,
                        Cost = item.Cost
                    };
                    CostBooking += item.Cost;
                    listBookingDetail.Add(bookingDetail);
                    //this.unitOfWork.bookingDetailRepository.Add(bookingDetail);
                    //this.unitOfWork.SaveChange();
                }

                
                var Booking = new Booking
                {
                    //Id = BookingId,
                    CustomerId = CustomerId,
                    BookingDate = DateTime.Now,
                    CheckIn = checkIn,
                    CheckOut = checkOut,
                    BookingCost = CostBooking,
                    Status = 1,
                    NumberPerson = NumberPerson,
                    Note = Note,
                    BookingDetails = listBookingDetail
                };
                this.unitOfWork.BookingRepository.Add(Booking);
                this.unitOfWork.SaveChange();
                return new ResponseResult();
            }
            catch (Exception ex)
            {

                return new ResponseResult(ex.Message);
            }
        }

        public IEnumerable<Booking> GetAll()
        {
            return this.unitOfWork.BookingRepository.GetAll();
        }

        public Booking GetBookingById(int Id)
        {
            return this.unitOfWork.BookingRepository.GetById(Id);
        }

        public IEnumerable<decimal> StatisticsMonthByYear(int Year)
        {
            var result = new List<decimal>();
            for (int i = 1; i <= 12; i++)
            {
                decimal a = this.unitOfWork.BookingRepository.StatisticsMonthByYear(i, Year);
                result.Add(a);
            }
            return result;
        }

        public IEnumerable<StatisticsRoomViewModel> StatisticsRoomByMonth(int Month, int Year)
        {
            var result = new List<StatisticsRoomViewModel>();
            var listRoom = this.unitOfWork.RoomRepository.GetAll();
            foreach (var item in listRoom)
            {
                var room = new StatisticsRoomViewModel
                {
                    RoomName = item.RoomName,
                    Money = this.unitOfWork.BookingRepository.StatisticsRoomByMonth(item.Id,Month,Year)
                };
                result.Add(room);
            }
            return result;
        }

        public IEnumerable<StatisticsRoomViewModel> StatisticsRoomByYear(int Year)
        {
            var result = new List<StatisticsRoomViewModel>();
            var listRoom = this.unitOfWork.RoomRepository.GetAll();
            foreach (var item in listRoom)
            {
                var room = new StatisticsRoomViewModel
                {
                    RoomName = item.RoomName,
                    Money = this.unitOfWork.BookingRepository.StatisticsRoomByYear(item.Id, Year)
                };
                result.Add(room);
            }
            return result;
        }

        public IEnumerable<StatisticsRoomTypeViewModel> StatisticsRoomTypeByMonth(int Month, int Year)
        {
            var result = new List<StatisticsRoomTypeViewModel>();
            var listRoomType = this.unitOfWork.RoomTypeRepository.GetAll();
            foreach (var item in listRoomType)
            {
                var roomType = new StatisticsRoomTypeViewModel
                {
                    RoomTypeName = item.Name,
                    Money = this.unitOfWork.BookingRepository.StatisticsRoomTypeByMonth(item.Id,Month,Year)
                };
                result.Add(roomType);
            }
            return result;
        }

        public IEnumerable<StatisticsRoomTypeViewModel> StatisticsRoomTypeByYear(int Year)
        {
            var result = new List<StatisticsRoomTypeViewModel>();
            var listRoomType = this.unitOfWork.RoomTypeRepository.GetAll();
            foreach (var item in listRoomType)
            {
                var roomType = new StatisticsRoomTypeViewModel
                {
                    RoomTypeName = item.Name,
                    Money = this.unitOfWork.BookingRepository.StatisticsRoomTypeByYear(item.Id, Year)
                };
                result.Add(roomType);
            }
            return result;
        }
    }
}
