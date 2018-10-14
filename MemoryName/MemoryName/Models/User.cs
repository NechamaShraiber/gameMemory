
using System.ComponentModel.DataAnnotations;

namespace MemoryGame.Models
{
  
    public class User
    {
        [Required]
        [MinLength(2),MaxLength(10)]
        [Key]
        public string UserName { get; set; }
        [Required]
        [Range(18, 120)]
        public int Age { get; set; }
        [MinLength(2), MaxLength(10)]
        public string PartnerName { get; set; }
        public int Score { get; set; }
    }
}