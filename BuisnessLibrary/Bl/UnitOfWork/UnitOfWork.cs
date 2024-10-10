using BuisnessLibrary.Bl.Repository;
using BuisnessLibrary.Bl.UnitOfWork.Interface;
using DomainLibrary.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace BuisnessLibrary.Bl.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IGenericRepository<TbItem> Items { get; private set; }

        private string _errorMessage = string.Empty;
        //The following Object is going to hold the Transaction Object
        private DbContextTransaction _objTran;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Items = new GenericRepository<TbItem>(_context);
            //Books = new BooksRepository(_context);
        }
       
        public void CreateTransaction()
        {
            //It will Begin the transaction on the underlying store connection
            _objTran = (DbContextTransaction)_context.Database.BeginTransaction();
        }
       
        public void Commit()
        {
            //Commits the underlying store transaction
            _objTran.Commit();
        }
        
        public void Rollback()
        {
            //Rolls back the underlying store transaction
            _objTran.Rollback();
            //The Dispose Method will clean up this transaction object and ensures Entity Framework
            //is no longer using that transaction.
            _objTran.Dispose();
        }
        //The Save() Method Implement DbContext Class SaveChanges method 
        //So whenever we do a transaction we need to call this Save() method 
        //so that it will make the changes in the database permanently
        public void Save()
        {
            try
            {
                //Calling DbContext Class SaveChanges method 
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        _errorMessage = _errorMessage + $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage} {Environment.NewLine}";
                    }
                }
                throw new Exception(_errorMessage, dbEx);
            }
        }

        public void Dispose()
        {
             _context.Dispose();
        }
        //Disposing of the Context Object
    }
}
