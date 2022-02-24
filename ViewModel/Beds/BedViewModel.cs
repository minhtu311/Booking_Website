using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ViewModel.Beds
{
    public class BedViewModel
    {
        [Display(Name = "Mã loại giường")]
        public int Id { get; set; }

        [Display(Name = "Tên loại giường")]
        [Required(ErrorMessage = "Tên loại giường không được để trống")]
        public string Name { get; set; }
    }
}
