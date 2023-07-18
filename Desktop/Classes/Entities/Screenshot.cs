namespace Diplom.Classes.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("public.Screenshot")]
    public partial class Screenshot
    {
        public int ScreenshotId { get; set; }

        public int FilmId { get; set; }

        [Required]
        [MaxLength(2147483647)]
        public byte[] ScreenshotImage { get; set; }

        public virtual Film Film { get; set; }
    }
}
