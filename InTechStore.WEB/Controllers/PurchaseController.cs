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
                    ProductId = item.ProductId,
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
                Product product = _uow.ProductRepository.Get(p => p.Id == order.ProductId).FirstOrDefault();
                if (order.Count > 0 && product.Count >= order.Count)
                {
                    product.Count -= order.Count;

                    if (product.Count == 0)
                    {
                        product.IsInStock = false;
                    }

                    _uow.ProductRepository.Update(product);
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


                double discount = 0;
                Discount _discount = _uow.DiscountRepository.Get(d => d.Code == order.DiscountCode).FirstOrDefault();
                if (_discount != null)
                {
                    discount = _discount.Value;
                }

                decimal sum = (decimal)((double)(product.ProductInfo.Price) * order.Count * (1 - (discount / (double)100)));

                PurchaseInfo purInf = new PurchaseInfo()
                {
                    Id = purchase.Id,
                    Count = order.Count,
                    Discount = discount,
                    Sum =sum
                };
                _uow.PurchaseInfoRepository.Create(purInf);

                _user.Orders.Add(purchase);
                _uow.SaveChanges();


                return View("Successfully");
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

            Purchase purchase = _purchases.Where(p => p.Id == id.Value).FirstOrDefault();

            if (purchase == null)
            {
                return HttpNotFound();
            }

            DeletePurchaseViewModel deletePurchase = new DeletePurchaseViewModel()
            {
                Id = purchase.Id,
                ApplicationUserName = purchase.ApplicationUser.UserName,
                ProductId = purchase.ProductId,
                ProductName = purchase.Product.ProductInfo.Name,
                Count = purchase.PurchaseInfo.Count,
                Discount = purchase.PurchaseInfo.Count,
                Sum = purchase.PurchaseInfo.Sum,
                OrderDate = purchase.OrderDate,
                ReceivingDate = purchase.ReceivingDate,
                DeliveryStatus = purchase.DeliveryStatus
            };

            return View(deletePurchase);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            PurchaseInfo purchaseInfo = _uow.PurchaseInfoRepository.FindById(id);
            _uow.PurchaseInfoRepository.Remove(purchaseInfo);

            Purchase deletedPurchase = _uow.PurchaseRepository.FindById(id);
            _uow.PurchaseRepository.Remove(deletedPurchase);
            _uow.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}