using System;
using System.ComponentModel.DataAnnotations;

namespace Skopei.DTO
{
    /*
     * UserPutDTO class
     *
     * This class is a Data Transfer Object (DTO) which is used when an User is updated.
     * Contains only the necessary fields when updating an User.
     */
    public class UserPutDTO
    {
        [Required]
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required] 
        public DateTime DateCreated { get; set; }

        [Required] 
        public bool Deleted { get; set; }
    }
}