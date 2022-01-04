using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Repository.Interfaces
{
    public interface IHebergementRepo
    {
        public Task InsertHebergement(Hebergement entity);
        public Task<Hebergement> GetHebergementDetailsAsync(int Id);

    }
}
