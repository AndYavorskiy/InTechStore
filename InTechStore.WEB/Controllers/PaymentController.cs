using InTechStore.DAL.Entities;
using InTechStore.DAL.Interfaces;
using InTechStore.DAL.Repositories;
using InTechStore.WEB.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;

namespace InTechStore.WEB.Controllers
{
    public class PaymentController : Controller
    {
        IUnitOfWork _uow;
        IGenericRepository<Payment> _paymentRepository;
        ApplicationUser _user;

        public PaymentController()
        {
            _uow = new EFUnitOfWork();
            _paymentRepository = _uow.PaymentRepository;
            _user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
        }

        public ActionResult Details()
        {
            if (_user.Payment == null)
            {
                return View("NotSet");
            }

            PaymentViewModel paymentVM = PaymentViewModel.GetViewModel(_user.Payment);
            return View(paymentVM);
        }

        [HttpGet]
        public ActionResult Set()
        {
            //TODO: зробити в моделі поле IsPaymentConfirmed
            return View();
        }

        [HttpPost]
        public ActionResult Set(PaymentViewModel paymentVM)
        {
            if (ModelState.IsValid)
            {
                Payment payment = PaymentViewModel.GetModel(paymentVM);
                payment.Id = _user.Id;

                //TODO: задати _user.IsPaymentConfirmed = true;
                _uow.PaymentRepository.Create(payment);
                _uow.SaveChanges();

                return RedirectToAction("Index", "Manage");
            }

            return View(paymentVM);
        }

        [HttpGet]
        public ActionResult Edit()
        {
            PaymentViewModel paymentVM = PaymentViewModel.GetViewModel(_user.Payment);

            return View(paymentVM);
        }

        [HttpPost]
        public ActionResult Edit(PaymentViewModel paymentVM)
        {
            if (ModelState.IsValid)
            {
                _user.Payment = PaymentViewModel.GetModel(paymentVM);
                _user.Payment.Id = _user.Id;

                _uow.PaymentRepository.Update(_user.Payment);
                _uow.SaveChanges();

                return RedirectToAction("Details", "Payment");
            }
            return View(paymentVM);
        }
    }
}