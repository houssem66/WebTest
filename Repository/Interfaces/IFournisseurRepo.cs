﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Repository.Interfaces
{
   public interface IFournisseurRepo
    {
        public Task<string> Delete(Fournisseur entity);
        public  Task<Fournisseur> GetFournisseurAsync(string id);
        public IQueryable<Fournisseur> GetFournisseursAsync();
    }
}
