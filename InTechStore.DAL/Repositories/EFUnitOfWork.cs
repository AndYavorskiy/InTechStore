using InTechStore.DAL.Interfaces;
using System;
using InTechStore.DAL.Entities;
using InTechStore.DAL.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace InTechStore.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        private IGenericRepository<PersonalInfo> _personalInfoRepository;
        private IGenericRepository<Payment> _paymentRepository;
        private IGenericRepository<Product> _productRepository;
        private IGenericRepository<ProductInfo> _productInfoRepository;
        private IGenericRepository<Category> _categoryRepository;
        private IGenericRepository<Producer> _producerRepository;
        private IGenericRepository<Purchase> _purchaseRepository;
        private IGenericRepository<PurchaseInfo> _purchaseInfoRepository;

        public EFUnitOfWork()
        {
            _context = new ApplicationDbContext();
            _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context));
            _roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(_context));
        }

        public ApplicationUserManager UserManager { get => _userManager; }
        public ApplicationRoleManager RoleManager { get => _roleManager; }

        public IGenericRepository<PersonalInfo> PersonalInfoRepository
        {
            get
            {
                return _personalInfoRepository ?? (_personalInfoRepository = new EFGenericRepository<PersonalInfo>(_context));
            }
        }
        public IGenericRepository<Payment> PaymentRepository
        {
            get
            {
                return _paymentRepository ?? (_paymentRepository = new EFGenericRepository<Payment>(_context));
            }
        }
        public IGenericRepository<Product> ProductRepository
        {
            get
            {
                return _productRepository ?? (_productRepository = new EFGenericRepository<Product>(_context));
            }
        }
        public IGenericRepository<ProductInfo> ProductInfoRepository
        {
            get
            {
                return _productInfoRepository ?? (_productInfoRepository = new EFGenericRepository<ProductInfo>(_context));
            }
        }
        public IGenericRepository<Category> CategoryRepository
        {
            get
            {
                return _categoryRepository ?? (_categoryRepository = new EFGenericRepository<Category>(_context));
            }
        }
        public IGenericRepository<Producer> ProducerRepository
        {
            get
            {
                return _producerRepository ?? (_producerRepository = new EFGenericRepository<Producer>(_context));
            }
        }
        public IGenericRepository<Purchase> PurchaseRepository
        {
            get
            {
                return _purchaseRepository ?? (_purchaseRepository = new EFGenericRepository<Purchase>(_context));
            }
        }
        public IGenericRepository<PurchaseInfo> PurchaseInfoRepository
        {
            get
            {
                return _purchaseInfoRepository ?? (_purchaseInfoRepository = new EFGenericRepository<PurchaseInfo>(_context));
            }
        }

        public void SaveChanges()
        {
           _context.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
