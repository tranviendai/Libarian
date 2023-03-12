using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libarian.Models
{
    public class Profile
    {
        [Key]
        public string Id { get; set; }

        [ForeignKey("Id")]
        public IdentityUser User { get; set; }

        [Display(Name ="Họ Tên")]
        [StringLength(42)]
        [Required(ErrorMessage ="Vui lòng nhập Họ Tên Thủ Thư")]
        public string name { get; set; }

        [Display(Name ="Giới Tính")]
        public bool sex { get; set; }

        [StringLength(100)]
        [Display(Name ="Địa Chỉ")]
        public string address { get; set; }

        [Required(ErrorMessage ="Vui lòng chọn ngày sinh")]
        [DataType(DataType.Date)]
        [Display(Name ="Ngày Sinh")]
        [DisplayFormat(DataFormatString = "{0:dd/M/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime birthday { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập số điện thoại")]
        [Display(Name ="Số Điện Thoại")]
        [DataType(DataType.PhoneNumber)]
        public string phone { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/M/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name ="Ngày Tham Gia")]
        public DateTime startProfile { get; set; }
    }
}
