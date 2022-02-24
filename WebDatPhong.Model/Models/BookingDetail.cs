using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDatPhong.Model.Models
{
    [Table("BookingDetail")]
    public class BookingDetail
    {
        [Key]
        [Column(Order =0)]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RoomId { get; set; }
        [Key]
        [Column(Order =1)]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BookingId { get; set; }
        public int NumberRoomBooking { get; set; }

        public decimal RoomCost { get; set; }
        public decimal Cost { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }

        [ForeignKey("BookingId")]
        public virtual Booking Booking { get; set; }
    }
}
