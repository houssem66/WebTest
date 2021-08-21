using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities
{
    public class TransportDocument
    {
        public int Id { get; set; }
        public string Filepath { get; set; }
        public virtual ServiceTransport ServiceTransport { get; set; }
    }
}
