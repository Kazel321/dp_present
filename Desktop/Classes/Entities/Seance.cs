namespace Diplom.Classes.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("public.Seance")]
    public partial class Seance
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Seance()
        {
            Ticket = new HashSet<Ticket>();
        }

        public int SeanceId { get; set; }

        public int FilmId { get; set; }

        public int HallId { get; set; }

        [Column(TypeName = "date")]
        public DateTime SeanceDate { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan SeanceTime { get; set; }

        public float SeanceCost { get; set; }

        public virtual Film Film { get; set; }

        public virtual Hall Hall { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
