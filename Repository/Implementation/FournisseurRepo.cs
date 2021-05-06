using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data;
using TourMe.Data.Entities;

namespace Repository.Implementation
{
 public   class FournisseurRepo : IFournisseurRepo
    {
        private readonly TourMeContext dbContext;
        private readonly IGenericRepository<Fournisseur> genericRepoFournisseur;

        public FournisseurRepo(TourMeContext dbContext, IGenericRepository<Fournisseur> _GenericRepoFournisseur)
        {
            this.dbContext = dbContext;
            genericRepoFournisseur = _GenericRepoFournisseur;
        }

        public async Task<string> Delete(Fournisseur entity)
        {
            try
            {

                await genericRepoFournisseur.DeleteAsync(entity.Id);
            }
            catch (Exception)
            {
                return "fail";

            }
            return "success";
        }
    }
}
