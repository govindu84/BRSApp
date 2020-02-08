using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BRSApplication.Models
{
    [Table("tbl_BRSRequest")]
    public class BRSRequest
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string RequestedAlias { get; set; }

        public DateTime? RequestedDate { get; set; }

        public int EnvironmentId { get; set; }

        public string GeosId { get; set; }

        public int SlotId { get; set; }

        public int BuildId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ExpiryDate { get; set; } /*= System.DateTime.Now.Date;*/

        [StringLength(200)]
        public string Comments { get; set; }

        public int FeatureAreaId { get; set; }

        public int FeatureNameId { get; set; }

        public int SettingId { get; set; }

        [StringLength(50)]
        public string ApproverAlias { get; set; }
        
       
        public string Value { get; set; }

        public DateTime? ApproveDate { get; set; }

        [ForeignKey("BuildId")]
        public virtual Build Build { get; set; }

        [ForeignKey("EnvironmentId")]
        public virtual Environment Environment { get; set; }

        [ForeignKey("SlotId")]
        public virtual Slots Slot { get; set; }

        [ForeignKey("FeatureAreaId")]
        public virtual FeatureAreas FeatureArea { get; set; }

        [ForeignKey("FeatureNameId")]
        public virtual FeatureNames FeatureName { get; set; }

        [ForeignKey("SettingId")]
        public virtual Setting Setting { get; set; }

        public virtual ICollection<EnvGoBuildBrsMap> EnvGoBuildBrsMap { get; set; }
      


    }
}