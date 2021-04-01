using Domaine.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Implementation;
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
    public class ExperienceController : Controller
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        readonly private IExperienceService ExperienceService;
        private readonly UserManager<Utilisateur> userManager;
        private readonly IUserService userService;
        private readonly IRatingService ratingService;
        
        public ExperienceController(IWebHostEnvironment hostingEnvironment, IExperienceService experienceService, UserManager<Utilisateur> userManager, IUserService _UserService,IRatingService ratingService)
        {
            this.hostingEnvironment = hostingEnvironment;
            ExperienceService = experienceService;
            this.userManager = userManager;
            userService = _UserService;
            this.ratingService = ratingService;
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
                    TypeExperience = model.TypeExperience,
                    Saison= model.Saison
                    

                };
                await ExperienceService.InsertExperienceAsync(experience);
            }
            return View("GetAll");

        }

        public IActionResult GetAll()

        {
            

           
           
            return View(ExperienceService.GetAllExperiences());


        }
        [HttpGet]
        [AllowAnonymous]
        
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var exp = await ExperienceService.GetById(id);
           
            
            if (exp == null)
            {
                return NotFound();
            }

            return View(exp);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string rating,Experience exp)
        {
            Debug.WriteLine("id"+exp.ExperienceId);
            string idu = userManager.GetUserId(User);
            Utilisateur user = await userService.GetUtilisateurByIdAsync(idu);


            var experience = await ExperienceService.GetById(exp.ExperienceId);
            ratingService.Rater(experience.ExperienceId, idu, 0);
            if (exp == experience)
            {
                return NotFound();
            }

            return RedirectToAction("GetAll");
        }
    }
}
