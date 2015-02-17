using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TaskList.BizModels;

namespace TaskList.ServiceContracts
{
    public interface IAccountService
    {
        /// <summary>
        /// Persists a user and team if they dont already exist
        /// </summary>
        /// <param name="team"></param>
        /// <param name="username"></param>
        void LoginUser(string team, string username);

        /// <summary>
        /// Gets a user by this name and team
        /// </summary>
        /// <param name="team"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        User GetUser(string team, string username);

        List<User> GetUsers(string team);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        Team GetTeam(string team);
    }
}
