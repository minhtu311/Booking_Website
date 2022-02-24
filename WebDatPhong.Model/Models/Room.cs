using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDatPhong.Model.Models
{
    [Table("Rooms")]
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public int RoomTypeId { get; set; }
        public int BedId { get; set; }
        public int NumberBed { get; set; }
        public string RoomName { get; set; }
        public string Description { get; set; }
        public int NumberRoom { get; set; }
        public int Area { get; set; }
        public int NumberPerson { get; set; }
        public decimal Cost { get; set; }
        public bool Status { get; set; }
        public string Image { get; set; }

        [ForeignKey("RoomTypeId")]
        public virtual RoomType RoomType { get; set; }

        [ForeignKey("BedId")]
        public virtual Bed Bed { get; set; }


        public virtual ICollection<RoomConvenient> RoomConvenients { get; set; }
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
    }
}
