using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TaskList.BizModels;

namespace TaskList.Manager
{
    /// <summary>
    /// Holds the users and teams until I get an actual DB setup
    /// </summary>
    public class AccountManager : IAccountManager
    {
        private Dictionary<string, Dictionary<string, User>> _users;
        private Dictionary<string, Team> _teams;
        private uint _runningTeamID;
        private uint _runningUserID;

        public AccountManager()
        {
            _users = new Dictionary<string, Dictionary<string, User>>();
            _teams = new Dictionary<string, Team>();
            _runningTeamID = 1;
            _runningUserID = 1;
        }

        public void LoginUser(string teamName, string username)
        {
            //First check to see if the team exists
            Team team;
            if (!_teams.TryGetValue(teamName, out team))
            {
                team = new Team()
                {
                    Name = teamName,
                    Id = _runningTeamID++
                };
                _teams[teamName] = team;
            }

            Dictionary<string, User> teamUsers;
            if (!_users.TryGetValue(teamName, out teamUsers))
            {
                teamUsers = new Dictionary<string, User>();
                _users[teamName] = teamUsers;
            }

            User user;
            if (!teamUsers.TryGetValue(username, out user))
            {
                user = new User()
                {
                    Name = username,
                    Id = _runningUserID++,
                    Team = team
                };
                teamUsers[username] = user;
            }
            
        }

        public User GetUser(string team, string username)
        {
            User retval;
            Dictionary<string, User> teamusers;
            if (!_users.TryGetValue(team, out teamusers) || !teamusers.TryGetValue(username, out retval))
            {
                return null;
            }
            return retval;
        }

        public Team GetTeam(string team)
        {
            Team retval = null;
            _teams.TryGetValue(team, out retval);
            return retval;
        }
    }
}

