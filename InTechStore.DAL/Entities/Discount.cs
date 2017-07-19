using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InTechStore.DAL.Entities
{
    public class Discount
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public string Code { get; set; }
    }
}
