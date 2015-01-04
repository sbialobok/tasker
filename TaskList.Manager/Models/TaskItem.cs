using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskList.Manager
{
    public class TaskItem
    {
        /// <summary>
        /// ID of the task
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// ID of the user who created the task
        /// </summary>
        public uint OwnerId { get; set; }

        /// <summary>
        /// List of users who share the task
        /// </summary>
        public List<uint> Users { get; set; }

        /// <summary>
        /// The description of the task
        /// </summary>
        public string Description { get; set; }
    }
}
