using System.ComponentModel.DataAnnotations;

namespace Librarian.Models
{
	public class MLoginModel
	{
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Vui lòng nhập username")]

        public string Username { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string Password { get; set; }
	}
}
