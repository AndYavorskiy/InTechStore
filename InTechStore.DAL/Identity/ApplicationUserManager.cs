using Microsoft.AspNet.Identity;
using InTechStore.DAL.Entities;

namespace InTechStore.DAL.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
                : base(store)
        {
        }
    }
}
