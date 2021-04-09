﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Services.Interfaces
{
    public interface IActiviteService
    {
        public  Task Ajout(Activite activite);
        public IEnumerable<Activite> GetActivite(int id); 



    }
}
