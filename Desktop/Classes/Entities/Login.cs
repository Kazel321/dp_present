namespace Diplom.Classes.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("public.Login")]
    public partial class Login
    {
        public int LoginId { get; set; }

        public int UserId { get; set; }

        public DateTime LoginDateTime { get; set; }

        public virtual User User { get; set; }
    }
}
