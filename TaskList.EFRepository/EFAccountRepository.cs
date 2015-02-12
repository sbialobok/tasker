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
        public void AddTeam(BizModels.Team team)
        {
            var efteam = _context.Teams.Add(ToEFTeam(team));
            //TODO is this necessary??
            team.Id = efteam.ID;
        }

        public BizModels.Team GetTeam(string name)
        {
            var result = from t in _context.Teams
                         where t.Name == name
                         select ToBizTeam(t);

            if (result.Count() == 0)
                return null;

            return result.ElementAt(0);
        }

        protected virtual EFData.Team ToEFTeam(BizModels.Team team)
        {
            if (team == null)
                throw new ArgumentNullException("team");

            return new EFData.Team
            {
                ID = team.Id,
                Name = team.Name
            };
        }

        protected virtual BizModels.Team ToBizTeam(Team team)
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
        public void AddUser(BizModels.User user)
        {
            _context.Users.Add(ToEFUser(user));
        }

        public BizModels.User GetUser(string team, string name)
        {
            var teamObj = this.GetTeam(team);
            var result = from u in _context.Users
                         where u.TeamID == teamObj.Id && u.Name == name
                         select ToBizUser(u);

            if (result.Count() == 0)
                return null;

            return result.ElementAt(0);
                         
        }

        public List<BizModels.User> GetUserByTeam(string team)
        {
            var teamobj = this.GetTeam(team);
            var result = from u in _context.Users
                         where u.TeamID == teamobj.Id
                         select ToBizUser(u);

            return result.ToList();
        }

        /// <summary>
        /// Converts a biz model user to an ef model user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        protected virtual EFData.User ToEFUser(BizModels.User user)
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
        protected virtual BizModels.User ToBizUser(EFData.User user)
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
