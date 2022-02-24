using AutoMapper;
using ViewModel.Beds;
using ViewModel.Contacts;
using ViewModel.Convenients;
using ViewModel.Customers;
using ViewModel.Rooms;
using ViewModel.RoomTypes;
using ViewModel.Services;
using ViewModel.Sliders;
using ViewModel.Users;
using WebDatPhong.Model.Models;

namespace WebDatPhong.Web.App_Start
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<CreateRoomTypeViewModel, RoomType>().ReverseMap();

            CreateMap<DetailRoomTypeViewModel, RoomType>().ReverseMap();

            CreateMap<BedViewModel, Bed>().ReverseMap();

            CreateMap<ConvenientViewModel, Convenient>().ReverseMap();

            CreateMap<RoomViewModel, Room>().ReverseMap();

            CreateMap<ServiceViewModel, ServiceNews>().ReverseMap();

            CreateMap<SliderViewModel, Slider>().ReverseMap();

            CreateMap<CustomerViewModel, Customer>().ReverseMap();

            CreateMap<EditCustomerViewModel, Customer>().ReverseMap();

            CreateMap<EditCustomerViewModel, CustomerViewModel>().ReverseMap();

            CreateMap<UserViewModel, User>().ReverseMap();

            CreateMap<ContactViewModel, Contact>().ReverseMap();
        }
    }
}