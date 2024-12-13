using System.ComponentModel.DataAnnotations;

namespace WebData.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255, ErrorMessage = "Name cannot be longer than 256 characters")]
        public string Name { get; set; }

        [Range(0, 10000000, ErrorMessage = "Price must be between 0 - 1.000.000")]
        public float Price { get; set; }

        [Range(1, 1001, ErrorMessage = "Quantity must be between 1 and 1000.")]
        public int Quantity { get; set; }
        public string? IFormImage { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<Category>? Category { get; set; }

    }
}
