using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InTechStore.DAL.Entities
{
    public class PersonalInfo
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday { get; set; }
        public Gender Gender { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
