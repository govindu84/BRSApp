using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace BRSApplication.Models
{
    [Table("tbl_Geos")]
    public class Geos
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(30)]
        [DisplayName("Geo Name")]
        public string GeoName { get; set; }

        public virtual ICollection<EnvGeoMap> EnvGeoMap { get; set; }
        public virtual ICollection<EnvGoBuildBrsMap> EnvGoBuildBrsMap { get; set; }
     
    }
}