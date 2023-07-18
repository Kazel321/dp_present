namespace Diplom.Classes.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("public.User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Login = new HashSet<Login>();
            Ticket = new HashSet<Ticket>();
        }

        public int UserId { get; set; }

        public int RoleId { get; set; }

        [Required]
        [StringLength(8000)]
        public string UserEmail { get; set; }

        [Required]
        [StringLength(8000)]
        public string UserLogin { get; set; }

        [Required]
        [StringLength(8000)]
        public string UserPassword { get; set; }

        [Required]
        [StringLength(8000)]
        public string UserLastName { get; set; }

        [Required]
        [StringLength(8000)]
        public string UserFirstName { get; set; }

        [Required]
        [StringLength(8000)]
        public string UserPatronymic { get; set; }

        [Required]
        [StringLength(15)]
        public string UserPhoneNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime UserBirthDate { get; set; }

        public bool UserActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Login> Login { get; set; }

        public virtual Role Role { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
