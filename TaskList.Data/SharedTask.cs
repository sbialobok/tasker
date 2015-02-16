namespace TaskList.EFData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SharedTask
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int TaskID { get; set; }

        public int UserID { get; set; }

        public virtual Task Task { get; set; }

        public virtual User User { get; set; }
    }
}
