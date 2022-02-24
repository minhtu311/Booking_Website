using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDatPhong.Model.Models
{
    [Table("RoomTypes")]
    public class RoomType
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Image { get; set; }
        public string Detail { get; set; }

        public bool Status { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
