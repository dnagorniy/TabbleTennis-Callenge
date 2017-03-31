using System.ComponentModel.DataAnnotations;

namespace TTC_DB.Entities
{
    public partial class GameResult
    {
        [Key]
        public int GR_Id { get; set; }

        public int GameId { get; set; }

        public int PlayerId { get; set; }

        public int Score { get; set; }

        public virtual Game Game { get; set; }

        public virtual Player Player { get; set; }
    }
}
