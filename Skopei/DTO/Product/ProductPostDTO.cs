using System.ComponentModel.DataAnnotations;

namespace Skopei.DTO.Product
{
    /*
     * ProductPostDTO class
     *
     * This class is a Data Transfer Object (DTO) which is used for when posting an Product.
     * Contains only the necessary fields to create an Product.
     * The price attribute has a ? so you can't post a Product without sending a Price.
     * Without the ? the Price value would be automatically be 0 if it was not included.
     */
    public class ProductPostDTO
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public int Quantity { get; set; } = 0;

        [Required] 
        public double? Price { get; set; }
    }
}