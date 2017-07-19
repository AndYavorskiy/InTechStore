using InTechStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InTechStore.WEB.ViewModels
{
    public class IndexPurchaseViewModel
    {
        public int Id { get; set; }

        public string ApplicationUserName { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public int Count { get; set; }
        public double Discount { get; set; }
        public decimal Sum { get; set; }

        public DateTime? OrderDate { get; set; }
        public DateTime? ReceivingDate { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
    }

    public class MakePurchaseViewModel
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public string DiscountCode { get; set; }
    }
    public class DeletePurchaseViewModel
    {
        public int Id { get; set; }

        public string ApplicationUserName { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public int Count { get; set; }
        public double Discount { get; set; }
        public decimal Sum { get; set; }

        public DateTime? OrderDate { get; set; }
        public DateTime? ReceivingDate { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
    }
}