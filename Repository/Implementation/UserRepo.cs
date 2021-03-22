using Domaine.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data;

namespace Repository.Implementation
{
    public class UserRepo : IUserRepo
    {
        protected readonly TourMeContext _dbContext;
        public UserRepo(TourMeContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Utilisateur> GetAllUserAsync()
        {
            var User = _dbContext.User.Where(User => User.Id != "");
              
            return User;
        }

        public async Task<Utilisateur> GetUserDetailsAsync(string id)
        {
            var User = await _dbContext.User.SingleAsync(User => User.Id == id);

            _dbContext.Entry(User);
                    
                    

            return User;
        }

        
        public async Task PutUserAsync(string id, Utilisateur entity)
        {
            var user = await _dbContext.User.SingleAsync(user => user.Id == entity.Id);
            _dbContext.Entry(user).State = EntityState.Detached;
            _dbContext.Entry(entity).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }

        }

        public Task updateUser(string id, Utilisateur entity)
        {
            throw new NotImplementedException();
        }
        
    }
}
