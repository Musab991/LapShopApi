using BuisnessLibrary.Bl.Repository.Interface;
using DomainLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLibrary.Bl.UnitOfWork.Interface
{

    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TbItem> Items { get; }
        IItemImageRepository ItemImages { get; }
        IGenericRepository<TbCategory>Categories{get;}
        IGenericRepository<TbItemType> ItemTypes { get;}
        IGenericRepository<TbGpu> Gpus{ get;}
        IGenericRepository<TbO> Os{ get;}
        IGenericRepository<TbProcessor> Processors { get;}
        IGenericRepository<TbHardDisk> HardDisks { get;}
        IGenericRepository<TbScreenResolution> ScreenResolutions { get;}
        IGenericRepository<TbRam> Rams { get;}
        IGenericRepository<TbSalesInvoice> SalesInvoices { get;}
        ISalesInvoiceItemRepository SalesInvoiceItems { get;}
        //Start the database Transaction
        void CreateTransaction();
        //Commit the database Transaction
        void Commit();
        Task CommitAsync();
        //Rollback the database Transaction
        void Rollback();
        //DbContext Class SaveChanges method
        void Save();
        Task SaveAsync();
    }

}
