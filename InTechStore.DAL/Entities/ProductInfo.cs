using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InTechStore.DAL.Entities
{
    public class ProductInfo
    {
        [Key]
        [ForeignKey("Product")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime? ManufactureDate { get; set; }
        public DateTime? LastDate { get; set; }

        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual Product Product { get; set; }

    }
}