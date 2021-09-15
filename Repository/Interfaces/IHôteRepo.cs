using Domaine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Repository.Interfaces
{
    public interface IHôteRepo
    {
        public IQueryable<Hôte> GetAllCommercant();
        public IEnumerable<HôteDocuments> GetListfile(string id);
        public Task<Hôte> GetCommercantDetailsAsync(string id);

    }
}
