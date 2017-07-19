using InTechStore.DAL.Entities;
using InTechStore.DAL.Identity;
using System;

namespace InTechStore.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }

        IGenericRepository<PersonalInfo> PersonalInfoRepository { get; }
        IGenericRepository<Payment> PaymentRepository { get; }

        IGenericRepository<Product> ProductRepository { get; }
        IGenericRepository<ProductInfo> ProductInfoRepository { get; }
        IGenericRepository<Category> CategoryRepository { get; }

        IGenericRepository<Producer> ProducerRepository { get; }

        IGenericRepository<Purchase> PurchaseRepository { get; }
        IGenericRepository<PurchaseInfo> PurchaseInfoRepository { get; }
        IGenericRepository<Discount> DiscountRepository { get; }

        void SaveChanges();
    }
}
