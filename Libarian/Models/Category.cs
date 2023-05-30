using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Librarian.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int categoryID { get; set; }

        [Required(ErrorMessage ="Vui Lòng Nhập Tên Thể Loại")]
        [MaxLength(50, ErrorMessage = "Vui lòng nhập tên Thể Loại từ 1-50 kí tự.")]
        [MinLength(1, ErrorMessage = "Vui lòng nhập tên Thể Loại từ 1-50 kí tự.")]
        [Display(Name = "Thể Loại")]
        [StringLength(50)]
        public string nameCategory { get; set; }
        public ICollection<Book> books { get; set; } = new List<Book>();

    }
}
