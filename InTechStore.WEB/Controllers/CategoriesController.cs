using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InTechStore.DAL.Entities;
using InTechStore.WEB.ViewModels;
using InTechStore.DAL.Interfaces;
using InTechStore.DAL.Repositories;

namespace InTechStore.WEB.Controllers
{
    public class CategoriesController : Controller
    {
        IUnitOfWork _uow;
        IGenericRepository<Category> _categoryRepository;

        public CategoriesController()
        {
            _uow = new EFUnitOfWork();
            _categoryRepository = _uow.CategoryRepository;
        }

        // GET: Categories
        public ActionResult Index()
        {
            List<DetailsCategoryViewModel> categories = new List<DetailsCategoryViewModel>();
            foreach (var item in _categoryRepository.GetAll())
            {
                categories.Add(new DetailsCategoryViewModel() { Id = item.Id, Name = item.Name });
            }


            return View(categories);
        }

        // GET: Categories/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Category category = _categoryRepository.FindById(id.Value);
        //    if (category == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(category);
        //}

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                Category categ = new Category() { Name = category.Name };
                _categoryRepository.Create(categ);
                _uow.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _categoryRepository.FindById(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            EditeCategoryViewModel editeCategory = new EditeCategoryViewModel()
            {
                Id = category.Id,
                Name = category.Name
            };
            return View(editeCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] EditeCategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                Category categ = _categoryRepository.GetAll().Where(c => c.Id == category.Id).FirstOrDefault();
                if (categ == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotModified);
                }
                categ.Name = category.Name;
                _categoryRepository.Update(categ);
                _uow.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _categoryRepository.FindById(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = _categoryRepository.FindById(id);
            _categoryRepository.Remove(category);
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
