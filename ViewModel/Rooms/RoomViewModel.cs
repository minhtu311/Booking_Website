using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Rooms
{
    public class RoomViewModel
    {
        [Display(Name = "Mã phòng")]
        public int Id { get; set; }

        [Display(Name = "Tên phòng")]
        [Required(ErrorMessage = "Tên phòng không được để trống")]
        public string RoomName { get; set; }

        [Display(Name = "Loại phòng")]
        public int RoomTypeId { get; set; }

        [Display(Name = "Loại giường")]
        public int BedId { get; set; }

        [Display(Name = "Số lượng giường")]
        [Required(ErrorMessage = "Số lượng giường không được để trống")]
        public int NumberBed { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Số lượng phòng")]
        [Required(ErrorMessage = "Số lượng phòng không được để trống")]
        public int NumberRoom { get; set; }

        [Display(Name = "Diện tích")]
        [Required(ErrorMessage = "Diện tích không được để trống")]
        public int Area { get; set; }

        [Display(Name = "Số lượng người tối đa")]
        [Required(ErrorMessage = "Số lượng người tối đa không được để trống")]
        public int NumberPerson { get; set; }

        [Display(Name = "Giá tiền")]
        [Required(ErrorMessage = "Giá tiền không được để trống")]
        public decimal Cost { get; set; }

        [Display(Name = "Trạng thái hiển thị")]
        public bool Status { get; set; }

        [Display(Name = "Ảnh")]
        public string Image { get; set; }

        public string Convenients { get; set; }
    }
}
