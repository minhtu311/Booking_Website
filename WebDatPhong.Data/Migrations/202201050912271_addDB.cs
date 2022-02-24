namespace WebDatPhong.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Beds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomTypeId = c.Int(nullable: false),
                        BedId = c.Int(nullable: false),
                        NumberBed = c.Int(nullable: false),
                        RoomName = c.String(),
                        Description = c.String(),
                        NumberRoom = c.Int(nullable: false),
                        Area = c.Int(nullable: false),
                        NumberPerson = c.Int(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Boolean(nullable: false),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Beds", t => t.BedId, cascadeDelete: true)
                .ForeignKey("dbo.RoomTypes", t => t.RoomTypeId, cascadeDelete: true)
                .Index(t => t.RoomTypeId)
                .Index(t => t.BedId);
            
            CreateTable(
                "dbo.BookingDetail",
                c => new
                    {
                        RoomId = c.Int(nullable: false),
                        BookingId = c.Int(nullable: false),
                        NumberRoomBooking = c.Int(nullable: false),
                        RoomCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.RoomId, t.BookingId })
                .ForeignKey("dbo.Bookings", t => t.BookingId, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.RoomId)
                .Index(t => t.BookingId);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        BookingDate = c.DateTime(nullable: false),
                        CheckIn = c.DateTime(nullable: false),
                        CheckOut = c.DateTime(nullable: false),
                        BookingCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        NumberPerson = c.Int(nullable: false),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        CustomerName = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Gender = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoomConvenient",
                c => new
                    {
                        ConvenientId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ConvenientId, t.RoomId })
                .ForeignKey("dbo.Convenients", t => t.ConvenientId, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.ConvenientId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.Convenients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoomTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Image = c.String(),
                        Detail = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Subject = c.String(),
                        Detail = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Introes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Detail = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Gender = c.Boolean(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ServiceNews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Summary = c.String(),
                        Detail = c.String(),
                        Thumbnail = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sliders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Rooms", "RoomTypeId", "dbo.RoomTypes");
            DropForeignKey("dbo.RoomConvenient", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.RoomConvenient", "ConvenientId", "dbo.Convenients");
            DropForeignKey("dbo.BookingDetail", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Bookings", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.BookingDetail", "BookingId", "dbo.Bookings");
            DropForeignKey("dbo.Rooms", "BedId", "dbo.Beds");
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.RoomConvenient", new[] { "RoomId" });
            DropIndex("dbo.RoomConvenient", new[] { "ConvenientId" });
            DropIndex("dbo.Bookings", new[] { "CustomerId" });
            DropIndex("dbo.BookingDetail", new[] { "BookingId" });
            DropIndex("dbo.BookingDetail", new[] { "RoomId" });
            DropIndex("dbo.Rooms", new[] { "BedId" });
            DropIndex("dbo.Rooms", new[] { "RoomTypeId" });
            DropTable("dbo.Sliders");
            DropTable("dbo.ServiceNews");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Introes");
            DropTable("dbo.Contacts");
            DropTable("dbo.RoomTypes");
            DropTable("dbo.Convenients");
            DropTable("dbo.RoomConvenient");
            DropTable("dbo.Customers");
            DropTable("dbo.Bookings");
            DropTable("dbo.BookingDetail");
            DropTable("dbo.Rooms");
            DropTable("dbo.Beds");
        }
    }
}
