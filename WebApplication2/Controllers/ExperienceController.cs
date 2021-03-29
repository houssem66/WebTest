using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourMe.Data.Entities;
using TourMe.Web.Models;

namespace TourMe.Web.Controllers
{
    public class ExperienceController : Controller
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        readonly private IExperienceService ExperienceService;

        public ExperienceController(IWebHostEnvironment hostingEnvironment, IExperienceService experienceService)
        {
            this.hostingEnvironment = hostingEnvironment;
            ExperienceService = experienceService;
        }

        [HttpGet]
        [AllowAnonymous]

        public IActionResult CreateExperience()
        {
           
            return View();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateExperience(ExperienceViewModel model)
        {
            if (ModelState.IsValid)
            {
                Experience experience = new Experience
                {
                    Titre = model.Titre,
                    Lieu = model.Lieu,
                    TypeExperience = model.TypeExperience

                };
                await ExperienceService.InsertExperienceAsync(experience);
            }
            return View(model);

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAllExperience(string searchTerm)
        {
          
            return View(ExperienceService.Search(searchTerm));

        }



    }
    }
