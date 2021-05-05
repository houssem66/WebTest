using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities
{
    public class Trasport
    {
        public string TypeVehicule { get; set; }
        public string Image { get; set; }
        public DateTime DateDisp { get; set; }
        public decimal Prix { get; set; }
        public int Periode { get; set; }
    }
}
