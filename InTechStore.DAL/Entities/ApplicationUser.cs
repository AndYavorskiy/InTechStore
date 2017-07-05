using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace InTechStore.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual PersonalInfo PersonalInfo { get; set; }
        public virtual AddInfo AddInfo { get; set; }
        public virtual Payment Payment { get; set; }
        public string Image { get; set; }

        public virtual ICollection<Purchase> Orders { get; set; } = new List<Purchase>();

    }
}
