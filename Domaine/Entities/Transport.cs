using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities.Enum;

namespace TourMe.Data.Entities
{
    public class Transport
    { [Key]
        public int TrasportId { get; set; }
        public TypeTransport TypeTransport { get; set; }
        public string Image { get; set; }
        public DateTime DateDisp { get; set; }
        public decimal Prix { get; set; }
        public int Periode { get; set; }
        public int ExperienceId { get; set; }
    }
}
