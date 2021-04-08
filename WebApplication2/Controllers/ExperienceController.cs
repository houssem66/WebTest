using Domaine.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.Implementation;
using Services.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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

        public ICollection<Activite> Activites = new Collection<Activite>();
        readonly private IActiviteService ActiviteService;
        private readonly UserManager<Utilisateur> userManager;

        private readonly IUserService userService;
        private readonly IRatingService ratingService;

        public ExperienceController(IWebHostEnvironment hostingEnvironment, IExperienceService experienceService, IActiviteService activiteService, UserManager<Utilisateur> userManager, IUserService _UserService, IRatingService ratingService)
        {
            this.hostingEnvironment = hostingEnvironment;
            ExperienceService = experienceService;
           
            ActiviteService = activiteService;
            this.userManager = userManager;
            userService = _UserService;
            this.ratingService = ratingService;
        }
        [AllowAnonymous]
        public async Task< IActionResult> GetAll(string searchTerm)

        {
            var list2 = ExperienceService.BestExperience();

            IQueryable<Experience> x = ExperienceService.GetAllExperienceDetails(searchTerm);

            foreach (var item in x) 
            {if (item.Rating == null) { Debug.WriteLine("rating was null"); }
                var s = ratingService.Moyen(item.ExperienceId).Result;
               
                Experience exp = await ExperienceService.GetExperienceByIdAsync(item.ExperienceId);
                exp.Rating = s.ToString();
               await ExperienceService.PutExperienceAsync(exp.ExperienceId,exp);
            }
            var list = ExperienceService.BestExperience();
            ViewBag.Best = ExperienceService.BestExperience();

            return View(ExperienceService.GetAllExperienceDetails(searchTerm));


        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult CreateExperience(int ActiviteId)
        {
            TempData.Keep("Message");
            // ViewBag.Message = TempData["Message"];
            return View();
        }




            [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateExperience(ExperienceViewModel model)
        {
            ViewData["Message"] = JsonConvert.DeserializeObject<List<Activite>>((string)TempData["Message"]);
           
            Activites = (ICollection<Activite>)ViewData["Message"];
            if (ModelState.IsValid)
            {
                //    string uniqueFileName = null;
                //    if (model.FileP != null && model.FileP.Count > 0)
                //    {
                //        // Loop thru each selected file
                //        foreach (IFormFile photo in model.FileP)
                //        {
                //            // The file must be uploaded to the images folder in wwwroot
                //            // To get the path of the wwwroot folder we are using the injected
                //            // IHostingEnvironment service provided by ASP.NET Core
                //            string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                //            // To make sure the file name is unique we are appending a new
                //            // GUID value and and an underscore to the file name
                //            uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                //            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                //            // Use CopyTo() method provided by IFormFile interface to
                //            // copy the file to wwwroot/images folder
                //            photo.CopyTo(new FileStream(filePath, FileMode.Create));
                //        }
                //    }

                //  var activite =  



                Experience experience = new Experience
                {
                    Titre = model.Titre,
                    Lieu = model.Lieu,
                    TypeExperience = model.TypeExperience,
                    dateDebut = model.dateDebut,
                    dateFin = model.dateFin,
                    Saison = model.Saison,
                    Activites = Activites


                };
                foreach (var item in Activites)
                {

                    // experience.Activites.Add(item);
                    await ActiviteService.Ajout(item);

                }
               
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
        [HttpGet]
        [AllowAnonymous]

        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var exp = await ExperienceService.GetById(id);

            var x = ratingService.GetListByeEXp(exp);
            if (exp == null)
            {

                return NotFound();
            }

            var s = ratingService.Moyen(exp.ExperienceId).Result;
           
            ViewBag.avg = s;



            return View(exp);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string rating, Experience exp)
        {

            string idu = userManager.GetUserId(User);
            Utilisateur user = await userService.GetUtilisateurByIdAsync(idu);

            ViewBag.avg = ratingService.Moyen(exp.ExperienceId);
            var experience = await ExperienceService.GetById(exp.ExperienceId);
            await ratingService.Rater(experience, user, rating);


            if (exp == experience)
            {
                return NotFound();
            }

            return RedirectToAction("GetAll");
        }


        [HttpPost]
        [AllowAnonymous]

        public ActionResult CreateActivite(Activite activite)
        {
          //  ExperienceViewModel model = new ExperienceViewModel();


            if (ModelState.IsValid)
            {
                //string uniqueFileName = null;
                //if (model.FileP != null && model.FileP.Count > 0)
                //{
                //    // Loop thru each selected file
                //    foreach (IFormFile photo in model.FileP)
                //    {
                //        // The file must be uploaded to the images folder in wwwroot
                //        // To get the path of the wwwroot folder we are using the injected
                //        // IHostingEnvironment service provided by ASP.NET Core
                //        string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                //        // To make sure the file name is unique we are appending a new
                //        // GUID value and and an underscore to the file name
                //        uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                //        // Use CopyTo() method provided by IFormFile interface to
                //        // copy the file to wwwroot/images folder
                //        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                //    }
                //}
                //activite.Image = uniqueFileName;


                Activites.Add(activite);

                TempData["Message"] = JsonConvert.SerializeObject(Activites);
              
                //  TempData["Message"] = Activites ;

                //    await  ActiviteService.Ajout(activite);

            }
            ViewData["Message"] = JsonConvert.DeserializeObject<List<Activite>>((string)TempData["Message"]);
            Activites = (ICollection<Activite>)ViewData["Message"];
            TempData.Keep("Message");




          


            return RedirectToAction("CreateExperience");



        }
   

        [HttpPost]
        [AllowAnonymous]
        public ActionResult test(string type)
        {



        

                System.Diagnostics.Debug.WriteLine("Le type est" + type);
                return PartialView("_Activité");
           


        }


    }
}
