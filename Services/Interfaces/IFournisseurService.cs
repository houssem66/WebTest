using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Services.Interfaces
{
  public  interface IFournisseurService
    {
        public Task Ajout(Fournisseur fournisseur);
        public IList<Fournisseur> GetAllFournisseurs();
        public Task Update(Fournisseur fournisseur);
        public Task<Fournisseur> GetFournisseurById(string id);
        public Task<string> Delete(Fournisseur fournisseur);
    }
}
