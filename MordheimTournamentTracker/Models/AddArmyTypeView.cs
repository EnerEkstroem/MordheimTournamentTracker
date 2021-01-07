using MordheimTournamentTrackerModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MordheimTournamentTracker.Models {
    public class AddArmyTypeView {
        [DisplayName("Tilgængelige spil")]
        public SelectList AvailableGames { get; set; }
        public int SelectedGame { get; set; }
        [DisplayName("Navn")]
        public string Name { get; set; }
    }
}