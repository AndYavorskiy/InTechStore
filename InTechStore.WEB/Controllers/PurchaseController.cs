using InTechStore.DAL.Entities;
using InTechStore.DAL.Interfaces;
using InTechStore.DAL.Repositories;
using InTechStore.WEB.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace InTechStore.WEB.Controllers
{
    public class PurchaseController : Controller
    {
        IUnitOfWork _uow;
        IGenericRepository<Purchase> _purchaseRepository;
        InTechStore.DAL.Identity.ApplicationUserManager userMan;

        IEnumerable<Purchase> _purchases;
        IEnumerable<Product> _products;
        ApplicationUser _user;

        public PurchaseController()
        {
            _uow = new EFUnitOfWork();
            _purchaseRepository = _uow.PurchaseRepository;
            userMan = _uow.UserManager;

            _purchases = _purchaseRepository.GetAll();
            _products = _uow.ProductRepository.GetAll();
            _user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
        }

        // GET: Purchase
        public ActionResult Index()
        {
            List<IndexPurchaseViewModel> ordersList = new List<IndexPurchaseViewModel>();

            foreach (var item in _purchases)
            {
                ordersList.Add(new IndexPurchaseViewModel()
                {
                    Id = item.Id,
                    ApplicationUserName = item.ApplicationUser.UserName,
                    ProductId = item.ProductId.Value,
                    ProductName = item.Product.ProductInfo.Name,
                    Count = item.PurchaseInfo.Count,
                    Discount = item.PurchaseInfo.Discount,
                    Sum = item.PurchaseInfo.Sum,
                    OrderDate = item.OrderDate,
                    ReceivingDate = item.ReceivingDate,
                    DeliveryStatus = item.DeliveryStatus
                });
            }
            return View(ordersList);
        }

        [HttpGet]
        public ActionResult Make(int? id)
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

            MakePurchaseViewModel makePurchase = new MakePurchaseViewModel()
            {
                Product = product,
                ProductId = product.Id,
                Count = 1
            };

            return View(makePurchase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Make(MakePurchaseViewModel order)
        {
            if (ModelState.IsValid)
            {
                Product product = _products.Where(p => p.Id == order.ProductId).FirstOrDefault();
                if ( order.Count>0 && product.Count>=order.Count)
                {
                    product.Count -= order.Count;
                    _uow.SaveChanges();

                }
                else
                {
                    ModelState.AddModelError("Count", "Невірна кількість товару");
                    order.Product = product;
                    return View(order);
                }

                Purchase purchase = new Purchase()
                {
                    ApplicationUserId = _user.Id,
                    Product = _uow.ProductRepository.FindById(order.ProductId),
                    OrderDate = DateTime.Now,
                    DeliveryStatus = DeliveryStatus.IsGoingToSend
                };
                _uow.PurchaseRepository.Create(purchase);
                _uow.SaveChanges();

                int discount = 0;
                if (order.DiscountCode == "1234567890")
                {
                    discount = 15;
                }


                PurchaseInfo purInf = new PurchaseInfo()
                {
                    Id = purchase.Id,
                    Count = order.Count,
                    Discount = discount,
                    Sum = product.ProductInfo.Price - product.ProductInfo.Price * discount
                };
                _uow.PurchaseInfoRepository.Create(purInf);

                _user.Orders.Add(purchase);
                _uow.SaveChanges();


                return View("Successfully");
            }

            return View();
        }
    }
}