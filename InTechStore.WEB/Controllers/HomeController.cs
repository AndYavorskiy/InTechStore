using InTechStore.DAL.Entities;
using InTechStore.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InTechStore.WEB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

       // public async Task<ActionResult> About()
        public ActionResult About()
        {
            //EFUnitOfWork uow = new EFUnitOfWork();
            //ApplicationUser user = await uow.UserManager.FindByNameAsync(User.Identity.Name);

            //PersonalInfo pi = new PersonalInfo() { Id = user.Id, FirstName = "Андрій", MiddleName = "Васильович", LastName = "Яворський", Birthday = new DateTime(1998, 06, 21), Gender = Gender.Male };
            //AddInfo addin = new AddInfo() { Id=user.Id,PhoneNumber="+380988818175",Address="Зарічне"};
            //Payment p = new Payment() { Id=user.Id, CardNumber="2106351245981211",Date=new TimeSpan(1,1,1),CvvCode="168"};

            //uow.PersonalInfoRepository.Create(pi);
            //uow.AddInfoRepository.Create(addin);
            //uow.PaymentRepository.Create(p);
            //uow.SaveChanges();

            ViewBag.Message = "Your application description page.";
            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}