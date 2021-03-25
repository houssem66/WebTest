using Domaine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUserRepo
    {
        public Task<Utilisateur> GetUserDetailsAsync(string id);
        public  Task PutUserAsync(string id, Utilisateur entity);
        public IEnumerable<Utilisateur> GetAllUserAsync();
        public Task updateUser(string id, Utilisateur entity);
      
    }
}
