using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ExperienceController(IWebHostEnvironment hostingEnvironment, IExperienceService experienceService, IActiviteService activiteService)
        {
            this.hostingEnvironment = hostingEnvironment;
            ExperienceService = experienceService;
           
            ActiviteService = activiteService;
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
            //ViewData["Message"] = JsonConvert.DeserializeObject<List<Activite>>((string)TempData.Peek("Message"));
            //TempData.Keep("Message");
            //Activites = (ICollection<Activite>)ViewData["Message"];
            if (ModelState.IsValid)
            {
              
                Experience experience = new Experience
                {
                    Titre = model.Titre,
                    Lieu = model.Lieu,
                    TypeExperience = model.TypeExperience,
                    dateDebut = model.dateDebut,
                    dateFin = model.dateFin,
                    Saison = model.Saison,
                    Activites = new Collection<Activite>()


                };
                //foreach (var item in Activites)
                //{

                //    await ActiviteService.Ajout(item);

                //}
               
               int a = await ExperienceService.InsertExperience(experience);
                Experience experience1 = await ExperienceService.GetById(a); 
              
                TempData["experience"] = JsonConvert.SerializeObject(experience1);
                System.Diagnostics.Debug.WriteLine( "idezeeeeeeeeeeee est" +experience1.ExperienceId);
                TempData["exp"] = JsonConvert.SerializeObject(model);
                ViewData["exp"] = JsonConvert.DeserializeObject<ExperienceViewModel>((string)TempData.Peek("exp"));
            
            }
            
            
            return View("CreateActivite");

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAllExperience(string searchTerm)
        {

            return View(ExperienceService.Search(searchTerm));

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult CreateActivite()
        {
            ViewData["experience1"] = JsonConvert.DeserializeObject<Experience>((string)TempData.Peek("experience1"));
            TempData.Keep("experience1");
            Experience experience = (Experience)ViewData["experience1"];

            ViewData["exp"] = JsonConvert.DeserializeObject<ExperienceViewModel>((string)TempData.Peek("exp"));
            TempData.Keep("exp");

             ViewData["ListeActivite"]= ActiviteService.GetActivite(experience.ExperienceId);
            System.Diagnostics.Debug.WriteLine(ActiviteService.GetActivite(experience.ExperienceId).Count());

             return View();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateActivite(ActiviteViewModel model)
        {
            ViewData["exp"] = JsonConvert.DeserializeObject<ExperienceViewModel>((string)TempData.Peek("exp"));
            TempData.Keep("exp");

            ViewData["experience"] = JsonConvert.DeserializeObject<Experience>((string)TempData.Peek("experience"));
            TempData.Keep("experience");
            Experience experience = (Experience)ViewData["experience"];
            System.Diagnostics.Debug.WriteLine(experience.ExperienceId);
          
            if (ModelState.IsValid)
            {   
                //if (ViewData["Message"] != null) 
                //{
                //    ViewData["Message"] = JsonConvert.DeserializeObject<List<Activite>>((string)TempData["Message"]);
                //    Activites = (ICollection<Activite>)ViewData["Message"];
                //    TempData.Keep("Message");

                //}
               
                string uniqueFileName = null;
                if (model.FileP != null && model.FileP.Count > 0)
                {
                    // Loop thru each selected file
                    foreach (IFormFile photo in model.FileP)
                    {
                        // The file must be uploaded to the images folder in wwwroot
                        // To get the path of the wwwroot folder we are using the injected
                        // IHostingEnvironment service provided by ASP.NET Core
                        string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                        // To make sure the file name is unique we are appending a new
                        // GUID value and and an underscore to the file name
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        // Use CopyTo() method provided by IFormFile interface to
                        // copy the file to wwwroot/images folder
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                }

                Activite activite = new Activite

                {
                    Details = model.Details,
                    Image = uniqueFileName
                };

              


                Activites.Add(activite);

                experience.Activites = Activites;
           await      ExperienceService.PutExperienceAsync(experience.ExperienceId,experience);
                await ActiviteService.Ajout(activite);
                TempData["experience1"] = JsonConvert.SerializeObject(experience);
                ViewData["ListeActivite"] = ActiviteService.GetActivite(experience.ExperienceId);
                System.Diagnostics.Debug.WriteLine(ActiviteService.GetActivite(experience.ExperienceId).Count());
            }

            return RedirectToAction("CreateActivite","Experience");

        }
   

      [HttpGet]
        [AllowAnonymous]
        public ActionResult test(string type)
        {



        

                System.Diagnostics.Debug.WriteLine("Le type est" + type);
                return PartialView("_Activité");
           


        }
    

    }
}
