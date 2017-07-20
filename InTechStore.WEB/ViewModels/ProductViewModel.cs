using InTechStore.DAL.Entities;
using System;

namespace InTechStore.WEB.ViewModels
{
    public class CreateProductViewModel
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public int? CategoryId { get; set; }
        public int? ProducerId { get; set; }

        public DateTime? ManufactureDate { get; set; }
        public DateTime? LastDate { get; set; }

        //TODO: (create product) Add image
    }

    public class ProductInfoViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsInStock { get; set; }
        public string Image { get; set; }

    }

    public class DetailsProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public string CategoryName { get; set; }
        public string ProducerName { get; set; }

        public DateTime? ManufactureDate { get; set; }
        public DateTime? LastDate { get; set; }
        public string Image { get; set; }

    }


    public class DeleteProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

    }


    public class EditProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public int? CategoryId { get; set; }
        public int? ProducerId { get; set; }

        public DateTime? ManufactureDate { get; set; }
        public DateTime? LastDate { get; set; }
        public string Image { get; set; }

    }

}