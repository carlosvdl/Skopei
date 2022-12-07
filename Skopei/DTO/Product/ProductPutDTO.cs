using System;
using System.ComponentModel.DataAnnotations;

namespace Skopei.DTO.Product
{
    /*
     * ProductPutDTO class
     *
     * This class is a Data Transfer Object (DTO) which is used when an Product is updated.
     * Contains only the necessary fields when updating an Product.
     */
    public class ProductPutDTO
    {
        [Required]
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