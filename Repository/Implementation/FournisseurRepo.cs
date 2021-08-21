using Microsoft.EntityFrameworkCore;
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

        public async Task<Fournisseur> GetFournisseurAsync(string id)
        {
            var Fournisseur = await dbContext.Fournisseurs.Include(x => x.EmployeDocuments).SingleAsync(f => f.Id == id);
            dbContext.Entry(Fournisseur).Collection(U => U.EmployeDocuments).Query().Load();
            dbContext.Entry(Fournisseur).State = EntityState.Detached;
            return Fournisseur;
        }

        public IQueryable<Fournisseur> GetFournisseursAsync()
        {
            var Fournisseur = dbContext.Fournisseurs.Where(ex => ex.Id != null).Include(x => x.EmployeDocuments);
            return Fournisseur;
        }
    }
}
