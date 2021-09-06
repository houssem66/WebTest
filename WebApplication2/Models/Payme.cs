using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourMe.Web.Models
{
    public class Payme
    {
        public int vendor { get; set; }
        public float amount { get; set; }
        public string note { get; set; }
    }
}
