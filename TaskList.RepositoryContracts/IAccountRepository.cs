using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TaskList.BizModels;

namespace TaskList.RepositoryContracts
{
    public interface IAccountRepository
    {
        void AddTeam(Team team);
        Team GetTeam(string name);
        
        void AddUser(User user);
        User GetUser(string team, string name);
        List<User> GetUserByTeam(string team);
    }
}
