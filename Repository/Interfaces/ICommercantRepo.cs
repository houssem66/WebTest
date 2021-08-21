using Domaine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
   public interface ICommercantRepo
    {
        public IQueryable<Commerçant> GetAllCommercant();
    }
}
