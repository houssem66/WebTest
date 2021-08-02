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
    public class PanierRepo : IPanierRepo
    {
        private readonly TourMeContext dbContext;
        private readonly IGenericRepository<Panier> genericRepoPanier;

        public PanierRepo(TourMeContext dbContext, IGenericRepository<Panier> _GenericRepoPanier)
        {
            this.dbContext = dbContext;
            genericRepoPanier = _GenericRepoPanier;
        }

        public  decimal PrixTotal(Panier panier,int nbrNuit,int nbrRepats,int nbrJours)
        {
            decimal prix = 0;
            var products =   genericRepoPanier.GetByIdAsync(panier.Id).Result;
            if(products.Logments.Count>0)
            {
                foreach(var item in products.Logments)
                {
                    prix += item.PrixParNuit*nbrNuit;

                }

            }

            if (products.Nourittures.Count > 0)
            {
                foreach (var item in products.Nourittures)
                {
                    prix += item.Prix * nbrRepats;

                }

            }

            if (products.Transports.Count > 0)
            {
                foreach (var item in products.Transports)
                {
                    prix += item.Prix * nbrJours;

                }

            }




            return prix;


        }

        public async Task Update(Panier panier)
        {
            var pan = await dbContext.Paniers.SingleAsync(e => e.Id == panier.Id);
            dbContext.Entry(pan).State = EntityState.Detached;
            dbContext.Entry(panier).State = EntityState.Modified;
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new NotImplementedException();

            }
        }
        }
    }

