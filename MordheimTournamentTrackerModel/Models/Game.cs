using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordheimTournamentTrackerModel.Models {
    class Game {
        public int Id { get; set; } //ID for entity Framework
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Brugte Hære")]
        public List<ArmyType> Armies { get; set; }
    }
}
