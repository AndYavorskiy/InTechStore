using InTechStore.DAL.Entities;
using InTechStore.DAL.Interfaces;
using InTechStore.DAL.Repositories;
using InTechStore.WEB.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InTechStore.WEB.Controllers
{
    public class PersonalInfoController : Controller
    {
        IUnitOfWork _uow;
        IGenericRepository<PersonalInfo> _personInfRepository;
        ApplicationUser _user;

        public PersonalInfoController()
        {

            _uow = new EFUnitOfWork();
            _personInfRepository = _uow.PersonalInfoRepository; 
            _user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
        }

        public ActionResult Details()
        {
            if (_user.PersonalInfo == null)
            {
                return View("NotSet");
            }

            PersonalInfoViewModel personalInfoVM = PersonalInfoViewModel.GetViewModel(_user.PersonalInfo);
            return View(personalInfoVM);
        }

        [HttpGet]
        public ActionResult Set()
        {
            //TODO: зробити в моделі поле IsPersonalInfoConfirmed
            return View();
        }

        [HttpPost]
        public ActionResult Set(PersonalInfoViewModel setPersonalInfo)
        {
            if (ModelState.IsValid)
            {
                PersonalInfo personalInfo = PersonalInfoViewModel.GetModel(setPersonalInfo);
                personalInfo.Id = _user.Id;

                //TODO: задати _user.IsPersonalInfoConfirmed = true;
                _uow.PersonalInfoRepository.Create(personalInfo);
                _uow.SaveChanges();

                return RedirectToAction("Index", "Manage");
            }

            return View(setPersonalInfo);
        }

        [HttpGet]
        public ActionResult Edit()
        {
            PersonalInfoViewModel personalInfoVM = PersonalInfoViewModel.GetViewModel(_user.PersonalInfo);

            return View(personalInfoVM);
        }


        [HttpPost]
        public ActionResult Edit(PersonalInfoViewModel personalInfoVM)
        {
            if (ModelState.IsValid)
            {
                _user.PersonalInfo = PersonalInfoViewModel.GetModel(personalInfoVM);
                _user.PersonalInfo.Id = _user.Id;

                _uow.PersonalInfoRepository.Update(_user.PersonalInfo);
                _uow.SaveChanges();

                return RedirectToAction("Details", "PersonalInfo");
            }
            return View(personalInfoVM);
        }
    }
}