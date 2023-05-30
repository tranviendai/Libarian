using System.ComponentModel.DataAnnotations;

namespace Librarian.Models
{
    public class MSignUpModel: ApplicationUser
    {
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MinLength(6, ErrorMessage = "Mật khẩu dài ít nhất 6 kí tự")]
        public string Password {get; set; }
    }
}
