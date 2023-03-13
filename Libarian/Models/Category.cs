using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Libarian.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int categoryID { get; set; }
        [Display(Name = "Thể Loại")]
        [StringLength(50)]
        public string nameCategory { get; set; }

        public List<Book> book { get; set; }

       
    }
}
