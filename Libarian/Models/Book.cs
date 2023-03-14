using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Librarian.Models
{
    public class Book
    {
        [Key]
        [Display(Name = "Mã Sách")]
        [StringLength(5)]
        public string bookID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int bookIndex { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Tên Sách")]
        [Display(Name = "Tên Sách")]
        [StringLength(32)]
        public string title { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Tác Giả")]
        [Display(Name = "Tác Giả")]
        [StringLength(24)]
        public string author { get; set; }

        [Display(Name = "Hình Ảnh")]
        [Required(ErrorMessage = "Vui lòng chèn Hình Ảnh")]
        public string image { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Nhà Sản Xuất")]
        [Display(Name = "Nhà Sản Xuất")]
        [StringLength(32)]
        public string publisher { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Năm Sản Xuất")]
        [DataType(DataType.Date)]
        [Display(Name = "Năm Sản Xuất")]
        [Column(TypeName = "Date")]

        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime publishingYear { get; set; }

        [Display(Name = "Tóm Tắt")]
        [Required(ErrorMessage = "Vui lòng nhập tóm tắt cho sách này")]
        public string summary { get; set; }

        [Display(Name = "Số Lượng")]
        public int count { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày Nhập")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "Date")]
        public DateTime addDate { get; set; }

        [Display(Name = "Thể Loại")]
        public int categoryID { get; set; }
        [ForeignKey("categoryID")]
        public Category category;

        public ICollection<LBook> LBooks;

    }
}
