using MordheimTournamentTrackerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MordheimTournamentTracker.Controllers {
    public class MatchesController : Controller {
        ModelConnection mc = new ModelConnection();
        // GET: Matches
        public ActionResult AllMatches() {
            if (Session["UserID"] != null) {
                return View(mc.GetMatches());
            }
            else {
                return RedirectToAction("Login", "Users");
            }
        }

        //GET : UserMatches
        public ActionResult UserMatches() {
            if(Session["UserID"] != null) {
                return View(mc.GetUserMatches(Convert.ToInt32(Session["UserID"])));
            }
            else {
                return RedirectToAction("Login", "Users");
            }
        }
    }
}