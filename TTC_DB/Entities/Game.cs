using System;
using System.Collections.Generic;

namespace TTC_DB.Entities
{
    public class Game
    {
        public Game()
        {
            GameResults = new HashSet<GameResult>();
        }

        public int GameId { get; set; }

        public DateTime Date { get; set; }

        public int WinnerId { get; set; }

        public virtual ICollection<GameResult> GameResults { get; set; }

        public virtual Player Winner { get; set; }

    }
}