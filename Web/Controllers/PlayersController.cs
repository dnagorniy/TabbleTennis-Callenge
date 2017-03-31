using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TTC_DB.Context;
using TTC_DB.Entities;
using WebGrease.Css.Extensions;

namespace Web.Controllers
{
    public class PlayersController : ApiController
    {
        TtcContext db = new TtcContext();

        // GET api/<controller>
        public IEnumerable<Player> Get()
        {
            return db.Players.AsEnumerable().Select(x => new Player
            {
                PlayerId = x.PlayerId,
                Name = x.Name.Split('@')[0],
                Rating = x.Rating,
                Games = x.Games,
                GameResults = x.GameResults
            }).OrderByDescending(y => y.Rating);
        }

        // GET api/<controller>/5
        public Profile Get(int id)
        {
            var player = db.Players.FirstOrDefault(x => x.PlayerId == id);
            var gr = db.GameResults.Where(x => x.PlayerId == player.PlayerId);
            var profile = new Profile()
            {
                Rating = player.Rating,
                Id = player.PlayerId,
                Name = player.Name,
                Games = db.Games.Where(x=>x.GameResults.Any(y=>gr.Contains(y))).ToList()
            };
            return profile;
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }

    public class Profile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Game> Games { get; set; }
        public int? Rating { get; set; }
    }
}