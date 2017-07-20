using InTechStore.DAL.Repositories;
using InTechStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InTechStore.DAL.Entities;
using InTechStore.WEB.ViewModels;
using System.Net;
using InTechStore.WEB.Utils;

namespace InTechStore.WEB.Controllers
{
    public class ProductController : Controller
    {
        IUnitOfWork _uow;
        IEnumerable<Product> _allProducts;
        IEnumerable<Product> _products;
        IEnumerable<Category> _category;
        IEnumerable<Producer> _producers;
        IGenericRepository<ProductInfo> _prodInfRepository;

        public ProductController()
        {
            _uow = new EFUnitOfWork();
            _products = _uow.ProductRepository.Get(p => p.IsInStock == true);
            _allProducts = _uow.ProductRepository.GetAll();
            _category = _uow.CategoryRepository.GetAll();
            _producers = _uow.ProducerRepository.GetAll();

            _prodInfRepository = _uow.ProductInfoRepository;
        }
        // GET: Product
        public ActionResult Index()
        {
            List<ProductInfoViewModel> productInfoViewModel = new List<ProductInfoViewModel>();
            foreach (var item in _products)
            {
                productInfoViewModel.Add(new ProductInfoViewModel() { Id = item.Id, Name = item.ProductInfo.Name, Price = item.ProductInfo.Price, IsInStock = item.IsInStock,Image = item.Image});
            }

            return View(productInfoViewModel);
        }

        public ActionResult ShowAll()
        {
            List<ProductInfoViewModel> productInfoViewModel = new List<ProductInfoViewModel>();

            foreach (var item in _allProducts)
            {
                productInfoViewModel.Add(new ProductInfoViewModel() { Id = item.Id, Name = item.ProductInfo.Name, Price = item.ProductInfo.Price, IsInStock = item.IsInStock });
            }

            return View("Index", productInfoViewModel);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = _allProducts.Where(p => p.Id == id.Value).FirstOrDefault();

            if (product == null)
            {
                return HttpNotFound();
            }

            DetailsProductViewModel detailProdVM = new DetailsProductViewModel()
            {
                Id = product.Id,
                Name = product.ProductInfo.Name,
                Count = product.Count,
                Price = product.ProductInfo.Price,
                Description = product.ProductInfo.Description,
                SerialNumber = product.ProductInfo.SerialNumber,
                CategoryName = product.ProductInfo.Category.Name,
                ManufactureDate = product.ProductInfo.ManufactureDate,
                LastDate = product.ProductInfo.LastDate,
                ProducerName = product.Producer.CompanyName,
                Image = product.Image
            };

            return View(detailProdVM);
        }

        [HttpGet]
        public ActionResult Create()
        {
            Dictionary<string, string> categoryList = new Dictionary<string, string>();
            foreach (var item in _category)
            {
                categoryList.Add(Convert.ToString(item.Id), item.Name);
            }

            Dictionary<string, string> producersList = new Dictionary<string, string>();
            foreach (var item in _producers)
            {
                producersList.Add(Convert.ToString(item.Id), item.CompanyName);
            }

            ViewBag.Categories = new SelectList(categoryList, "Key", "Value");
            ViewBag.Producers = new SelectList(producersList, "Key", "Value");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateProductViewModel createdProduct, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product()
                {
                    Count = createdProduct.Count,
                    IsInStock = true,
                    AddDate = DateTime.Now,
                    Producer = _uow.ProducerRepository.FindById(createdProduct.ProducerId.Value)
                };

                HttpPostedFileBase file2 = Request.Files["file"] as HttpPostedFileBase;

                product.Image = ImageControll.SaveImage(file, ImageType.Product);


                _uow.ProductRepository.Create(product);
                _uow.SaveChanges();

                ProductInfo productInfo = new ProductInfo()
                {
                    Id = product.Id,
                    Name = createdProduct.Name,
                    SerialNumber = Guid.NewGuid().ToString(),
                    Price = createdProduct.Price,
                    Description = createdProduct.Description,
                    ManufactureDate = createdProduct.ManufactureDate,
                    LastDate = createdProduct.LastDate,
                    Category = _uow.CategoryRepository.FindById(createdProduct.CategoryId.Value)
                };

                _prodInfRepository.Create(productInfo);
                _uow.SaveChanges();

                return RedirectToAction("Index", "Product");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = _allProducts.Where(p => p.Id == id.Value).FirstOrDefault();

            if (product == null)
            {
                return HttpNotFound();
            }

            DeleteProductViewModel deleteProd = new DeleteProductViewModel()
            {
                Id = product.Id,
                Name = product.ProductInfo.Name,
                Count = product.Count,
                Price = product.ProductInfo.Price,
                Description = product.ProductInfo.Description,
                SerialNumber = product.ProductInfo.SerialNumber,
                Image = product.Image
            };

            //TODO: каскадне видалення, видаляти інші записи де зустрічається продукт
            //Товар не видаляти, помічати як unavailable
            return View(deleteProd);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            ProductInfo prodInf = _uow.ProductInfoRepository.FindById(id);
            _uow.ProductInfoRepository.Remove(prodInf);
            _uow.SaveChanges();

            Product deletedProd = _uow.ProductRepository.FindById(id);
            _uow.ProductRepository.Remove(deletedProd);
            _uow.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = _allProducts.Where(p => p.Id == id.Value).FirstOrDefault();

            if (product == null)
            {
                return HttpNotFound();
            }

            EditProductViewModel editProd = new EditProductViewModel()
            {
                Id = product.Id,
                Name = product.ProductInfo.Name,
                Count = product.Count,
                Price = product.ProductInfo.Price,
                Description = product.ProductInfo.Description,
                SerialNumber = product.ProductInfo.SerialNumber,
                Image = product.Image
            };

            Dictionary<string, string> categoryList = new Dictionary<string, string>();
            foreach (var item in _category)
            {
                categoryList.Add(Convert.ToString(item.Id), item.Name);
            }

            Dictionary<string, string> producersList = new Dictionary<string, string>();
            foreach (var item in _producers)
            {
                producersList.Add(Convert.ToString(item.Id), item.CompanyName);
            }

            int categoryId = 0;
            foreach (var item in categoryList.Keys)
            {
                categoryId++;
                if (item == Convert.ToString(product.ProductInfo.CategoryId))
                {
                    break;
                }
            }

            int producerId = 0;
            foreach (var item2 in producersList.Keys)
            {
                producerId++;
                if (item2 == Convert.ToString(product.ProducerId))
                {
                    break;
                }
            }

            ViewBag.Categories = new SelectList(categoryList, "Key", "Value", categoryId);
            ViewBag.Producers = new SelectList(producersList, "Key", "Value", producerId);

            return View(editProd);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditProductViewModel editableProductVM)
        {
            if (ModelState.IsValid)
            {
                ProductInfo prodInf = _uow.ProductInfoRepository.FindById(editableProductVM.Id);
                prodInf.Name = editableProductVM.Name;
                prodInf.SerialNumber = editableProductVM.SerialNumber;
                prodInf.Description = editableProductVM.Description;
                prodInf.Price = editableProductVM.Price;
                prodInf.ManufactureDate = editableProductVM.ManufactureDate;
                prodInf.LastDate = editableProductVM.LastDate;
                prodInf.Category = _uow.CategoryRepository.FindById(editableProductVM.CategoryId.Value);

                _uow.ProductInfoRepository.Update(prodInf);

                HttpPostedFileBase file = Request.Files["file"] as HttpPostedFileBase;

                Product prod = _uow.ProductRepository.FindById(editableProductVM.Id);
                prod.Image = ImageControll.SaveImage(file, ImageType.Product);
                prod.Count = editableProductVM.Count;
                if (prod.Count > 0)
                {
                    prod.IsInStock = true;
                }
                prod.Producer = _uow.ProducerRepository.FindById(editableProductVM.ProducerId.Value);

                _uow.ProductRepository.Update(prod);
                _uow.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(editableProductVM);
        }
    }
}