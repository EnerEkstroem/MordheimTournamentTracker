using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordheimTournamentTrackerModel.Models {
    public class Tournament {
        public int Id { get; set; } //ID for Entity Framework
        [DisplayName("Turnering")]
        public string Name { get; set; }
        [DisplayName("Spil")]
        public Game Game { get; set; }
        [DisplayName("Konkurrerende Hære")]
        public List<Army> CompetingArmies { get; set; } = new List<Army>();
        public List<Match> Matches { get; set; }
    }
}
