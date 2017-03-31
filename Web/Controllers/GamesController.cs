using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TTC_DB.Context;
using TTC_DB.Entities;
using Web.Models;

namespace Web.Controllers
{
    public class GamesController : ApiController
    {
        TtcContext db = new TtcContext();

        // GET api/<controller>
        public IEnumerable<Game> Get()
        {
            return db.Games.OrderByDescending(x=>x.Date);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] GameViewModel value)
        {
            var p1 = db.Players.FirstOrDefault(x => x.Name == value.P1Name);
            var p2 = db.Players.First(x=>x.Name.Contains(value.P2Name));
            var winner = (value.P1Score > value.P2Score) ? p1 : p2;
            var loser = (p1 == winner) ? p2 : p1;
            var points = 0;
            var diffRate = winner.Rating - loser.Rating;
            if (diffRate < -20)
            {
                points = 4;
            }
            if (diffRate < -10 && diffRate >= -20)
            {
                points = 3;
            }
            if (diffRate < 0 && diffRate >= -10)
            {
                points = 2;
            }
            if (diffRate >= 0 && diffRate < 10)
            {
                points = 2;
            }
            if (diffRate >= 10)
            {
                points = 1;
            }
            winner.Rating += points;
            loser.Rating -= 1;

            db.Players.Attach(winner);
            var e1 = db.Entry(winner);
            e1.Property((x => x.Rating)).IsModified = true;

            db.Players.Attach(loser);
            var e2 = db.Entry(loser);
            e2.Property((x => x.Rating)).IsModified = true;

            var game = new Game()
            {
                Date = DateTime.Now,
                Winner = winner
            };
            db.Games.Add(game);

            var gr1 = new GameResult()
            {
                Game = game,
                Player = p1,
                Score = value.P1Score
            };
            var gr2 = new GameResult()
            {
                Game = game,
                Player = p2,
                Score = value.P2Score
            };
            db.GameResults.Add(gr1);
            db.GameResults.Add(gr2);
            db.SaveChanges();
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
}