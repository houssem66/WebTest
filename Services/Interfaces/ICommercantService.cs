using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;
using Domaine.Entities;



namespace Services.Interfaces
{
    public interface ICommercantService
    {
        public Task Ajout(Hôte commerçant);
        public IList<Hôte> GetAllCommerçants();
        public Task Update(Hôte Commerçant);
        public Task<Hôte> GetCommerçantById(string id);
        public Task Delete(Hôte Commerçant);
        public IEnumerable<HôteDocuments> GetListfile(string id);
    }
}
