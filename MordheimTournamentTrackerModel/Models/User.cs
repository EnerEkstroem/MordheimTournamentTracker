using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordheimTournamentTrackerModel.Models {
    public class User {
        public int Id { get; set; } //ID for Entity Framework
        [DisplayName("Bruger Navn")]
        public string UserName { get; set; }
        [DisplayName("Adgangskode")]
        public string Password { get; set; }
        [DisplayName("Navn")]
        public string Name { get; set; }
        [DisplayName("Dine Hære")]
        public List<Army> Armies { get; set; } = new List<Army>();
        [DisplayName("Turneringer du deltager i")]
        public List<Tournament> Tournaments { get; set; } = new List<Tournament>();
        [DisplayName("Turneringer du organisere")]
        public List<Tournament> OrganisedTournaments { get; set; } = new List<Tournament>();
    }
}
