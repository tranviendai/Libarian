using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Librarian.Models
{
    public class LBook
    {
        [Key]
        [Display(Name = "Mã Đầu Sách")]
        [StringLength(10)]
        public string lBookID { get; set; }

        [Display(Name = "Mã Sách")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int lBookIndex { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Tình Trạng")]
        [Display(Name = "Tình Trạng")]
        [StringLength(12)]
        public string status { get; set; }

        [Required(ErrorMessage ="Nhập Ghi Chú")]
        [Display(Name = "Ghi Chú")]
        public string note { get; set; }

        public string bookID { get; set; }
        [ForeignKey("bookID")]
        public Book Books;

    }
}
