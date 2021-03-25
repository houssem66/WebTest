﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Services.Interfaces
{
 public  interface IExperienceService
    {
        public IEnumerable<Experience> GetAllExperiences();


        public Task InsertExperienceAsync(Experience entity);
        public Task DeleteExperienceAsync(string id);
        public IEnumerable<Experience> GetAllExperienceDetails();


        public Task PutExperienceAsync(string id, Experience entity);
        

        public Task<Experience> GetExperienceByIdAsync(string id);
        public Task<Experience> GetById(string id);


    }
}