using MordheimTournamentTracker.Models;
using MordheimTournamentTrackerModel;
using MordheimTournamentTrackerModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MordheimTournamentTracker.Controllers {
    public class ChallengesController : Controller {
        private ModelConnection mc = new ModelConnection();

        //GET Challenges/IssueChallenge
        public ActionResult IssueChallenge() {
            if (Session["UserID"] != null) {
                IssueChallengeModelView model = new IssueChallengeModelView {
                    AvailableGames = new SelectList(mc.GetAllGames(), "Id", "Name")
                };
                return View(model);
            }
            else {
                return RedirectToAction("Login", "Users");
            }
        }
        //POST Challeges/IssueChallenge
        [HttpPost]
        public ActionResult IssueChallenge(IssueChallengeModelView model) {
            if (Session["UserID"] != null) {
                mc.CreateChallengeBetween(Convert.ToInt32(model.ChallengingArmy), Convert.ToInt32(model.DefendingArmy), model.ShowDown.Date);
                int game = Convert.ToInt32(model.SelectedGame);
                return RedirectToAction("Dashboard", "Users");
            }
            else {
                return RedirectToAction("Login", "Users");
            }
        }

        // GET: Challenges/Details/id
        public ActionResult Details(int id) {
            if (Session["UserID"] != null) {
                Challenge challenge = mc.GetChallengeBy(id);
                if (challenge == null) {
                    return HttpNotFound();
                }
                return View(challenge);
            }
            else {
                return RedirectToAction("Login", "Users");
            }
        }
        // POST: Challenges/Details/id
        [HttpPost]
        public ActionResult Details(Challenge challenge, string answer) {
            if (Session["UserID"] != null) {
                if (ModelState.IsValid && !String.IsNullOrWhiteSpace(answer)) {
                    if (answer.Equals("Accepter")) {
                        mc.AcceptChallengeBy(challenge.Id);
                        return RedirectToAction("Dashboard", "Users");
                    }
                    else {
                        mc.RejectChallengeBy(challenge.Id);
                        return RedirectToAction("Dashboard", "Users");
                    }
                }
                else {
                    return RedirectToAction("Dashboard", "Users");
                }
            }
            else {
                return RedirectToAction("Login", "Users");
            }
        }

        public JsonResult GetUserArmiesForGame(string gameId) {
            List<SelectListItem> UserArmies = new List<SelectListItem>();
            foreach (Army army in mc.GetUserArmiesForGame(Convert.ToInt32(Session["UserID"]), Convert.ToInt32(gameId))) {
                UserArmies.Add(new SelectListItem { Text = army.Name + ", " + army.Type.Name + ", " + army.Raiting, Value = army.Id.ToString() });
            }
            return Json(new SelectList(UserArmies, "Value", "Text", JsonRequestBehavior.AllowGet));
        }

        public JsonResult GetPossibleOpponentsInGame(string gameId) {
            List<SelectListItem> OpposingArmies = new List<SelectListItem>();
            foreach (Army army in mc.GetPossibleOpponentsForUserInGame(Convert.ToInt32(Session["UserID"]), Convert.ToInt32(gameId))) {
                OpposingArmies.Add(new SelectListItem { Text = army.Name + ", " + army.Type.Name + ", " + army.Raiting, Value = army.Id.ToString() });
            }
            return Json(new SelectList(OpposingArmies, "Value", "Text", JsonRequestBehavior.AllowGet));
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {

                mc.Dispose(); ;
            }
            base.Dispose(disposing);
        }
    }
}