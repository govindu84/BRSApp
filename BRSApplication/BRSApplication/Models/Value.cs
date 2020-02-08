using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BRSApplication.Models
{
    [Table("tbl_Value")]
    public class Value
    {
        public int Id { get; set; }
        
        
        [Required]
        [StringLength(50)]
        public string ValueName { get; set; }
    }
}