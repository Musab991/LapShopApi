using DomainLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLibrary.Bl.Repository.Interface
{
    public interface IItemImageRepository : IGenericRepository<TbItemImage>
    {
        public Task UploadImages(int itemId, List<string> uploadedImageNames);
    }
   
   
}
