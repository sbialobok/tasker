using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.Manager
{
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
            _runningTeamID = 0;
            _runningUserID = 0;
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
            }
            
        }

        public User GetUser(uint id)
        {
            throw new NotImplementedException();
        }

        public User GetUser(string team, string username)
        {
            throw new NotImplementedException();
        }

        public List<User> GetTeamUsers(uint teamid)
        {
            throw new NotImplementedException();
        }


        public Team GetTeam(string team)
        {
            throw new NotImplementedException();
        }
    }
}

