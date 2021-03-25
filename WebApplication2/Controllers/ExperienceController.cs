using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourMe.Web.Controllers
{
    public class ExperienceController :Controller
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        readonly private IExperienceService ExperienceService;

        public ExperienceController(IWebHostEnvironment hostingEnvironment, IExperienceService experienceService)
        {
            this.hostingEnvironment = hostingEnvironment;
            ExperienceService = experienceService;
        }
    }
}
