using Infra.Persistance.Interfaces;
using RatingEngine.Persistance.Entities;
using System.Transactions;

namespace RatingEngine.Persistance.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Province> Provinces { get; }
        int SaveChanges();
        public TransactionScope StartTransactionScope();
    }
}