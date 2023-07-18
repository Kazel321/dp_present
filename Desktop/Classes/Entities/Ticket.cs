namespace Diplom.Classes.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("public.Ticket")]
    public partial class Ticket
    {
        public int TicketId { get; set; }

        public int SeanceId { get; set; }

        public DateTime TicketDateTime { get; set; }

        public int PlaceId { get; set; }

        public float TicketCost { get; set; }

        [Required]
        [StringLength(6)]
        public string TicketCode { get; set; }

        public bool TicketActive { get; set; }

        public int? UserId { get; set; }

        public virtual Place Place { get; set; }

        public virtual Seance Seance { get; set; }

        public virtual User User { get; set; }
    }
}
