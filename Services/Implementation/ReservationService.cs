using Repository.Implementation;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Services.Implementation
{
    public class ReservationService : IReservationService
    {
        private readonly IGenericRepository<Reservation> genericRepo;

        public ReservationService(IGenericRepository<Reservation> _genericRepo)
        {
            genericRepo = _genericRepo;
        }
        public Task Ajout(Reservation reservation)
        {
            return genericRepo.InsertAsync(reservation);
        }
    }
}
