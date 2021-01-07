using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MordheimTournamentTracker.Models;
using MordheimTournamentTrackerModel;
using MordheimTournamentTrackerModel.DBContext;
using MordheimTournamentTrackerModel.Models;

namespace MordheimTournamentTracker.Controllers {
    public class GamesController : Controller {
        private ModelConnection mc = new ModelConnection();

        // GET: Games
        public ActionResult Index() {
            if (Session["UserID"] != null) {
                return null; //View(db.Games.ToList());
            }
            else {
                return RedirectToAction("Login", "Users");
            }
        }

        // GET: Games/Details/5
        public ActionResult Details(int id) {
            if (Session["UserID"] != null) {
                Game game = mc.GetGameBy(id);
                if (game == null) {
                    return HttpNotFound();
                }
                return View(game);
            }
            else {
                return RedirectToAction("Login", "Users");
            }
        }

        // GET: Games/Create
        public ActionResult Create() {
            if (Session["UserID"] != null) {
                return View();
            }
            else {
                return RedirectToAction("Login", "Users");
            }
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Game game) {
            if (Session["UserID"] != null) {
                if (ModelState.IsValid) {
                    mc.CreateGame(game);
                    return RedirectToAction("Dashboard", "Users");
                }
                return View(game);
            }
            else {
                return RedirectToAction("Login", "Users");
            }
        }

        //GET: Games/AddArmyType
        public ActionResult AddArmyType() {
            if (Session["UserID"] != null) {
                AddArmyTypeView model = new AddArmyTypeView();
                ViewBag.games = new SelectList(mc.GetAllGames(), "Id", "Name");
                return View(model);
            }
            else {
                return RedirectToAction("Login", "Users");
            }
        }

        //POST: Games/AddArmyType
        [HttpPost]
        public ActionResult AddArmyType(AddArmyTypeView model) {
            if (Session["UserID"] != null) {
                int selectedValue = model.SelectedGame;
                mc.AddArmyTypeToGame(selectedValue, model.Name);
                return RedirectToAction("Dashboard", "Users");
            }
            else {
                return RedirectToAction("Login", "Users");
            }

        }

        // GET: Games/Edit/5
        public ActionResult Edit(int? id) {
            if (Session["UserID"] != null) {
                /*
                if (id == null) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Game game = db.Games.Find(id);
                if (game == null) {
                    return HttpNotFound();
                }
                return View(game); */
                return null;
            }
            else {
                return RedirectToAction("Login", "Users");
            }
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Game game) {
            if (Session["UserID"] != null) {
                return null;
            }
            else {
                return RedirectToAction("Login", "Users");
            }
            /*
            if (ModelState.IsValid) {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(game);
            */
        }

        // GET: Games/Delete/5
        public ActionResult Delete(int? id) {
            if (Session["UserID"] != null) {
                return null;
            }
            else {
                return RedirectToAction("Login", "Users");
            }
            /*
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null) {
                return HttpNotFound();
            }
            return View(game); */
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            if (Session["UserID"] != null) {
                return null; //View(db.Games.ToList());
            }
            else {
                return RedirectToAction("Login", "Users");
            }
            /*
             * Game game = db.Games.Find(id);
            db.Games.Remove(game);
            db.SaveChanges();
            return RedirectToAction("Index");
            */
        }

        protected override void Dispose(bool disposing) {
             if (disposing) {
                 mc.Dispose();
             }
             base.Dispose(disposing);
        }
    }
}
