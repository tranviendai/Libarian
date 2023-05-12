using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Librarian.Models
{
    public class CallCard
    {
        [Key]
        [StringLength(5)]
        [Display(Name = "Mã Mượn Sách")]
        public string callCardID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int callCardIndex { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày mượn")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày Mượn Sách")]
        [Column(TypeName = "Date")]

        [DisplayFormat(DataFormatString = "{0:dd/M/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime startDate { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn Hạn trả sách")]
        [DataType(DataType.Date)]
        [Display(Name = "Hạn Trả Sách")]
        [Column(TypeName = "Date")]

        [DisplayFormat(DataFormatString = "{0:dd/M/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime deadline { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày trả Sách")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd/M/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? endDate { get; set; }

        [Display(Name = "Tình trạng sách")]
        [StringLength(24)]
        public string bookStatus { get; set; }

        [Display(Name = "Số Thẻ")]
        public string libraryCardID { get; set; }
        [ForeignKey("libraryCardID")]
        public LibraryCard? libaryCard { get; set; }

        public string lBookID { get; set; }
        [ForeignKey("lBookID")]
        public LBook? lBook { get; set; }
    }
}
