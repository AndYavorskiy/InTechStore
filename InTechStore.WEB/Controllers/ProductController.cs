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

namespace InTechStore.WEB.Controllers
{
    public class ProductController : Controller
    {
        IUnitOfWork _uow;
        IEnumerable<Product> _products;
        IEnumerable<Category> _category;
        IEnumerable<Producer> _producers;
        IGenericRepository<ProductInfo> _prodInfRepository;

        public ProductController()
        {
            _uow = new EFUnitOfWork();
            _products = _uow.ProductRepository.GetAll();
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
                productInfoViewModel.Add(new ProductInfoViewModel() { Id = item.Id, Name = item.ProductInfo.Name, Price = item.ProductInfo.Price });
            }

            return View(productInfoViewModel);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = _products.Where(p => p.Id == id.Value).FirstOrDefault();

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
                Category = product.ProductInfo.Category,
                ManufactureDate = product.ProductInfo.ManufactureDate,
                LastDate = product.ProductInfo.LastDate,
                Producer = product.Producer
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
        public ActionResult Create(CreateProductViewModel createdProduct)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product()
                {
                    Count = createdProduct.Count,
                    AddDate = DateTime.Now,
                    Producer = _uow.ProducerRepository.FindById(createdProduct.ProducerId.Value)
                };

                _uow.ProductRepository.Create(product);
                _uow.SaveChanges();

                ProductInfo productInfo = new ProductInfo()
                {
                    Id = product.Id,
                    Name = createdProduct.Name,
                    SerialNumber = createdProduct.SerialNumber,
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

            Product product = _products.Where(p => p.Id == id.Value).FirstOrDefault();

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
            };

            return View(deleteProd);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            ProductInfo prodInf = _uow.ProductInfoRepository.FindById(id);
            _uow.ProductInfoRepository.Remove(prodInf);

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

            Product product = _products.Where(p => p.Id == id.Value).FirstOrDefault();

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
            };

            return View(editProd);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditProductViewModel editableWorkDayTimesVM)
        {
            if (ModelState.IsValid)
            {
                ProductInfo prodInf = _uow.ProductInfoRepository.FindById(editableWorkDayTimesVM.Id);
                prodInf.Name = editableWorkDayTimesVM.Name;
                prodInf.SerialNumber = editableWorkDayTimesVM.SerialNumber;
                prodInf.Description = editableWorkDayTimesVM.Description;
                prodInf.Price = editableWorkDayTimesVM.Price;
                prodInf.ManufactureDate = editableWorkDayTimesVM.ManufactureDate;
                prodInf.LastDate = editableWorkDayTimesVM.LastDate;

                _uow.ProductInfoRepository.Update(prodInf);

                Product prod = _uow.ProductRepository.FindById(editableWorkDayTimesVM.Id);
                prod.Count = editableWorkDayTimesVM.Count;

                _uow.ProductRepository.Update(prod);
                _uow.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(editableWorkDayTimesVM);
        }
    }
}