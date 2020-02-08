using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BRSApplication.Models
{
    [Table("tbl_Setting")]
    public class Setting
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

     
        public string SettingValue { get; set; }
        public DateTime LastUpdatedOn { get; set; }

        public virtual ICollection<BRSRequest> BRSRequest { get; set; }
    }
}