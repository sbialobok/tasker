﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TaskList.BizModels;

namespace TaskList.Manager
{
    public interface IAccountManager
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        Team GetTeam(string team);
    }
}
