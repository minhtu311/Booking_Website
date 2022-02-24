using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Users
{
    public class UserViewModel
    {
        [Display(Name = "Id tài khoản")]
        public int Id { get; set; }

        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string Password { get; set; }

        [Display(Name = "Tên người dùng")]
        [Required(ErrorMessage = "Tên người dùng không được để trống")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email không được để trống")]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        public string Phone { get; set; }

        [Display(Name = "Giới tính")]
        [Required(ErrorMessage = "Giới tính không được để trống")]
        public bool Gender { get; set; }

        [Display(Name = "Quyền hạn")]
        [Required(ErrorMessage = "Quyền hạn không được để trống")]
        public int RoleId { get; set; }
    }
}
