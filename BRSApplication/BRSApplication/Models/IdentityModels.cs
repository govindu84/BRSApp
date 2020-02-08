using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BRSApplication.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }


        public DbSet<Environment> Environment { get; set; }
        public DbSet<Geos> Geos { get; set; }
        public DbSet<EnvGeoMap> EnvGeoMap { get; set; }
        public DbSet<Slots> Slot { get; set; }
        public DbSet<Build> Build { get; set; }
        public DbSet<FeatureAreas> FeatureArea { get; set; }
        public DbSet<FeatureNames> FeatureName { get; set; }
        public DbSet<Setting> Setting { get; set; }
        public DbSet<Value> Value { get; set; }
        public DbSet<BRSRequest> BrsRequest { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
