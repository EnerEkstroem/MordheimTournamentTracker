using MordheimTournamentTrackerModel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordheimTournamentTrackerModel.DBContext {
    public class DatabaseContext : DbContext {

        public DatabaseContext() : base("name=TournamentDatabase") {

        }

        public DbSet<Army> Armies {get; set;}
        public DbSet<ArmyType> Types { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
