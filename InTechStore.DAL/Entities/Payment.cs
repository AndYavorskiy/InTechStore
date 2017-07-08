using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InTechStore.DAL.Entities
{
    public class Payment
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public string CardNumber { get; set; }
        public DateTime? Date { get; set; }
        public string CvvCode { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}