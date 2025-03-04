using Infra.Persistance.Interfaces;
using Microsoft.EntityFrameworkCore;
using RatingEngine.Persistance.Entities;
using RatingEngine.Persistance.Interfaces;
using System.Transactions;

namespace RatingEngine.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private readonly IRepository<Province> _provinceRepository;
        public UnitOfWork(DbContext dbContext, IRepository<Province> provinceRepository)
        {
            _dbContext = dbContext;
            _provinceRepository = provinceRepository;
        }

        public IRepository<Province> Provinces => _provinceRepository;
        public int SaveChanges() => _dbContext.SaveChanges();
        public TransactionScope StartTransactionScope() => new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

    }
}
