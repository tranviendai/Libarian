using System.ComponentModel.DataAnnotations;

namespace Libarian.Models
{
    public class UserRoleModel
    {
        [Display(Name ="Email")]
        public string email { get; set; }

        [Display(Name = "Họ Tên")]
        public string fullName { get; set; }

        [Display(Name = "Giới Tính")]
        public string sex { get; set; }

        [Display(Name = "Địa Chỉ")]
        public string address { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Số Điện Thoại")]

        public string phone { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày Sinh")]
        [DisplayFormat(DataFormatString = "{0:dd/M/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime birthday { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/M/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngày Tham Gia")]
        public DateTime startProfile { get; set; }

        [Display(Name ="Vai Trò")]
        public IEnumerable<string> Roles { get; set; }

    }
}
