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
        [MaxLength(30, ErrorMessage = "Vui lòng nhập tên sách từ 1-30 kí tự.")]
        [MinLength(1, ErrorMessage = "Vui lòng nhập tên sách từ 1-30 kí tự.")]
        //[Range]
        [Display(Name = "Tên Sách")]
        [StringLength(32)]
        public string title { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Tác Giả")]
        [MaxLength(20, ErrorMessage = "Vui lòng nhập tên tác giả từ 1-20 kí tự.")]
        [MinLength(1, ErrorMessage = "Vui lòng nhập tên tác giả từ 1-20 kí tự.")]
        [Display(Name = "Tác Giả")]
        [StringLength(24)]
        public string author { get; set; }

        [Display(Name = "Hình Ảnh")]
        [Required(ErrorMessage = "Vui lòng chèn Hình Ảnh")]
        public string image { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Nhà Sản Xuất")]
        [MaxLength(30, ErrorMessage = "Vui lòng nhập tên nhà sản xuất từ 1-30 kí tự.")]
        [MinLength(1, ErrorMessage = "Vui lòng nhập tên nhà sản xuất từ 1-30 kí tự.")]
        [Display(Name = "Nhà Sản Xuất")]
        [StringLength(32)]
        public string publisher { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Năm Sản Xuất")]
        //Năm sản xuất bé hơn ngày hiện tại
        [DataType(DataType.Date)]
        [Display(Name = "Năm Sản Xuất")]
        [Column(TypeName = "Date")]

        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime publishingYear { get; set; }

        [Display(Name = "Tóm Tắt")]
        [Required(ErrorMessage = "Vui lòng nhập tóm tắt cho sách này")]
        public string summary { get; set; }

        [Display(Name = "Số Lượng")]
        [MinLength(0, ErrorMessage = "Vui lòng nhập số lượng không âm cho sách.")]

        public int count { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày Nhập")]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "Date")]
        public DateTime addDate { get; set; }

        [Display(Name = "Thể Loại")]
        [Required(ErrorMessage = "Vui lòng chọn thể loại")]
        public int categoryID { get; set; }
        [ForeignKey("categoryID")]
        public Category? category { get; set; }

        public ICollection<LBook> lBooks { get; set; } = new List<LBook>();

    }
}
