using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InTechStore.WEB.ViewModels
{
    public class DetailsCategoryViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Назва категорії")]
        public string Name { get; set; }
    }

    public class CreateCategoryViewModel
    {
        [Required]
        [Display(Name = "Назва категорії")]
        public string Name { get; set; }
    }
}