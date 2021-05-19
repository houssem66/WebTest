using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Services.Interfaces
{
  public  interface IReservationService
    {
        public Task Ajout(Reservation reservation);
    }
}
