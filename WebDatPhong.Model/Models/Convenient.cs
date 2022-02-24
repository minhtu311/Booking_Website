using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDatPhong.Model.Models
{
    [Table("Convenients")]
    public class Convenient
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<RoomConvenient> RoomConvenients { get; set; }
    }
}
