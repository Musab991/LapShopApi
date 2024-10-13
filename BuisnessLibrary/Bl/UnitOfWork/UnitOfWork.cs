using BuisnessLibrary.Bl.Repository;
using BuisnessLibrary.Bl.Repository.Interface;
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
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;

        public IGenericRepository<TbItem> Items { get; private set; }
        public IGenericRepository<TbCategory> Categories{ get; private set; }
        public IGenericRepository<TbItemType> ItemTypes{ get; private set; }
        public IGenericRepository<TbO> Os{ get; private set; }
        public IItemImageRepository ItemImages { get; private set; }

        private string _errorMessage = string.Empty;
        // Use the correct type for EF Core transactions
        private IDbContextTransaction _objTran;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Items = new GenericRepository<TbItem>(_context);
            Categories = new GenericRepository<TbCategory>(_context);
            ItemTypes = new GenericRepository<TbItemType>(_context);
            Os = new GenericRepository<TbO>(_context);
            ItemImages = new ItemImageRepository(_context);

        }

        public void CreateTransaction()
        {
            // BeginTransaction returns IDbContextTransaction in EF Core
            _objTran = _context.Database.BeginTransaction();
        }

        public async Task CommitAsync()
        {
            try
            {
                await SaveAsync();
                _objTran?.Commit();
            }
            catch
            {
                Rollback();
                throw; // Re-throw the exception after rollback
            }
        }

        public void Rollback()
        {
            _objTran?.Rollback();
            _objTran?.Dispose();
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var errorMessage = new StringBuilder();
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage.AppendLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
                throw new Exception(errorMessage.ToString(), dbEx);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            _objTran?.Dispose();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }
    }
}
