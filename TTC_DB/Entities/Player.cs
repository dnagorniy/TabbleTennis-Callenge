using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TTC_DB.Entities
{
    public class Player
    {
        public Player()
        {
            GameResults = new HashSet<GameResult>();
            Games = new HashSet<Game>();
        }

        public int PlayerId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [DefaultValue(10)]
        public int? Rating { get; set; }

        public ICollection<GameResult> GameResults { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}