using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.Manager
{
    public interface IAccountManager
    {
        void LoginUser(string team, string username);

        /// <summary>
        /// Gets a user by their ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User GetUser(uint id);

        /// <summary>
        /// Gets a user by this name and team
        /// </summary>
        /// <param name="team"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        User GetUser(string team, string username);

        /// <summary>
        /// Returns a list of users associated with a team
        /// </summary>
        /// <param name="teamid"></param>
        /// <returns></returns>
        List<User> GetTeamUsers(uint teamid);

        Team GetTeam(string team);
    }
}
