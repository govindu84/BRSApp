using BRSApplication.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BRSApplication.ViewModels
{
    public enum RoleData { Requestor = 0, Approver = 1, SuperAdmin = 2 };
    public class BRSRequestViewModel
    {

        public BRSRequestViewModel()
        {
            GeosForProds = new List<Geos>();
            EnvsForFilter = new List<Environment>();
            GeosForFilter = new List<Geos>();
        }
        //public IEnumerable<Environment> Environments { get; set; }
        //public IEnumerable<GeosForProd> GeosForProds { get; set; }

         
        public List<Environment> Environments { get; set; }
        public List<Geos> GeosForProds { get; set; }

        public List<EnvGeoMap> EnvGeoMap { get; set; }

        public List<Slots> Slots { get; set; }

        public List<Build> Builds { get; set; }

        [Required(ErrorMessage = "Select any slot")]
        public int SlotId { get; set; }

        [Required(ErrorMessage = "Select any Environment")]
        public int EnvId { get; set; }
        public int EnvIdForFilter { get; set; }

        [Required(ErrorMessage = "Select any Geo")]
        public int[] GeoIds { get; set; }



        public int BuildId { get; set; }

        public string BuildName { get; set; }

        // public System.DateTime ExpiryDateValue { get; set; }

        public System.DateTime? ExpiryDateValue { get; set; }
        public string Comments { get; set; }

        public int[] GeoIdsForFilter { get; set; }

        public List<Geos> GeosForFilter { get; set; }
        public List<Environment> EnvsForFilter { get; set; }
        
        public int BuildIdsForFilter { get; set; }
      

        public FeatureAreas FeatureArea { get; set; }

        public int FeatureAreaId { get; set; }

        public int FeatureNameId { get; set; }
        public IEnumerable<FeatureAreas> FeatureAreas { get; set; }
        
        public FeatureNames FeatureName { get; set; }

        public IEnumerable<FeatureNames> FeatureNames { get; set; }
      
        public int SettingId { get; set; }
        public Setting Setting { get; set; }
       
        public string Value { get; set; }
        public List<Setting> Settings { get; set; }
        public BRSRequest BRSRequest { get; set; }
        public int BRSRequestEditId { get; set; }
       // public Value Value { get; set; }

        public IEnumerable<BRSRequest> BRSRequestDetails { get; set; }
        public UserInfo userInfo { get; set; }
    }

    public class UserInfo
    {
        public RoleData RoleType { get; set; }
        public string Username { get; set; }
    }

        public class AuthenticationRoleClass
    {
      
        public  Dictionary<RoleData, string> RoleInfo { get; set; }
        public AuthenticationRoleClass()
        {
            RoleInfo = new Dictionary<RoleData, string>();
            RoleInfo.Add(RoleData.Requestor, "dekulk");
            RoleInfo.Add(RoleData.Approver, "raghuk");
            RoleInfo.Add(RoleData.SuperAdmin, "prandala");
        }
    }
}