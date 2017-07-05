using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InTechStore.DAL.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public DateTime AddDate { get; set; }

        public virtual ProductInfo ProductInfo { get; set; }
        public string Image { get; set; }

        public int? ProducerId { get; set; }
        public virtual Producer Producer { get; set; }

        public virtual ICollection<ApplicationUser> Customers { get; set; } = new List<ApplicationUser>();
    }
}
