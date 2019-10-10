using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestoranUygulamasý.Models
{
    [Table("YemHaz")]
    public partial class YemHaz
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int yem_id {get; set;}
        [Key]
        [Column(Order = 1)]
        public string hazir_id {get; set;}

        public virtual Yemek Yemek { get; set;}
        public virtual Hazirlanis Hazirlanis { get; set;}
    }
}