using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Contacts
{
    public class ContactViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Tên khách hàng")]
        [Required(ErrorMessage = "Tên khách hàng không được để trống")]
        public string CustomerName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email không được để trống")]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        public string Phone { get; set; }

        [Display(Name = "Tiêu đề thư")]
        [Required(ErrorMessage = "Tiêu đề thư không được để trống")]
        [MaxLength(200,ErrorMessage="Tiêu đề không quá 200 kí tự")]
        public string Subject { get; set; }

        [Display(Name = "Nội dung")]
        [Required(ErrorMessage = "Nội dung không được để trống")]
        public string Detail { get; set; }
    }
}
