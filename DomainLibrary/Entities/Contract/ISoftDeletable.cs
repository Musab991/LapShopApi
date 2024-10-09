using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLibrary.Entities.Contract
{
    public interface ISoftDeletable
    {

        public bool IsDeleted { get; set; }

        public DateTime? DateDeleted { get; set; }

        public void Delete()
        {
            IsDeleted = true;
            DateDeleted = DateTime.Now;
        }

        public void UndoDelete()
        {
            IsDeleted = false;
            DateDeleted = null;
        }

    }
}
