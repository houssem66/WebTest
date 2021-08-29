using Repository.Implementation;
using Repository.Interfaces;
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
        private readonly IReservationRepo iReservationRepo;

        public ReservationService(IGenericRepository<Reservation> _genericRepo, IReservationRepo iReservationRepo)
        {
            genericRepo = _genericRepo;
            this.iReservationRepo = iReservationRepo;
        }
        public Task Ajout(Reservation reservation)
        {
            return genericRepo.InsertAsync(reservation);
        }

        public Task InsertAsync(Panier reservation, int id)
        {
            return iReservationRepo.InsertAsync(reservation, id);
        }
    }
}
