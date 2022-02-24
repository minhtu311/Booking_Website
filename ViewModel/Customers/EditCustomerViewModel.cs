using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Customers
{
    public class EditCustomerViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Họ tên không được để trống")]
        public string CustomerName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email không được để trống")]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        public string Phone { get; set; }

        [Display(Name = "Giới tính")]
        public bool Gender { get; set; }
    }
}
