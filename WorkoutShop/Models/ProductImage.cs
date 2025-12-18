using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutShop.Models
{
    public class ProductImage
    {
        [Key]
        public int ImageId { get; set; }  

        [Required]
        public int ProductId { get; set; } 

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Required]
        [MaxLength(255)]
        public string ImageUrl { get; set; }

        [Required]
        public bool IsPrimary { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }
    }
}
