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
        public Task Ajout(Commerçant commerçant);
        public IList<Commerçant> GetAllCommerçants();
        public Task Update(Commerçant Commerçant);
        public Task<Commerçant> GetCommerçantById(string id);
        public Task Delete(Commerçant Commerçant);
    }
}
