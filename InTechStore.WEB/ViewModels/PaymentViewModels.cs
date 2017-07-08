using InTechStore.DAL.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace InTechStore.WEB.ViewModels
{
    public class PaymentViewModel
    {
        [Required]
        [Display(Name = "Номер карти")]
        [StringLength(16, MinimumLength = 16)]
        public string CardNumber { get; set; }

        [Required]
        [Display(Name = "Місяць/Рік")]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [Required]
        [Display(Name = "CVV код")]
        [StringLength(3, MinimumLength = 3)]
        public string CvvCode { get; set; }

        public static PaymentViewModel GetViewModel(Payment payment)
        {
            return new PaymentViewModel()
            {
                CardNumber = payment.CardNumber,
                Date = payment.Date,
                CvvCode = payment.CvvCode
            };
        }

        public static Payment GetModel(PaymentViewModel paymentVM)
        {
            return new Payment()
            {
                CardNumber = paymentVM.CardNumber,
                Date = paymentVM.Date,
                CvvCode = paymentVM.CvvCode
            };
        }
    }
}