using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BRSApplication.Models
{
    [Table("tbl_EnvGeoMap")]
    public class EnvGeoMap
    {

        public int Id { get; set; }

        // Foreign key   
        [Display(Name = "Environment")]
        public int EnvironmentId { get; set; }


        // Foreign key   
        [Display(Name = "Geo")]
        public int GeoId { get; set; }


        [ForeignKey("EnvironmentId")]
        public virtual Environment Environments { get; set; }

        [ForeignKey("GeoId")]
        public virtual Geos Geos { get; set; }


    }
}