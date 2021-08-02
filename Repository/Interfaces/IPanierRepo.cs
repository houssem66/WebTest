using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Repository.Interfaces
{
 public   interface IPanierRepo
    {
        public Task Update(Panier panier);
        public decimal PrixTotal(Panier panier, int nbrNuit, int nbrRepats, int nbrJours);
    }
}
