using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Librarian.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Họ Tên")]
        [StringLength(42)]
        [Required(ErrorMessage = "Vui lòng nhập Họ Tên")]
        [MaxLength(40, ErrorMessage = "Vui lòng nhập tên thủ thư từ 1-40 kí tự.")]
        [MinLength(1, ErrorMessage = "Vui lòng nhập tên thủ thư từ 1-40 kí tự.")]
        public string fullName { get; set; }


        [Display(Name = "Giới Tính")]
        [StringLength(4)]
        public string sex { get; set; }

        [StringLength(100)]
        [Display(Name = "Địa Chỉ")]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        [MaxLength(100, ErrorMessage = "Vui lòng nhập địa chỉ từ 1-100 kí tự.")]
        [MinLength(1, ErrorMessage = "Vui lòng nhập địa chỉ từ 1-100 kí tự.")]
        public string address { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày sinh")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày Sinh")]
        [DisplayFormat(DataFormatString = "{0:dd/M/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime birthday { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/M/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngày Tham Gia")]
        public DateTime startProfile { get; set; }

    }
}
