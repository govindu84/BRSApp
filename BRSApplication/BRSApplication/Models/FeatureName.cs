using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BRSApplication.Models
{
    [Table("tbl_FeatureName")]
    public class FeatureNames
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FeatureName { get; set; }

        public virtual ICollection<BRSRequest> BRSRequest { get; set; }
    }
}