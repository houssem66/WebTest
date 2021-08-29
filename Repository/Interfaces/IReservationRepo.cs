using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Repository.Interfaces
{
  public  interface IReservationRepo
    {
        // Task Add(Panier panier, int id);
        Task InsertAsync(Panier panier, int id);

    }
}
