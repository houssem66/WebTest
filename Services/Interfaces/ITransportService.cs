using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities;

namespace Services.Interfaces
{
    public interface ITransportService
    {
        public Task Ajout(Transport entity);
        public Transport GetTransport(int id);
        public Task Update(Transport entity);
        public Task<Transport> GetTransportById(int id);
    }
}
