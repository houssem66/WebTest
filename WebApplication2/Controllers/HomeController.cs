using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TourMe.Data.Entities;
using TourMe.Web.Models;

namespace TourMe.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IExperienceService ExperienceService;
        private readonly INourritureExtService NourritureExtService;
        private readonly ILogementextService LogementExtService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogementextService logementextService, INourritureExtService nourritureExtService, IExperienceService experienceService, ILogger<HomeController> logger)
        {
           ExperienceService = experienceService;
            LogementExtService = logementextService;
            NourritureExtService = nourritureExtService;
            _logger = logger;
        }
        [AllowAnonymous]
        public  IActionResult Index()
        {
            List<ServiceNouritture> nourritures = new List<ServiceNouritture>();
            List<ServiceLogment> logements = new List<ServiceLogment>();
            List<Experience> experiences = new List<Experience>();
            if (ExperienceService.GetAllExperienceDetails(null)!=null)
                experiences = ExperienceService.GetAllExperienceDetails(null).OrderBy(x => x.AvgRating).Take(5).ToList();
            else experiences = null;
            logements = LogementExtService.GetAllLogements().OrderBy(x => x.PrixParNuit).Take(6).ToList();
            nourritures= NourritureExtService.GetAllLogements().OrderBy(x => x.Prix).Take(6).ToList();
            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.ListeExperience = experiences;
            homeViewModel.ListeLogement = logements;
            homeViewModel.ListeNourriture = nourritures;

            return View(homeViewModel);
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
