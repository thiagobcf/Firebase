using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models.Domain
{
    public class Producto
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Required]
        [Column(TypeName = "REAL")]
        public decimal Price { get; set; }
    }
}
