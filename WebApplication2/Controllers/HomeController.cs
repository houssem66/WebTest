using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TourMe.Web.Models;

namespace TourMe.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IExperienceService experienceService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IExperienceService experienceService, ILogger<HomeController> logger)
        {
            this.experienceService = experienceService;
            _logger = logger;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            var x = experienceService.BestExperiences();
            return View(x);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
