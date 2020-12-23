using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordheimTournamentTrackerModel.Models {
    class ArmyType {
        public int Id { get; set; } // ID for Entity Framework
        [DisplayName("Hær")]
        public string Type { get; set; }
    }
}
