using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MordheimTournamentTracker.Models {
    public class IssueChallengeModelView {
        [DisplayName("Mulige Spil")]
        public SelectList AvailableGames { get; set; }
        public string SelectedGame { get; set; }
        public string ChallengingArmy { get; set; }
        public string DefendingArmy { get; set; }
        [DisplayName("Spil Dato")]
        public DateTime ShowDown { get; set; }
    }
}