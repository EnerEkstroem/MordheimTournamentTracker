using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordheimTournamentTrackerModel.Models {
    public class ArmyType {
        public int Id { get; set; } // ID for Entity Framework
        [DisplayName("Hær")]
        public string Name { get; set; }
    }
}
