using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MordheimTournamentTracker.Models;
using MordheimTournamentTrackerModel;
using MordheimTournamentTrackerModel.Models;

namespace MordheimTournamentTracker.Controllers {
    public class UsersController : Controller {
        private ModelConnection mc = new ModelConnection();

        //GET : Login
        public ActionResult Login() {
            Session["UserID"] = null;
            return View();
        }

        [HttpPost]
        //POST :Login
        public ActionResult Login(User AttemptedLogin) {
            User AuthorisedUser = mc.GetUserLogginIn(AttemptedLogin);
            if (AuthorisedUser != null) {
                Session["UserID"] = AuthorisedUser.Id.ToString();
                return RedirectToAction("Dashboard");
            }
            else {
                ModelState.AddModelError("", "Brugernavnet eller kodeordet er forket");
            }
            return View();
        }

        //GET : Users/Dashboard
        public ActionResult Dashboard() {
            if (Session["UserID"] != null) {
                UserDashboardModelView model = new UserDashboardModelView();
                model.LoggedInUser = mc.GetUserBy(Convert.ToInt32(Session["UserID"])).Name;
                model.UpcommingMatches = mc.GetUpcommingMatches(Convert.ToInt32(Session["UserID"]));
                model.IncommingChallenges = mc.GetChallengesForUser(Convert.ToInt32(Session["UserID"]));
                return View(model);
            }
            else {
                return RedirectToAction("Login");
            }
        }

        //Get : Users/UserArmies
        public ActionResult UserArmies() {
            if (Session["UserID"] != null) {
                List<Army> userArmies = mc.GetUsersArmies(Convert.ToInt32(Session["UserID"]));

                return View(userArmies);
            }
            else {
                return RedirectToAction("Login");
            }
        }

        //GET : Users/CreateArmy
        public ActionResult CreateArmy() {
            if (Session["UserID"] != null) {
                AddUserArmyView model = new AddUserArmyView {
                    AvailableGames = new SelectList(mc.GetAllGames(), "Id", "Name")
                };
                return View(model);
            }
            else {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        //POST : Users/CreateArmy
        public ActionResult CreateArmy(AddUserArmyView model) {
            if (Session["UserID"] != null) {
                mc.CreateArmyForUser(Convert.ToInt32(Session["UserID"]), model.SelectedGame, model.Name, model.SelectedArmyType, model.Raiting);
                return RedirectToAction("UserArmies");
            }
            else {
                return RedirectToAction("Login");
            }
        }

        // GET: Users/Register
        public ActionResult Register() {
            return View();
        }

        // POST: Users/Register
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserRegistrationModelView modelView) {
            if (ModelState.IsValid) {
                bool userNameTaken = false;
                foreach (User registeredUser in mc.GetAllUsers()) {
                    if (modelView.User.Name.Trim().Equals(registeredUser.UserName)) {
                        userNameTaken = true;
                    }
                }
                if (modelView.User.UserName == null || modelView.User.Password == null || modelView.User.Name == null) {
                    modelView.User.UserName = "";
                    modelView.User.Password = "";
                    modelView.User.Name = "";
                    ModelState.AddModelError("", "Udfyld venligst alle felter");
                }
                else if (userNameTaken) {
                    modelView.User.UserName = "";
                    modelView.User.Password = "";
                    modelView.User.Name = "";
                    ModelState.AddModelError("", "Brugernavnet er allerede taget");
                }
                else if (!modelView.User.Password.Equals(modelView.PasswordConfirmation)) {
                    modelView.User.UserName = "";
                    modelView.User.Password = "";
                    modelView.User.Name = "";
                    ModelState.AddModelError("", "Dine adgangs koder er ikke identiske");
                }
                else if (modelView.User.UserName.Equals("") || modelView.User.Password.Equals("") || modelView.User.Name.Equals("")) {
                    modelView.User.UserName = "";
                    modelView.User.Password = "";
                    modelView.User.Name = "";
                    ModelState.AddModelError("", "Udfyld venligst alle felter");
                }
                else {
                    User user = new User() {
                        UserName = modelView.User.UserName,
                        Password = modelView.User.Password,
                        Name = modelView.User.Name.Trim()
                    };
                    mc.CreateUser(user);
                    return RedirectToAction("Login");
                }
            }

            return View(modelView);
        }

        // GET: Users/Edit/5
        public ActionResult Edit() {
            if (Session["UserID"] != null) {
                User user = mc.GetUserBy(Convert.ToInt32(Session["UserID"]));
                if (user == null) {
                    return HttpNotFound();
                }
                return View(user);
            }
            else {
                return RedirectToAction("Login");
            }
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Password,Name")] User user) {
            /*
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            */
            return View(user);
        }

        [HttpPost]
        public JsonResult GetAvailableArmyTypes(string gameId) {
            int gameIndex = Convert.ToInt32(gameId);
            List<SelectListItem> AvailableArmyTypes = new List<SelectListItem>();
            foreach (ArmyType army in mc.GetArmyTypesForGame(gameIndex)) {
                AvailableArmyTypes.Add(new SelectListItem { Text = army.Name, Value = army.Id.ToString() });
            }
            return Json(new SelectList(AvailableArmyTypes, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        protected override void Dispose(bool disposing) {
            if (disposing) {

                mc.Dispose(); ;
            }
            base.Dispose(disposing);
        }
    }
}
