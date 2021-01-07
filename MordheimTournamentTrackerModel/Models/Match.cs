using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordheimTournamentTrackerModel.Models {
    public class Match : IComparable<Match>{
        public int Id { get; set; } //ID for Entity Framework
        [DisplayName("Kamp")]
        public string Name { get; set; }
        [DisplayName("Dato")]
        public DateTime Date { get; set; }
        [DisplayName("Første Hær")]
        public Army ArmyOne { get; set; }
        [DisplayName("Anden Hær")]
        public Army ArmyTwo { get; set; }
        [DisplayName("Afgjort")]
        public bool Resolved { get; set; } = false;
        [DisplayName("Vinder")]
        public Army Winner { get; set; } = null;

        public int CompareTo(Match other) {
            return DateTime.Compare(Date, other.Date);
        }

        public void resolveMatch(Army winner, int armyOneRaitingChange, int armyTwoRaitingChange) {
            if (DateTime.Compare(DateTime.Now, Date) >= 0) {
                Winner = winner;
                ArmyOne.Raiting += armyOneRaitingChange;
                ArmyTwo.Raiting += armyTwoRaitingChange;
            }
        }

    }
}
