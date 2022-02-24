using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDatPhong.Model.Models
{
    [Table("RoomConvenient")]
    public class RoomConvenient
    {
        [Key]
        [Column(Order = 0)]
        public int ConvenientId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int RoomId { get; set; }

        [ForeignKey(nameof(ConvenientId))]
        public virtual Convenient Convenient { get; set; }

        [ForeignKey(nameof(RoomId))]
        public virtual Room Room { get; set; }
    }
}
