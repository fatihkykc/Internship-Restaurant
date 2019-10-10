using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestoranUygulamasý.Models
{
    [Table("YemIcin")]
    public partial class YemIcin
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int yem_id {get; set;}
        [Key]
        [Column(Order = 1)]
        public int icin_id {get; set;}

        public virtual Yemek Yemek { get; set;}
        public virtual Icindekiler Icindekiler { get; set;}
    }
}