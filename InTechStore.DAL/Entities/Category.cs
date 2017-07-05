﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InTechStore.DAL.Entities
{
   public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ProductInfo> ProductsInfo { get; set; }

        public Category()
        {
            ProductsInfo = new List<ProductInfo>();
        }
    }
}
