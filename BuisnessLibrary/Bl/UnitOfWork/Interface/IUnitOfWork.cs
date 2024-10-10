using DomainLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLibrary.Bl.UnitOfWork.Interface
{
    public interface IUnitOfWork<out TContext> where TContext : AppDbContext, new()
    {
        //The following Property is going to hold the context object
        TContext Context { get; }
        //Start the database Transaction
        void CreateTransaction();
        //Commit the database Transaction
        void Commit();
        //Rollback the database Transaction
        void Rollback();
        //DbContext Class SaveChanges method
        void Save();
    }
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TbItem> Items { get; }

        //IBooksRepository Books { get; }

        //Start the database Transaction
        void CreateTransaction();
        //Commit the database Transaction
        void Commit();
        //Rollback the database Transaction
        void Rollback();
        //DbContext Class SaveChanges method
        void Save();
    }

}
