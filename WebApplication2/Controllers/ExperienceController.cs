using Domaine.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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


        public IActionResult GetAll(string[] searchTerm)

        {
            ViewBag.Best = ExperienceService.BestExperience();
            string[] search = new string[10];
            List<Experience> list = new List<Experience>();
            if (!(searchTerm.Count() == 0))
            {
                foreach (var ch in searchTerm)
                {
                    var list2 = ExperienceService.GetAllExperienceDetails(ch).ToList();
                    list = list.Concat(list2).ToList();
                }
                ;

                return View(list);
            }
            return View(ExperienceService.GetAllExperienceDetails("").ToList());


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
                    NbPlaces=model.NbPlaces,
                    tarif=model.tarif,
                    Activites = new Collection<Activite>()


                };
                //foreach (var item in Activites)
                //{

                //    await ActiviteService.Ajout(item);

                //}
            
                var result = await ExperienceService.InsertExperience(experience);
                Experience experience1 = await ExperienceService.GetById((int)result);
               
                TempData["experience"] = JsonConvert.SerializeObject(experience1);
                System.Diagnostics.Debug.WriteLine("idezeeeeeeeeeeee est" + experience1.ExperienceId);
                TempData["exp"] = JsonConvert.SerializeObject(model);
                ViewData["exp"] = JsonConvert.DeserializeObject<ExperienceViewModel>((string)TempData.Peek("exp"));
                return View("CreateActivite");
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
        public IActionResult CreateActivite()
        {
            ViewData["experience1"] = JsonConvert.DeserializeObject<Experience>((string)TempData.Peek("experience1"));
            TempData.Keep("experience1");
            Experience experience = (Experience)ViewData["experience1"];

            ViewData["exp"] = JsonConvert.DeserializeObject<ExperienceViewModel>((string)TempData.Peek("exp"));
            TempData.Keep("exp");

            ViewData["ListeActivite"] = ActiviteService.GetActivite(experience.ExperienceId);
            System.Diagnostics.Debug.WriteLine(ActiviteService.GetActivite(experience.ExperienceId).Count());

            return View();

        }

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

            var experience = await ExperienceService.GetById(exp.ExperienceId);
            await ratingService.Rater(experience, user, rating);

            ViewBag.avg = ratingService.Moyen(exp.ExperienceId);

            var x = await ratingService.Moyen(exp.ExperienceId);
            experience.AvgRating = x.ToString();
            await ExperienceService.PutExperienceAsync(experience.ExperienceId, experience);
            if (exp == experience)
            {
                return NotFound();
            }

            return RedirectToAction("GetAll");
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
                    Image = uniqueFileName,
                    dateDebut = model.dateDebut,
                    dateFin = model.dateFin
                };




                Activites.Add(activite);

                experience.Activites = (IList<Activite>)Activites;
                await ExperienceService.PutExperienceAsync(experience.ExperienceId, experience);
                await ActiviteService.Ajout(activite);
                TempData["experience1"] = JsonConvert.SerializeObject(experience);
                TempData["ListeActivite"] = JsonConvert.SerializeObject(ActiviteService.GetActivite(experience.ExperienceId));
                System.Diagnostics.Debug.WriteLine(ActiviteService.GetActivite(experience.ExperienceId).Count());
            }

            return RedirectToAction("CreateActivite", "Experience");

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AffichageExperience()
        {
            ViewData["experience1"] = JsonConvert.DeserializeObject<Experience>((string)TempData.Peek("experience1"));
            TempData.Keep("experience1");
            ViewData["ListeActivite"] = JsonConvert.DeserializeObject <ICollection<Activite>>((string)TempData.Peek("ListeActivite"));
           // ViewData["ListeActivite"] = (ICollection<Activite>)ActiviteService.GetActivite(ViewBag.experience1.ExperienceId);
            return View("ExperienceAffichage");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AffichageExperience(Experience model, IFormFile FileP,string details,int activiteId)
        { 
           
          
            model.Activites = (IList<Activite>)ActiviteService.GetActivite(model.ExperienceId);
          if(activiteId!=0)
            {
                string uniqueFileName = null;
                if (FileP != null)
                {
                    // The image must be uploaded to the images folder in wwwroot
                    // To get the path of the wwwroot folder we are using the inject
                    // HostingEnvironment service provided by ASP.NET Core
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Files");
                    // To make sure the file name is unique we are appending a new
                    // GUID value and and an underscore to the file name
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + FileP.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    // Use CopyTo() method provided by IFormFile interface to
                    // copy the file to wwwroot/images folder
                    FileP.CopyTo(new FileStream(filePath, FileMode.Create));

                }

            System.Diagnostics.Debug.WriteLine("l'id ta3 l'activité est "+ activiteId);
                Activite a = await ActiviteService.GetActiviteById(activiteId);
           
             
                   if (details !=null)
                   a.Details = details;
                   if(uniqueFileName !=null)
                   a.Image = uniqueFileName;
                   await  ActiviteService.Update(a);
            if(details is null && uniqueFileName is null)
            {
                await ActiviteService.Update(a);
            }
                
            }
            TempData["ListeActivite"] = JsonConvert.SerializeObject(ActiviteService.GetActivite(model.ExperienceId));
            TempData["experience1"] = JsonConvert.SerializeObject(model);

            await ExperienceService.PutExperienceAsync(model.ExperienceId, model);
            return RedirectToAction("AffichageExperience", "Experience");
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

