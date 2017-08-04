using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InTechStore.DAL.Entities;
using InTechStore.DAL.Interfaces;
using InTechStore.DAL.Repositories;

namespace InTechStore.WEB.Controllers
{
    public class DiscountsController : Controller
    {
        IUnitOfWork _uow;
        IGenericRepository<Discount> _discountRepository;

        public DiscountsController()
        {
            _uow = new EFUnitOfWork();
            _discountRepository = _uow.DiscountRepository;
        }

        public ActionResult Index()
        {
            return View(_discountRepository.GetAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Value")] Discount discount)
        {
            if (ModelState.IsValid)
            {
                discount.Code = Guid.NewGuid().ToString();
                _discountRepository.Create(discount);
                _uow.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(discount);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discount discount = _discountRepository.FindById(id.Value);
            if (discount == null)
            {
                return HttpNotFound();
            }
            return View(discount);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Discount discount = _discountRepository.FindById(id);
            _discountRepository.Remove(discount);
            _uow.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _uow.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
