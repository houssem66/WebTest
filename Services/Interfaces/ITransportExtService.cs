using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Services.Interfaces
{
    public interface ITransportExtService
    {
        public Task Ajout(ServiceTransport logement);
        public IList<ServiceTransport> GetAllTransports();
        public Task Update(ServiceTransport logement);
        public Task<ServiceTransport> GetLogementById(int id);
        public IList<ServiceTransport> GetTransportByUser(string id);
        public Task Delete(int id);
    }
}
