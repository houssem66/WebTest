using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourMe.Web.Models
{
    public class ActiviteViewModel
    {
        public List<IFormFile> FileP { get; set; }
        public string Details { get; set; }
    }
}
