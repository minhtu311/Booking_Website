using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ViewModel.Sliders
{
    public class SliderViewModel
    {
        [Display(Name = "Mã slider")]
        public int Id { get; set; }

        [Display(Name = "Tên slider")]
        [Required(ErrorMessage = "Tên slider không được để trống")]
        public string Name { get; set; }

        [Display(Name = "Ảnh")]
        public string Image { get; set; }
    }
}
