using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InTechStore.DAL.Entities;
using InTechStore.WEB.Utils;
using InTechStore.DAL.Interfaces;
using InTechStore.DAL.Repositories;

namespace InTechStore.WEB.Controllers
{
    public class ProducersController : Controller
    {
        IUnitOfWork _uow;
        IGenericRepository<Producer> _producerRepository;
        IEnumerable<Producer> _producers;
        public ProducersController()
        {
            _uow = new EFUnitOfWork();
            _producerRepository = _uow.ProducerRepository;
            _producers = _producerRepository.GetAll();
        }

        // GET: Producers
        public ActionResult Index()
        {
            return View(_producers);
        }

        // GET: Producers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producer producer = _producerRepository.FindById(id.Value);
            if (producer == null)
            {
                return HttpNotFound();
            }
            return View(producer);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CompanyName,PhoneNumber,Address,Image")] Producer producer, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file2 = Request.Files["file"] as HttpPostedFileBase;
                producer.Image = ImageControll.SaveImage(file, ImageType.Producer);

               _producerRepository.Create(producer);
                _uow.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(producer);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producer producer = _producerRepository.FindById(id.Value);
            if (producer == null)
            {
                return HttpNotFound();
            }
            return View(producer);
        }

        // POST: Producers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CompanyName,PhoneNumber,Address")] Producer producer)
        {
            if (ModelState.IsValid)
            {
                Producer prod = _producerRepository.FindById(producer.Id);
                prod.CompanyName = producer.CompanyName;
                prod.Address = producer.Address;
                prod.PhoneNumber = producer.PhoneNumber;

                HttpPostedFileBase file = Request.Files["file"] as HttpPostedFileBase;
                if (!string.IsNullOrEmpty(file.FileName))
                {
                    prod.Image = ImageControll.SaveImage(file, ImageType.Producer);
                }
                _producerRepository.Update(prod);
                _uow.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(producer);
        }

        // GET: Producers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producer producer = _producerRepository.FindById(id.Value);
            if (producer == null)
            {
                return HttpNotFound();
            }
            return View(producer);
        }

        // POST: Producers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producer producer = _producerRepository.FindById(id);
            _producerRepository.Remove(producer);
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
