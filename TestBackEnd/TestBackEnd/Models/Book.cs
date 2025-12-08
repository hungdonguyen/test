using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestBackEnd.Models
{
    [Table("Books")]
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

      [Required]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        [Required]
   [MaxLength(150)]
     public string Author { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Required]
 public int GenreId { get; set; }

      [Required]
    public int StockQuantity { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
