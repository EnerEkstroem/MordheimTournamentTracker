using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordheimTournamentTrackerModel.Models {
    public class Challenge : IComparable<Challenge> {
        public int Id { get; set; } //Id for entity framework
        [DisplayName("Udfordring")]
        public string Name { get; set; }
        [DisplayName("Udfordreren")]
        public Army Challenger { get; set; }
        [DisplayName("Udfordrede")]
        public Army Challengee { get; set; }
        [DisplayName("Dato")]
        public DateTime ShowDown { get; set; }

        /// <summary>
        /// Creates and returns a new match between the invovled armies
        /// </summary>
        /// <returns></returns>
        public Match AcceptChallenge() {
            return new Match {
                Name = Challenger.Name + " faces " + Challengee.Name + " on " + ShowDown.ToString("D"),
                ArmyOne = Challenger,
                ArmyTwo = Challengee,
                Date = ShowDown
            };
        }

        public int CompareTo(Challenge other) {
            return DateTime.Compare(ShowDown, other.ShowDown);
        }
    }
}
