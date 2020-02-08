using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BRSApplication.Models
{
    [Table("tbl_EnvGoBuildBrsMap")]
    public class EnvGoBuildBrsMap
    {
        public EnvGoBuildBrsMap()
        {
            IsActive = true;
        }

        [Key]
        public int Id { get; set; }
       
        [Required]
        [Display(Name = "EnvironmentId")]
        public int EnvironmentId { get; set; }

        [Required]
        // Foreign key   
        [Display(Name = "GeoId")]
        public int GeoId { get; set; }

        [Required]
        [Display(Name = "BuildId")]
        public int BuildId { get; set; }

        [Required]
        [Display(Name = "BrsRequestId")]
        public int BrsRequestId { get; set; }

        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }

        [ForeignKey("GeoId")]
        public virtual Geos Geos { get; set; }

        [Required]

        [ForeignKey("BuildId")]
        public virtual Build Build { get; set; }

        [ForeignKey("EnvironmentId")]
        public virtual Environment Environment { get; set; }

        [ForeignKey("BrsRequestId")]
        public virtual BRSRequest BRSRequest { get; set; }
    }
}