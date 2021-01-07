using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordheimTournamentTrackerModel.Models {
    public class Army : IEquatable<Army>, IComparable<Army> {
        public int Id { get; set; } //ID for Entity Framework
        private User _owner;
        [DisplayName("Ejer")]
        public User Owner {
            get {return _owner; }
            set {
                if(_owner != null) {
                    RemoveOwner();
                }
                _owner = value;
                value.Armies.Add(this);
            }
        }
        public Game Game { get; set; }
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Type")]
        public ArmyType Type { get; set; }
        [DisplayName("Rating")]
        public int Raiting { get; set; }

        public bool Equals(Army other) {
            return Id.Equals(other.Id);
        }

        private void RemoveOwner() {
            if (_owner.Armies.Contains(this)) {
                // Nah, we want to keep a record of who owned an army in old tournament records even if it is retired
            }
            _owner = null;
        }

        public int CompareTo(Army other) {
            return other.Raiting - Raiting;
        }
    }
}
