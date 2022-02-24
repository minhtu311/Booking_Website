using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDatPhong.Model.Models
{
    public class ServiceNews
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Detail { get; set; }
        public string Thumbnail { get; set; }

    }
}
