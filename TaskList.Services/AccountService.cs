using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TaskList.BizModels;
using TaskList.RepositoryContracts;
using TaskList.ServiceContracts;

namespace TaskList.Services
{
    public class AccountService : IAccountService
    {
        private IWorkFactory _workFactory;
        public AccountService(IWorkFactory workfactory)
        {
            _workFactory = workfactory;
        }

        public void LoginUser(string teamname, string username)
        {
            //Need two units of work so that if the team doesn't exist is can be created.
            using (IUnitOfWork work = _workFactory.GetWorkUnit())
            {
                //First check to see if the team exists
                var team = work.AccountRepository.GetTeam(teamname);
                if (team == null)
                {
                    work.AccountRepository.AddTeam(teamname);
                    work.Save();
                }

                //Then check to see if the user exists
                var user = work.AccountRepository.GetUser(teamname, username);
                if (user == null)
                {
                    work.AccountRepository.AddUser(username, teamname);
                    work.Save();
                }
            }
        }

        public List<BizModels.User> GetUsers(string team)
        {
            List<BizModels.User> users;
            using (IUnitOfWork work = _workFactory.GetWorkUnit())
            {
                users = work.AccountRepository.GetUsersByTeam(team);
            }

            return users;
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
