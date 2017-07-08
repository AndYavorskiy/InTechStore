using System.Data.Entity;

namespace InTechStore.DAL.Entities
{
    public class DBInicializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {

            base.Seed(context);
        }
    }
}
