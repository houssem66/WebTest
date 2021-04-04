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
  public   class ActiviteService : IActiviteService
    {
        private readonly IGenericRepository<Activite> GenericRepo;

        public ActiviteService(IGenericRepository<Activite> genericRepo)
        {
            GenericRepo = genericRepo;
        }
        public  Task Ajout(Activite activite)
        {
            return GenericRepo.InsertAsync(activite);
        }

    }
}
