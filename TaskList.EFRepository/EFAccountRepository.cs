using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TaskList.EFData;
using TaskList.RepositoryContracts;

namespace TaskList.EFRepository
{
    public class AccountRepository : IAccountRepository
    {
        /// <summary>
        /// Context to be used to handle db insteractions
        /// </summary>
        private TaskListDataContext _context;
        public AccountRepository(TaskListDataContext context)
        {
            _context = context;
        }

        #region Team
        public void AddTeam(string team)
        {
            var efteam = _context.Teams.Add(
                new Team
                {
                    Name = team
                }
            );
        }

        public BizModels.Team GetTeam(string name)
        {
            var result = from t in _context.Teams
                         where t.Name == name
                         select t;

            if (result.Count() == 0)
                return null;

            return ToBizTeam(result.First());
        }

        internal static EFData.Team ToEFTeam(BizModels.Team team)
        {
            if (team == null)
                throw new ArgumentNullException("team");

            return new EFData.Team
            {
                ID = team.Id,
                Name = team.Name
            };
        }

        internal static BizModels.Team ToBizTeam(Team team)
        {
            if (team == null)
                throw new ArgumentNullException("team");

            return new BizModels.Team
            {
                Id = team.ID,
                Name = team.Name
            };
        }
        #endregion Team

        #region User
        public void AddUser(string username, string teamname)
        {
            var team = GetTeam(teamname);
            if (team == null)
                throw new InvalidOperationException("Team name given for user does not exist");

            _context.Users.Add(new User 
            {
                Name = username,
                TeamID = team.Id
            });
        }

        public BizModels.User GetUser(string team, string name)
        {
            var teamobj = this.GetTeam(team);
            if (teamobj == null)
                return null;

            var result = from u in _context.Users
                         where u.TeamID == teamobj.Id && u.Name == name
                         select u;

            if (result.Count() == 0)
                return null;

            return ToBizUser(result.First());
                         
        }

        public List<BizModels.User> GetUsersByTeam(string team)
        {
            var teamobj = this.GetTeam(team);
            if (teamobj == null)
                return null;

            var result = from u in _context.Users
                         where u.TeamID == teamobj.Id
                         select u;

            return result.ToList().Select(u => ToBizUser(u)).ToList();
        }

        /// <summary>
        /// Converts a biz model user to an ef model user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        internal static EFData.User ToEFUser(BizModels.User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return new EFData.User
            {
                ID = user.Id,
                Name = user.Name,
                TeamID = user.Team.Id
            };
        }

        /// <summary>
        /// Converts a ef model user to a biz model user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        internal static BizModels.User ToBizUser(EFData.User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return new BizModels.User
            {
                Id = user.ID,
                Name = user.Name,
                Team = new BizModels.Team
                {
                    Id = user.TeamID
                }
            };
        }
        #endregion User
        
    }
}
