using System.Collections.Generic;
using TTC_DB.Entities;

namespace Web.Models
{
    public class ProfileViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Game> Games { get; set; }
        public int? Rating { get; set; }
    }
}