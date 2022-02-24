using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDatPhong.Model.Models;

namespace WebDatPhong.Data
{
    public class WebDatPhongDbContext : DbContext
    {
        public WebDatPhongDbContext() : base("name= webDatPhongConnection")
        {
            Database.SetInitializer<WebDatPhongDbContext>(new CreateDatabaseIfNotExists<WebDatPhongDbContext>());
        }

        public DbSet<Bed> Beds { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingDetail> BookingDetails { get; set; }
        public DbSet<Convenient> Convenients { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomConvenient> RoomConvenients { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<ServiceNews> ServiceNews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Intro> Intros { get; set; }
        public DbSet<Contact> Contacts { get; set; }

    }
}
