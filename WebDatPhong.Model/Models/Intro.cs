using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDatPhong.Model.Models
{
    public class Intro
    {
        [Key]
        public int Id { get; set; }
        public string Detail { get; set; }
    }
}
