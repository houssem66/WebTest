using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TourMe.Data.Entities;

namespace TourMe.Web.Models
{
    public class SendEmailViewModel
    {
        public string Id { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile> File { get; set; }
       
    }
}
