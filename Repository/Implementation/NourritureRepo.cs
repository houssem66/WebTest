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
    public class NourritureRepo : INourritureRepo
    {
        protected readonly TourMeContext _dbContext;
        readonly private IGenericRepository<Nourriture> genericRepoUser;

        public NourritureRepo(TourMeContext dbContext, IGenericRepository<Nourriture> _GenericRepoNourriture)
        {
            _dbContext = dbContext;
            genericRepoUser = _GenericRepoNourriture;

        }

        public Task Update(Nourriture nourriture)
        {
            throw new NotImplementedException();
        }
    }
}
