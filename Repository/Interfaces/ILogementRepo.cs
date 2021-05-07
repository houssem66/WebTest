using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Repository.Interfaces
{
   public interface ILogementRepo
    {
        public Task Update(Logement logement);
    }
}
