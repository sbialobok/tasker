using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TaskList.Models
{
    [DataContract]
    [KnownType(typeof(Task))]
    public class Task
    {
        /// <summary>
        /// ID of the task
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Name of the user who created the task
        /// </summary>
        [DataMember]
        public string Owner { get; set; }

        /// <summary>
        /// The name of the team the task belongs to
        /// </summary>
        [DataMember]
        public string TeamName { get; set; }

        /// <summary>
        /// List of users who share the task
        /// </summary>
        [DataMember]
        public List<string> Shared { get; set; }

        /// <summary>
        /// The description of the task
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Duedate of the task
        /// </summary>
        [DataMember]
        public DateTime DueDate { get; set; }
    }
}