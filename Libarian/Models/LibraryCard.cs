using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Librarian.Models
{
    public class LibraryCard
    {
        [Key]
        [Display(Name = "Số Thẻ")]
        [StringLength(5)]
        public string libraryCardID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int librayCardIndex { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Họ Tên")]
        [Display(Name = "Họ Tên")]
        [StringLength(24)]
        public string fullName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Tình Trạng")]
        [Display(Name = "Tình Trạng")]
        [StringLength(12)]
        public string cardStatus { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Ngày Lập Thẻ")]
        [Column(TypeName = "Date")]

        [DisplayFormat(DataFormatString = "{0:dd/M/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime startDate { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày hết hạn")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày Hết Hạn")]
        [Column(TypeName = "Date")]

        [DisplayFormat(DataFormatString = "{0:dd/M/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime expirationDate { get; set; }

        public ICollection<CallCard> callCards { get; set; } = new List<CallCard>();
    }
}
