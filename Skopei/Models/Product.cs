using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skopei.Models
{
    /*
     * Product model
     */
    public class Product
    {
        [Key]
        [DatabaseGenerated((DatabaseGeneratedOption.Identity))]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public int Quantity { get; set; } = 0;

        [Required] 
        public double Price { get; set; }

        [Required]
        public DateTime DateModified { get; set; } = DateTime.UtcNow;

        [Required] 
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        [Required] 
        public bool Deleted { get; set; } = false;
    }
}