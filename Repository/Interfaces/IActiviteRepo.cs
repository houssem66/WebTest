using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Repository.Interfaces
{
  public  interface IActiviteRepo
    {
              public Task Update(Activite activite);
        public Task<Activite> GetActiviteByImage(string src);
    }
}
