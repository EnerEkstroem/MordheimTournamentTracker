using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MordheimTournamentTracker.Models {
    public class AddUserArmyView {
        [DisplayName("Spil")]
        public SelectList AvailableGames { get; set; }
        public int SelectedGame { get; set; }
        [DisplayName("Arme Navn")]
        public string Name { get; set; }

        [DisplayName("Arme type")]
        public List<SelectListItem> AvailableArmyTypes { get; set; }
        public int SelectedArmyType { get; set; }

        [DisplayName("Arme Rating")]
        public int Raiting { get; set; }
    }
}