using InTechStore.DAL.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace InTechStore.WEB.ViewModels
{
    public class CreateProductViewModel
    {
        [Required(ErrorMessage = "Назву товару не вказано")]
        [StringLength(maximumLength: 30)]
        [Display(Name ="Назва")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Кількість не вказано")]
        [Range(0,10000,ErrorMessage = "Недопустиме значення. Вкажіть від 0 до 10000")]
        [Display(Name ="Кількість")]
        public int Count { get; set; }

        [Required(ErrorMessage ="Ціну не вказано")]
        [Range(0,100000, ErrorMessage = "Недопустиме значення. Вкажіть від 0 до 100000")]
        [Display(Name ="Ціна")]
        public decimal Price { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name ="Опис")]
        public string Description { get; set; }

        [Display(Name ="Зображення")]
        public string Image { get; set; }

        [Required]
        [Display(Name ="Категорія")]
        public int? CategoryId { get; set; }
        [Required]
        [Display(Name ="Виробник")]
        public int? ProducerId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name ="Дата виготовлення")]
        public DateTime? ManufactureDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name ="Термін придатності")]
        public DateTime? LastDate { get; set; }
    }

    public class ProductInfoViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Назва")]
        public string Name { get; set; }
        [Display(Name = "Ціна")]
        public decimal Price { get; set; }
        [Display(Name = "Доступно для замовлення")]
        public bool IsInStock { get; set; }
        [Display(Name = "Зображення")]
        public string Image { get; set; }
    }

    public class AllProductsInfoViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Назва")]
        public string Name { get; set; }

        [Display(Name = "Ціна")]
        public decimal Price { get; set; }

        [Display(Name = "Доступно")]
        public bool IsInStock { get; set; }

        [Display(Name = "Зображення")]
        public string Image { get; set; }

        [Display(Name = "Кількість")]
        public int Count { get; set; }

        [Display(Name = "Серійний номер")]
        public string SerialNumber { get; set; }

        [Display(Name = "Категор Id")]
        public int CategoryId { get; set; }

        [Display(Name = "Вироб Id")]
        public int ProducerId { get; set; }

        [Display(Name = "Дата вигот")]
        public DateTime? ManufactureDate { get; set; }

        [Display(Name = "Термін прид")]
        public DateTime? LastDate { get; set; }

    }

    public class DetailsProductViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Назва")]
        public string Name { get; set; }

        [Display(Name = "Ціна")]
        public decimal Price { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Опис")]
        public string Description { get; set; }

        [Display(Name = "Категорія")]
        public string CategoryName { get; set; }

        [Display(Name = "Виробник")]
        public string ProducerName { get; set; }

        [Display(Name = "Дата виготовлення")]
        public DateTime? ManufactureDate { get; set; }

        [Display(Name = "Термін придатності")]
        public DateTime? LastDate { get; set; }

        [Display(Name = "Зображення")]
        public string Image { get; set; }
    }


    public class DeleteProductViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Назва")]
        public string Name { get; set; }

        [Display(Name = "Ціна")]
        public decimal Price { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Опис")]
        public string Description { get; set; }

        [Display(Name = "Категорія")]
        public string CategoryName { get; set; }
        [Display(Name = "Виробник")]
        public string ProducerName { get; set; }

        [Display(Name = "Зображення")]
        public string Image { get; set; }

        [Display(Name = "Серійний номер")]
        public string SerialNumber { get; set; }

        [Display(Name = "Кількість")]
        public int Count { get; set; }

    }


    public class EditProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Назву товару не вказано")]
        [StringLength(maximumLength: 30)]
        [Display(Name = "Назва")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Кількість не вказано")]
        [Range(0, 10000, ErrorMessage = "Недопустиме значення. Вкажіть від 0 до 10000")]
        [Display(Name = "Кількість")]
        public int Count { get; set; }

        [Required(ErrorMessage = "Ціну не вказано")]
        [Range(0, 100000, ErrorMessage = "Недопустиме значення. Вкажіть від 0 до 100000")]
        [Display(Name = "Ціна")]
        public decimal Price { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Опис")]
        public string Description { get; set; }

        [Display(Name = "Серійний номер")]
        public string SerialNumber { get; set; }

        [Display(Name = "Зображення")]
        public string Image { get; set; }

        [Required]
        [Display(Name = "Категорія")]
        public int? CategoryId { get; set; }
        [Required]
        [Display(Name = "Виробник")]
        public int? ProducerId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="yyyy-mm-dd")]
        [Display(Name = "Дата виготовлення")]
        public DateTime? ManufactureDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "yyyy-mm-dd")]
        [Display(Name = "Термін придатності")]
        public DateTime? LastDate { get; set; }

    }

}