using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDatPhong.Data.IRepositories;

namespace WebDatPhong.Data.Infrastructures
{
    public interface IUnitOfWork:IDisposable
    {
        IBedRepository BedRepository { get; }
        IBookingRepository BookingRepository { get; }
        IConvenientRepository ConvenientRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IRoomRepository RoomRepository { get; }
        IRoomTypeRepository RoomTypeRepository { get; }
        ISliderRepository SliderRepository { get; }
        IRoomConvenientRepository RoomConvenientRepository { get; }
        IServiceRepository ServiceRepository { get; }
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        IIntroRepository IntroRepository { get; }
        IContactRepository ContactRepository { get; }
        int SaveChange();
        Task<int> SaveChangesAsync();
    }
}
