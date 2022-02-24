using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDatPhong.Data.IRepositories;
using WebDatPhong.Data.Repository;

namespace WebDatPhong.Data.Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WebDatPhongDbContext context;
        private IBedRepository bedRepository;
        private IBookingRepository bookingRepository;
        private IConvenientRepository convenientRepository;
        private ICustomerRepository customerRepository;
        private IRoomRepository roomRepository;
        private IRoomTypeRepository roomTypeRepository;
        private ISliderRepository sliderRepository;
        private IRoomConvenientRepository roomConvenientRepository;
        private IServiceRepository serviceRepository;
        private IUserRepository userRepository;
        private IRoleRepository roleRepository;
        private IIntroRepository introRepository;
        private IContactRepository contactRepository;


        public UnitOfWork(WebDatPhongDbContext context)
        {
            this.context = context;
        }

        public IBedRepository BedRepository
        {
            get
            {
                if (this.bedRepository == null)
                {
                    this.bedRepository = new BedRepository(this.context);
                }
                return this.bedRepository;
            }
        }

        public IBookingRepository BookingRepository
        {
            get
            {
                if (this.bookingRepository == null)
                {
                    this.bookingRepository = new BookingRepository(this.context);
                }
                return this.bookingRepository;
            }
        }

        public IConvenientRepository ConvenientRepository
        {
            get
            {
                if (this.convenientRepository == null)
                {
                    this.convenientRepository = new ConvenientRepository(this.context);
                }
                return this.convenientRepository;
            }
        }

        public ICustomerRepository CustomerRepository
        {
            get
            {
                if (this.customerRepository == null)
                {
                    this.customerRepository = new CustomerRepository(this.context);
                }
                return this.customerRepository;
            }
        }

        public IRoomRepository RoomRepository
        {
            get
            {
                if (this.roomRepository == null)
                {
                    this.roomRepository = new RoomRepository(this.context);
                }
                return this.roomRepository;
            }
        }

        public IRoomTypeRepository RoomTypeRepository
        {
            get
            {
                if (this.roomTypeRepository == null)
                {
                    this.roomTypeRepository = new RoomTypeRepository(this.context);
                }
                return this.roomTypeRepository;
            }
        }

        public ISliderRepository SliderRepository
        {
            get
            {
                if (this.sliderRepository == null)
                {
                    this.sliderRepository = new SliderRepository(this.context);
                }
                return this.sliderRepository;
            }
        }

        public IRoomConvenientRepository RoomConvenientRepository
        {
            get
            {
                if (this.roomConvenientRepository == null)
                {
                    this.roomConvenientRepository = new RoomConvenientRepository(this.context);
                }
                return this.roomConvenientRepository;
            }
        }

        public IServiceRepository ServiceRepository
        {
            get
            {
                if (this.serviceRepository == null)
                {
                    this.serviceRepository = new ServiceRepository(this.context);
                }
                return this.serviceRepository;
            }
        }

        public IIntroRepository IntroRepository
        {
            get
            {
                if (this.introRepository == null)
                {
                    this.introRepository = new IntroRepository(this.context);
                }
                return this.introRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new UserRepository(this.context);
                }
                return this.userRepository;
            }
        }

        public IRoleRepository RoleRepository
        {
            get
            {
                if (this.roleRepository == null)
                {
                    this.roleRepository = new RoleRepository(this.context);
                }
                return this.roleRepository;
            }
        }

        public IContactRepository ContactRepository
        {
            get
            {
                if (this.contactRepository == null)
                {
                    this.contactRepository = new ContactRepository(this.context);
                }
                return this.contactRepository;
            }
        }

        public void Dispose()
        {
            this.context.Dispose();
        }

        public int SaveChange()
        {
            return this.context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this.context.SaveChangesAsync();
        }
    }
}
