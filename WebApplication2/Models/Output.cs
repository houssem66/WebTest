using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourMe.Web.Models
{
    public class Output
    {
        public bool status { get; set; }
        public string message { get; set; }
        public int code { get; set; }
        public Data data { get; set; }
    }
}
