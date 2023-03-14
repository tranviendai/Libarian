using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Librarian.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Họ Tên")]
        [StringLength(42)]
        [Required(ErrorMessage = "Vui lòng nhập Họ Tên")]
        public string fullName { get; set; }

        [Display(Name = "Giới Tính")]
        [Required]
        [StringLength(4)]
        public string sex { get; set; }

        [StringLength(100)]
        [Display(Name = "Địa Chỉ")]
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
