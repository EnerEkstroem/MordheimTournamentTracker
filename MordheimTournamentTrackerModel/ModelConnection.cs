using MordheimTournamentTrackerModel.DBContext;
using MordheimTournamentTrackerModel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordheimTournamentTrackerModel {
    public class ModelConnection {
        private DatabaseContext database = new DatabaseContext();

        /// <summary>
        /// Searches the database for user with the specified user name and password, and returns the first found
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User GetUserLogginIn(User user) {
            return database.Users.Where(u => u.UserName == user.UserName && u.Password == user.Password).FirstOrDefault();
        }

        /// <summary>
        /// Saves the user given as a parameter to the database
        /// </summary>
        /// <param name="user"></param>
        public void CreateUser(User user) {
            database.Users.Add(user);
            database.SaveChanges();
        }
        /// <summary>
        /// returns the user with the corrosponding id in the database
        /// </summary>
        /// <param name="id">the id of the user to be retrieved</param>
        /// <returns></returns>
        public User GetUserBy(int id) {
            return database.Users.Find(id);
        }

        /// <summary>
        /// Returns a list of all the users in the database
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllUsers() {
            return new List<User>(database.Users.ToList());
        }

        /// <summary>
        /// Returns a list of all the armies owned by the user saved in the database with the given id
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<Army> GetUsersArmies(int userId) {
            return database.Armies.Include(x => x.Owner).Where(x => x.Owner.Id == userId)
                .Include(x => x.Game).Include(x => x.Type).ToList();
        }

        /// <summary>
        /// Saves the game given as a paramter in the database
        /// </summary>
        /// <param name="game"></param>
        public void CreateGame(Game game) {
            database.Games.Add(game);
            database.SaveChanges();
        }

        /// <summary>
        /// Returns the game saved in the database with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Game GetGameBy(int id) {
            return database.Games.Where(g => g.Id == id)
                .Include(g => g.Armies)
                .FirstOrDefault();
        }

        /// <summary>
        /// returns a list of all the games saved in the database
        /// </summary>
        /// <returns></returns>
        public List<Game> GetAllGames() {
            return database.Games.ToList();
        }

        /// <summary>
        /// Saves the army type given as a parameter in the database
        /// and associates it with the game saved in the database with the given id
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="armyType"></param>
        public void AddArmyTypeToGame(int gameId, string armyType) {
            Game GameGettingArmyType = GetGameBy(gameId);
            GameGettingArmyType.Armies.Add(new ArmyType() { Name = armyType });
            database.SaveChanges();
        }
        /// <summary>
        /// retrieves the army type saved by the given id in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ArmyType GetArmyTypeBy(int id) {
            return database.Types.Find(id);
        }

        /// <summary>
        /// Returns a list of all the army types available in the game saved by the given id in the database
        /// </summary>
        /// <param name="GameId"></param>
        /// <returns></returns>
        public List<ArmyType> GetArmyTypesForGame(int gameId) {
            Game game = database.Games.Include(g => g.Armies).Where(g => g.Id == gameId).First();
            return game.Armies;
        }

        /// <summary>
        /// Creates an army for the currently logged in user with the specified parameters
        /// </summary>
        /// <param name="loggedInUser"></param>
        /// <param name="gameId"></param>
        /// <param name="armyName"></param>
        /// <param name="armyType"></param>
        /// <param name="rating"></param>
        public void CreateArmyForUser(int loggedInUserId, int gameId, string armyName, int armyTypeId, int rating) {
            Army army = new Army {
                Name = armyName,
                Game = GetGameBy(gameId),
                Type = GetArmyTypeBy(armyTypeId),
                Owner = GetUserBy(loggedInUserId),
                Raiting = rating
            };
            database.Armies.Add(army);
            database.SaveChanges();
        }

        /// <summary>
        /// returns the army saved in the database with the specified Id
        /// </summary>
        /// <param name="armyId"></param>
        /// <returns></returns>
        public Army GetArmyBy(int armyId) {
            return database.Armies.Include(a => a.Owner).Include(a => a.Type).Include(a => a.Game).Where(a => a.Id == armyId).First();
        }

        /// <summary>
        /// returns a list of the armies that the specified user has for the specified game
        /// </summary>
        /// <returns></returns>
        public List<Army> GetUserArmiesForGame(int userId, int gameId) {
            return database.Armies.Include(a => a.Type).Where(a => a.Owner.Id == userId && a.Game.Id == gameId).ToList();
        }
        /// <summary>
        /// returns a list of the armies for the specified game not owned by the specified
        /// </summary>
        /// <returns></returns>
        public List<Army> GetPossibleOpponentsForUserInGame(int userId, int gameId) {
            List<Army> opponents = database.Armies.Include(a => a.Type).Where(a => a.Owner.Id != userId && a.Game.Id == gameId).ToList();
            opponents.Sort();
            return opponents;
        }

        /// <summary>
        /// Saves a challenges from the attacking army to defending army in the database
        /// </summary>
        /// <param name="attackerId"></param>
        /// <param name="DefenderId"></param>
        /// <param name="ShowDown"></param>
        public void CreateChallengeBetween(int attackerId, int defenderId, DateTime showDown) {
            Army attacker = GetArmyBy(attackerId);
            Army defender = GetArmyBy(defenderId);
            Challenge challenge = new Challenge {
                Name = attacker.Owner.Name + "s " + attacker.Name + " har udfordret dine " + defender.Name,
                Challenger = attacker,
                Challengee = defender,
                ShowDown = showDown
            };
            database.Challenges.Add(challenge);
            database.SaveChanges();
        }
        /// <summary>
        /// Returns a list of the outstanding challenges for the specified User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Challenge> GetChallengesForUser(int userId) {
            List<Challenge> challenges = database.Challenges.Where(c => c.Challengee.Id == userId && DateTime.Compare(DateTime.Today, c.ShowDown) <= 0).ToList();
            challenges.Sort();
            return challenges;

        }

        public Challenge GetChallengeBy(int id) {
            return database.Challenges.Include(c => c.Challenger).Include(c => c.Challenger.Type)
                .Include(c => c.Challengee).Include(c => c.Challengee.Type).First();
        }

        public void AcceptChallengeBy(int id) {
            Challenge challenge = GetChallengeBy(id);
            Match match = challenge.AcceptChallenge();
            database.Challenges.Remove(challenge);
            database.Matches.Add(match);
            database.SaveChanges();
        }

        public void RejectChallengeBy(int id) {
            Challenge challenge = GetChallengeBy(id);
            database.Challenges.Remove(challenge);
            database.SaveChanges();
        }

        /// <summary>
        /// Returns a list of upcoming matches for the player with the specified id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Match> GetUpcommingMatches(int id) {
            List<Match> upcommingMatches = new List<Match>();
            List<Match> Matches = database.Matches.Include(m => m.ArmyOne).Include(m => m.ArmyOne.Owner)
                .Include(m => m.ArmyTwo).Include(m => m.ArmyTwo.Owner).ToList();
            foreach (var match in Matches) {
                if (match.ArmyOne.Owner.Id == id || match.ArmyTwo.Owner.Id == id) {
                    if (DateTime.Compare(DateTime.Now, match.Date) < 0) {
                        upcommingMatches.Add(match);
                    }
                }
            }
            upcommingMatches.Sort();
            return upcommingMatches;
        }

        public List<Match> GetUserMatches(int id) {
            List<Match> upcommingMatches = new List<Match>();
            List<Match> Matches = database.Matches.Include(m => m.ArmyOne).Include(m => m.ArmyOne.Owner)
                .Include(m => m.ArmyTwo).Include(m => m.ArmyTwo.Owner).ToList();
            foreach (var match in Matches) {
                if (match.ArmyOne.Owner.Id == id || match.ArmyTwo.Owner.Id == id) {
                        upcommingMatches.Add(match);
                }
            }
            upcommingMatches.Sort();
            return upcommingMatches;
        }

        public List<Match> GetMatches() {
            List<Match> matches = database.Matches.Include(m=>m.ArmyOne).Include(m => m.ArmyTwo).Include(m => m.Winner).ToList();
            matches.Sort();
            return matches;
        }

        public Match GetMatchBy(int id) {
            return database.Matches.Include(m => m.ArmyOne).Include(m => m.ArmyTwo).Where(m => m.Id == id).First();
        }

        public void Dispose() {
            database.Dispose();
        }
    }
}
