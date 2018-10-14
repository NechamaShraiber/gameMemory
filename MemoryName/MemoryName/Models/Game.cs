using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MemoryGame.Models
{
    public class Game
    {
        [Required]
        public User Player1 { get; set; }
        [Required]
        public User Player2 { get; set; }
        [MinLength(2), MaxLength(10)]
        public string CurrentTurn { get; set; }
        /// <summary>
        /// The key is the card content
        /// The value  is the name of the user that found the pair
        /// </summary>
        public Dictionary<string, string> CardArray { get; set; } = new Dictionary<string, string>();
    }
}