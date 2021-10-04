using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TourMe.Data.Entities.Enum;

namespace TourMe.Web.Models
{
    public class TransportExtViewModel
    {
        public Region Region { get; set; }
        public string TypeTransport { get; set; }
        public string Load { get; set; }
        public string ReservationPrive { get; set; }
        public List<IFormFile> Images { get; set; }
        public string ImagesString { get; set; }
        public int NbrPlaces { get; set; }
        [Required(ErrorMessage = "Prix est obligatoire")]
        [Range(0.01, 999999999, ErrorMessage = "Price must be greater than 0.00")]
        [DisplayName("Tarif (dt)")]
        public decimal Prix { get; set; }

    }
}
