using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TTC_DB.Context;
using TTC_DB.Entities;
using Web.Models;

namespace Web.Controllers
{
    public class StatsController : ApiController
    {
        TtcContext db = new TtcContext();
        
        public StatsViewModel Get(int id)
        {
            var games =
                db.Games.Where(
                    x => x.GameResults.Any(y => db.GameResults.Where(z => z.PlayerId == id).Contains(y)));
            var count = games.Count();
            var wins = games.Count(x => x.WinnerId == id);
            var loses = count - wins;
            var winPercent = wins * 100 / count;
            var stats = new StatsViewModel
            {
                Count = games.Count(),
                Wins = wins,
                Loses = loses,
                WinsPercent = winPercent,
                LosePercent = 100 - winPercent
            };

            return stats;
        }

        // POST api/<controller>
        public void Post([FromBody] string value) { }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value) { }

        // DELETE api/<controller>/5
        public void Delete(int id) { }
    }
}