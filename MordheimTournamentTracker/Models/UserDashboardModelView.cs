using MordheimTournamentTrackerModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MordheimTournamentTracker.Models {
    public class UserDashboardModelView {
        public string LoggedInUser { get; set; }
        public List<Match> UpcommingMatches { get; set; }
        public List<Challenge> IncommingChallenges { get; set; }
    }
}