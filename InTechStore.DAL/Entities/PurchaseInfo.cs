using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InTechStore.DAL.Entities
{
    public class PurchaseInfo
    {
        [Key]
        [ForeignKey("Purchase")]
        public int Id { get; set; }

        public int Count { get; set; }
        public double Discount { get; set; }
        public decimal Sum { get; set; }

        public virtual Purchase Purchase { get; set; }

    }
}