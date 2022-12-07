using System.ComponentModel.DataAnnotations;

namespace Skopei.DTO
{
    /*
     * UserPostDTO class
     *
     * This class is a Data Transfer Object (DTO) which is used for when posting an User.
     * Contains only the necessary fields to create an User.
     */
    public class UserPostDTO
    {
        public string Name { get; set; }
        
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}