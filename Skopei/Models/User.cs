using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skopei.Models
{
    /*
     * User model
     */
    public class User
    {
        [Key]
        [DatabaseGenerated((DatabaseGeneratedOption.Identity))]
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public DateTime DateModified { get; set; } = DateTime.UtcNow;
        
        [Required] 
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        [Required] 
        public bool Deleted { get; set; } = false;
    }
    

}