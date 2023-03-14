using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Librarian.Models
{
    public class PenaltyTicket
    {
        [Key]
        public string callCardID { get; set; }
        [ForeignKey("callCardID")]
        public CallCard callCard;

        [Display(Name = "Giá phạt")]
        [Column(TypeName = "Money")]
        public decimal price { get; set; }

        [Display(Name = "Lý do")]
        public string reason { get; set; }

        [Display(Name = "Tình trạng")]
        public bool status { get; set; }
    }
}
