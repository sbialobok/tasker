using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskList.BizModels
{
    public class User
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public Team Team { get; set; }
    }
}
