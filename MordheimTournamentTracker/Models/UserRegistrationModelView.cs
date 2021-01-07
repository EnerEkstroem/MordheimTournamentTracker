using MordheimTournamentTrackerModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MordheimTournamentTracker.Models {
    public class UserRegistrationModelView {
        public User User { get; set; }
        [DisplayName("Bekræft Adgangskode")]
        public string PasswordConfirmation {get; set;}
    }
}