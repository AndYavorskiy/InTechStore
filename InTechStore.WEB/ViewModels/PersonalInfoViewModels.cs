using InTechStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InTechStore.WEB.ViewModels
{
    public class PersonalInfoViewModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday { get; set; }
        public Gender Gender { get; set; }

        public static PersonalInfoViewModel GetViewModel(PersonalInfo personalInf)
        {
            return new PersonalInfoViewModel()
            {
                FirstName = personalInf.FirstName,
                MiddleName = personalInf.MiddleName,
                LastName = personalInf.LastName,
                Birthday = personalInf.Birthday,
                Gender = personalInf.Gender
            };
        }

        public static PersonalInfo GetModel(PersonalInfoViewModel personalInfVM)
        {
            return new PersonalInfo()
            {
                FirstName = personalInfVM.FirstName,
                MiddleName = personalInfVM.MiddleName,
                LastName = personalInfVM.LastName,
                Birthday = personalInfVM.Birthday,
                Gender = personalInfVM.Gender
            };
        }
    }
}