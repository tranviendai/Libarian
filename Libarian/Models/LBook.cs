using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Libarian.Models
{
    public class LBook
    {
        [Key]
        [Display(Name = "Mã Đầu Sách")]
        [MaxLength(8)]
        public string lBookID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int lBookIndex { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Tình Trạng")]
        [Display(Name = "Tình Trạng")]
        [StringLength(12)]
        public string status { get; set; }

        [Display(Name = "Ghi Chú")]
        public string note { get; set; }

        [Display(Name = "Mã Sách")]
        public string bookID { get; set; }
        [ForeignKey("bookID")]
        public Book book { get; set; }
    }
}
