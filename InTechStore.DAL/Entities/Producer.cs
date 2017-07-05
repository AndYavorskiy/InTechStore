using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InTechStore.DAL.Entities
{
    public class Producer
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }

        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public string Image { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public Producer()
        {
            Products = new List<Product>();
        }
    }
}
