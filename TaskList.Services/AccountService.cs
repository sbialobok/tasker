using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TaskList.ServiceContracts;

namespace TaskList.Services
{
    public class AccountService : IAccountService
    {
        public void LoginUser(string team, string username)
        {
            throw new NotImplementedException();
        }

        public BizModels.User GetUser(string team, string username)
        {
            throw new NotImplementedException();
        }

        public BizModels.Team GetTeam(string team)
        {
            throw new NotImplementedException();
        }
    }
}
