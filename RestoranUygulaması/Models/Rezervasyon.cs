namespace RestoranUygulaması.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Rezervasyon")]
    public partial class Rezervasyon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int rez_id { get; set; }

        [StringLength(50)]
        public string rez_ad { get; set; }

        [StringLength(50)]
        public string rez_soyad { get; set; }

        [StringLength(50)]
        public string rez_eposta { get; set; }

        [StringLength(11)]
        public string rez_tel { get; set; }
    }
}
