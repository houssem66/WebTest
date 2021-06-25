using Microsoft.AspNetCore.Http;
using TourMe.Data.Entities.Enum;

namespace TourMe.Web.Models
{
    public class TransportExtViewModel
    {
        public Region Region { get; set; }
        public string TypeTransport { get; set; }
        public string Load { get; set; }
        public string ReservationPrive { get; set; }
        public IFormFile Images { get; set; }
        public int NbrPlaces { get; set; }
    }
}
