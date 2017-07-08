using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace InTechStore.DAL.Entities
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        static ApplicationDbContext()
        {
            //Database.SetInitializer<ApplicationDbContext>(new DBInicializer());
        }
        public ApplicationDbContext()
            : base("InTechStoreDbConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<PersonalInfo> PersonsInfo { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductInfo> ProductsInfo { get; set; }
        public DbSet<Category> Categorys { get; set; }


        public DbSet<Producer> Producers { get; set; }

        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseInfo> PurchasesInfo { get; set; }
    }
}
