using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InTechStore.DAL.Entities
{
    public class AddInfo
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
