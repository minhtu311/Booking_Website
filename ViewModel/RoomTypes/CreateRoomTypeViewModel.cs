using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ViewModel.RoomTypes
{
    public class CreateRoomTypeViewModel
    {
        [Display(Name = "Tên loại phòng")]
        [Required(ErrorMessage = "Tên loại phòng không được để trống")]
        public string Name { get; set; }

        [Display(Name = "Hình ảnh")]
        //[Required(ErrorMessage = "Hình ảnh không được để trống")]
        public string Image { get; set; }

        [Display(Name = "Mô tả")]
        [Required(ErrorMessage = "Mô tả không được để trống")]
        public string Detail { get; set; }
    }
}
