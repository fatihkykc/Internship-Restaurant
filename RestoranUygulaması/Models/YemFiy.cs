using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestoranUygulamasý.Models
{
    [Table("YemFiy")]
    public partial class YemFiy
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int yem_id {get; set;}
        [Key]
        [Column(Order = 1)]
        public int fiyat_id {get; set;}

        public virtual Yemek Yemek { get; set;}
        public virtual Fiyat Fiyat { get; set;}
    }
}