using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Repository.Interfaces
{
    public interface ITransportExtRepo
    {
        public Task<ServiceTransport> GetTransportDetailsAsync(int id);
        public IQueryable<ServiceTransport> GetAll();
    }
}
