using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Services.Interfaces
{
   public interface INourritureService
    {
        public Task Ajout(Nourriture entity);
        public Nourriture GetNourriture(int id);
        public Task Update(Nourriture entity);
        public Task<Nourriture> GetNourritureById(int id);
    }
}
