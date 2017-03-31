using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web.Mvc;
using TTC_DB.Context;
using TTC_DB.Entities;

namespace Web.Filters
{
    public class FirstLoginChecker : ActionFilterAttribute
    {
        TtcContext db = new TtcContext();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session.IsNewSession)
            {
                using (var principalContext = new PrincipalContext(ContextType.Domain))
                {
                    var playerName = filterContext.HttpContext.User.Identity.Name.Split('\\')[1];
                    var count = db.Players.Count(x => x.Name == playerName);
                    if (count == 0)
                    {
                        db.Players.Add(new Player {Name = playerName});
                        db.SaveChanges();
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}