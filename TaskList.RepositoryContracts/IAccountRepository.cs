using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TaskList.BizModels;

namespace TaskList.RepositoryContracts
{
    public interface IAccountRepository
    {
        void AddTeam(string team);
        Team GetTeam(string name);
        
        void AddUser(string username, string teamname);
        User GetUser(string team, string name);
        List<User> GetUsersByTeam(string team);
    }
}
