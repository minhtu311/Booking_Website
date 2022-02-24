using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ViewModel.Services
{
    public class ServiceViewModel
    {
        [Display(Name = "Mã dịch vụ")]
        public int Id { get; set; }

        [Display(Name = "Tên dịch vụ")]
        [Required(ErrorMessage = "Tên dịch vụ không được để trống")]
        public string Name { get; set; }

        [Display(Name = "Tóm tắt")]
        [Required(ErrorMessage = "Tóm tắt không được để trống")]
        [MaxLength(500,ErrorMessage ="Tóm tắt không quá 500 ký tự")]
        public string Summary { get; set; }

        [Display(Name = "Chi tiết")]
        [Required(ErrorMessage = "Chi tiết không được để trống")]
        public string Detail { get; set; }

        [Display(Name = "Ảnh")]
        public string Thumbnail { get; set; }
    }
}
