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

        public async  Task<Panier> GetPanier(int id)
        {
            var panier =  await dbContext.Paniers.Include(x => x.Experiences).SingleAsync(x => x.Id.Equals(id));
            dbContext.Entry(panier).Collection(pa => pa.Logments).Query().Load();
            dbContext.Entry(panier).Collection(pa => pa.Nourittures).Query().Load();
            dbContext.Entry(panier).Collection(pa => pa.Transports).Query().Load();
            dbContext.Entry(panier).State = EntityState.Detached;
            return panier;
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

        public async Task Update(Panier panier,ServiceLogment serviceLogment)
        {
           
            var pan = await dbContext.Paniers.Include(e=>e.Experiences).SingleAsync(x => x.Id.Equals(panier.Id));
            dbContext.Entry(pan).Collection(pa => pa.Logments).Query().Load();


            //panier.Experiences = pan.Experiences;
           
            //panier.Experiences.Clear();
            //foreach (var experience in pan.Experiences)
            //{     

            //    panier.Experiences.Add(experience);

            //}


            if (pan.Logments.FirstOrDefault()==serviceLogment)
            dbContext.Entry(panier).State = EntityState.Unchanged;
            else
            {
                panier.Logments = new List<ServiceLogment>();
                panier.Logments.Add(serviceLogment);
                dbContext.Entry(panier).State = EntityState.Modified;

            }
           
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            { 
                throw new NotImplementedException();

            }
        }



        public async Task InsertAsync(Panier panier, int id)

        {
            try
            {
                var experiences = await dbContext.Experience.SingleAsync(p => p.ExperienceId.Equals(id));
                panier.Experiences.Add(experiences);
                await dbContext.Paniers.AddAsync(panier);



                await dbContext.SaveChangesAsync();
            }
            catch
            {




            }


        }

        public IEnumerable<Panier>GetPanierByuserIdAsync(string id)
        {
            var panier = dbContext.Paniers.Include(x => x.Experiences).Include(x=>x.Logments).Include(x=>x.Nourittures).Include(x=>x.Transports).Where(x => x.User.Id.Equals(id));
         
            
        
            return panier;
          
        }

        public Task<Panier> GetPan(int id)
        {
            return genericRepoPanier.GetByIdAsync(id);
        }

        public async  Task UpdateNourriture(Panier panier, ServiceNouritture serviceNouritture)
        {

            var pan = await dbContext.Paniers.Include(e => e.Experiences).SingleAsync(x => x.Id.Equals(panier.Id));
            dbContext.Entry(pan).Collection(pa => pa.Nourittures).Query().Load();
            //panier.Experiences = pan.Experiences;
            if (pan.Nourittures.FirstOrDefault()==serviceNouritture)

                dbContext.Entry(panier).State = EntityState.Unchanged;

            else
            {
                panier.Nourittures = new List<ServiceNouritture>();
                panier.Nourittures.Add(serviceNouritture);
                dbContext.Entry(panier).State = EntityState.Modified;

            }
     
  
         
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new NotImplementedException();

            }
        }


        public async Task UpdateTransport(Panier panier, ServiceTransport serviceTransport)
        {

            var pan = await dbContext.Paniers.Include(e => e.Experiences).SingleAsync(x => x.Id.Equals(panier.Id));
            dbContext.Entry(pan).Collection(pa => pa.Transports).Query().Load();
            //panier.Experiences = pan.Experiences;
          

            if ( pan.Transports.FirstOrDefault()== serviceTransport)

                dbContext.Entry(panier).State = EntityState.Unchanged;

            else
            {
                panier.Transports = new List<ServiceTransport>();
                panier.Transports.Add(serviceTransport);

                dbContext.Entry(panier).State = EntityState.Modified;
            }



         
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new NotImplementedException();

            }
        }

        public async Task<Panier> GetPanUser(string id)
        {
            var panier = await dbContext.Paniers.OrderByDescending(e=>e.Prix).Include(x => x.Logments).LastOrDefaultAsync(x => x.User.Id.Equals(id));

            //dbContext.Entry(panier).Collection(pa => pa.Nourittures).Query().Load();
            //dbContext.Entry(panier).Collection(pa => pa.Transports).Query().Load();
            dbContext.Entry(panier).State = EntityState.Detached;
            return panier;
        }

        public async Task UpdatePanier(Panier entity)
        {
            var Panier = await dbContext.Paniers.SingleAsync(e => e.Id == entity.Id);
            dbContext.Entry(Panier).State = EntityState.Detached;
            dbContext.Entry(entity).State = EntityState.Modified;
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

