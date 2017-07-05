using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InTechStore.DAL.Entities
{
    public class Purchase
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }

        public virtual PurchaseInfo PurchaseInfo { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime ReceivingDate { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
    }

    public enum DeliveryStatus
    {
        Processing,
        IsGoingToSend,
        InTheWay,
        AtTheDeliveryPoint,
        Delivered
    }

}