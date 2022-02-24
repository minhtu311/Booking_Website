using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ViewModel.Convenients
{
    public class ConvenientViewModel
    {
        [Display(Name = "Mã tiện ích")]
        public int Id { get; set; }

        [Display(Name = "Tên tiện ích")]
        [Required(ErrorMessage = "Tên tiện ích không được để trống")]
        public string Name { get; set; }
    }
}
