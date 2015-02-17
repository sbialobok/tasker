using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TaskList.Models;
using TaskList.BizModels;

namespace TaskList.Mapping
{
    public static class ModelToBiz
    {
        /// <summary>
        /// Handles mapping the model task to the business task
        /// </summary>
        /// <param name="task"></param>
        /// <param name="team"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public static TaskItem ToBiz(this Task task)
        {
            TaskItem retval = new TaskItem()
            {
                TeamName = task.TeamName,
                Id = task.Id,
                Owner = new BizModels.User {
                    Name = task.Owner
                },
                Description = task.Description,
                DueDate = task.DueDate
            };

            return retval;
        }

        /// <summary>
        /// Handles mapping the biz task to the model task
        /// </summary>
        /// <param name="task"></param>
        /// <param name="team"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public static Task ToModel(this TaskItem task)
        {
            Task retval = new Task()
            {
                TeamName = task.TeamName,
                Id = task.Id,
                Owner = task.Owner.Name,
                Description = task.Description,
                DueDate = task.DueDate
            };

            return retval;
        }

        public static Models.User ToModel(this BizModels.User user)
        {
            Models.User retval = new Models.User
            {
                ID = user.Id,
                Name = user.Name,
                TeamName = user.Team.Name
            };

            return retval;
        }
    }
}
