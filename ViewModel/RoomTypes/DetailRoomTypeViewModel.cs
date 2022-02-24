using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ViewModel.RoomTypes
{
    public class DetailRoomTypeViewModel
    {
        [Display(Name = "Mã loại phòng")]
        public int Id { get; set; }

        [Display(Name = "Tên loại phòng")]
        [Required(ErrorMessage = "Tên loại phòng không được để trống")]
        public string Name { get; set; }

        [Display(Name = "Tình trạng hiển thị")]
        public bool Status { get; set; }

        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }

        [Display(Name = "Mô tả")]
        public string Detail { get; set; }
    }
}
