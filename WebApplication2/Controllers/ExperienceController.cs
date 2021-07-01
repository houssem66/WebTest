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

        private readonly SignInManager<Utilisateur> signInManager;

        public ICollection<Activite> Activites = new Collection<Activite>();

        readonly private IActiviteService ActiviteService;
        readonly private INourritureService NourritureService;
        readonly private ILogementService LogementService;
        readonly private ITransportService TransportService;

        private readonly UserManager<Utilisateur> userManager;

        private readonly IUserService userService;
        private readonly IRatingService ratingService;
        readonly private IUserService UserService;

        public ExperienceController(ITransportService transportService, ILogementService _LogementService, IUserService _UserService, INourritureService _NourritureService, IWebHostEnvironment hostingEnvironment, IExperienceService experienceService, IActiviteService activiteService, UserManager<Utilisateur> userManager, IRatingService ratingService, SignInManager<Utilisateur> signInManager)
        {
            TransportService = transportService;
            LogementService = _LogementService;
            this.hostingEnvironment = hostingEnvironment;
            ExperienceService = experienceService;
            UserService = _UserService;
            NourritureService = _NourritureService;
            ActiviteService = activiteService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            userService = _UserService;
            this.ratingService = ratingService;

        }
        [HttpGet]
        [AllowAnonymous]

        public IActionResult MesExperiences()

        {
            ViewBag.user = userManager.GetUserAsync(User).Result;
            // ViewBag.Best = ExperienceService.BestExperience();
            var list = ExperienceService.GetExperienceByUser(userManager.GetUserId(User));
            return View(list);

        }

        [HttpGet]
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
        // [Authorize(Policy = "CreateExperiencePolicy")]
        public IActionResult CreateExperience()
        {

            TempData.Keep("Message");
            // ViewBag.Message = TempData["Message"];
            return View();
        }

        [HttpPost]

        //[Authorize(Policy = "CreateExperiencePolicy")]
        public async Task<IActionResult> CreateExperience(ExperienceViewModel model)
        {
            //ViewData["Message"] = JsonConvert.DeserializeObject<List<Activite>>((string)TempData.Peek("Message"));
            //TempData.Keep("Message");
            //Activites = (ICollection<Activite>)ViewData["Message"];
            if (ModelState.IsValid)
            {
                string id = userManager.GetUserId(User);
                Experience experience = new Experience
                {
                    Titre = model.Titre,
                    Lieu = model.Lieu,
                    TypeExperience = model.TypeExperience,
                    dateDebut = model.dateDebut,
                    dateFin = model.dateFin,
                    Saison = model.Saison,
                    NbPlaces = model.NbPlaces,
                    tarif = model.tarif,
                    Activites = new Collection<Activite>(),
                    CommerçantId = id


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
        public async Task<IActionResult> Details(string rating, Experience exp, string comment)
        {

            string idu = userManager.GetUserId(User);
            Utilisateur user = await userService.GetUtilisateurByIdAsync(idu);

            var experience = await ExperienceService.GetById(exp.ExperienceId);
            if (rating != null)
            {
                await ratingService.Rater(experience, user, rating);

                ViewBag.avg = ratingService.Moyen(exp.ExperienceId);

                var x = await ratingService.Moyen(exp.ExperienceId);
                experience.AvgRating = x.ToString();
                await ExperienceService.PutExperienceAsync(experience.ExperienceId, experience);
            }
            if (comment != null)
            {
                await ratingService.Commenter(experience, user, comment);


            }
            if (exp == experience)
            {
                return NotFound();
            }

            return RedirectToAction("GetAll");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult CreateTransport(int id)
        {

            if (id != 0)
            {
                ViewData["Id"] = id;
            }

            else
            {
                ViewData["Id"] = JsonConvert.DeserializeObject<int>((string)TempData.Peek("expID"));
                TempData.Keep("Id");
            }
            return View();
        }
        public async Task<IActionResult> CreateTransport(TransportViewModel model, int id)
        {
            ViewData["Id"] = id;
            Experience experience = await ExperienceService.GetById(id);
            if (ModelState.IsValid)
            {

                string uniqueFileName = null;
                if (model.FileP != null)
                {

                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Files");

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.FileP.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    model.FileP.CopyTo(new FileStream(filePath, FileMode.Create));

                }


                Transport transport = new Transport

                {
                    DateDisp = model.DateDisp,
                    TypeTransport = model.TypeTransport,
                    Periode = model.Periode,
                    Image = uniqueFileName,

                    Prix = model.Prix,
                    ExperienceId = id

                };


                experience.Transport = transport;

                await ExperienceService.PutExperienceAsync(id, experience);
                await TransportService.Ajout(transport);

                return RedirectToAction("MesExperiences");

            }


            return View(model);
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult CreateNourriture(int id)
        {
            if (id != 0)
            {
                ViewData["Id"] = id;
            }

            else
            {
                ViewData["Id"] = JsonConvert.DeserializeObject<int>((string)TempData.Peek("expID"));
                TempData.Keep("Id");
            }
            return View();
        }



        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateNourriture(NourritureViewModel model, int id)
        {
            ViewData["Id"] = id;
            Experience experience = await ExperienceService.GetById(id);
            if (ModelState.IsValid)
            {

                string uniqueFileName = null;
                if (model.FileP != null)
                {

                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Files");

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.FileP.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    model.FileP.CopyTo(new FileStream(filePath, FileMode.Create));

                }


                Nourriture nourriture = new Nourriture

                {
                    Description = model.Description,
                    Image = uniqueFileName,
                    Plat = model.Plat,
                    Prix = model.Prix,
                    ExperienceId = id,
                    Type = model.Type
                };


                experience.Nourriture = nourriture;

                await ExperienceService.PutExperienceAsync(id, experience);
                TempData["expID"] = JsonConvert.SerializeObject(experience.ExperienceId);
                await NourritureService.Ajout(nourriture);

                return RedirectToAction("CreateTransport");

            }


            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult UpdateTransport(int id)
        {
            Transport t = TransportService.GetTransport(id);
            ViewData["Id"] = id;
            return View(t);
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> UpdateTransport(int id, Transport model, IFormFile FileP)
        {

            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (FileP != null)
                {

                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Files");

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + FileP.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    FileP.CopyTo(new FileStream(filePath, FileMode.Create));

                }
                Transport l = TransportService.GetTransport(id);


                l.ExperienceId = id;
                l.DateDisp = model.DateDisp;
                l.Periode = model.Periode;
                l.Prix = model.Prix;
                l.TypeTransport = model.TypeTransport;

                if (uniqueFileName != null)
                    l.Image = uniqueFileName;


                await TransportService.Update(l);
                return RedirectToAction("MesExperiences");

            }
            return View(model);



        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult UpdateNourriture(int id)
        {
            Nourriture l = NourritureService.GetNourriture(id);
            ViewData["Id"] = id;
            return View(l);
        }




        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> UpdateNourriture(int id, Nourriture model, IFormFile FileP)
        {

            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (FileP != null)
                {

                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Files");

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + FileP.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    FileP.CopyTo(new FileStream(filePath, FileMode.Create));

                }
                Nourriture l = NourritureService.GetNourriture(id);


                l.ExperienceId = id;
                l.Description = model.Description;
                l.Plat = model.Plat;
                l.Prix = model.Prix;
                l.Type = model.Type;

                if (uniqueFileName != null)
                    l.Image = uniqueFileName;


                await NourritureService.Update(l);
                return RedirectToAction("MesExperiences");

            }
            return View(model);



        }



        [AllowAnonymous]
        [HttpGet]
        public IActionResult UpdateLogement(int id)
        {
            Logement logement = LogementService.GetLogement(id);
            ViewData["Id"] = id;
            return View(logement);
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> UpdateLogement(int id, Logement model, IFormFile FileP)
        {

            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (FileP != null)
                {

                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Files");

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + FileP.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    FileP.CopyTo(new FileStream(filePath, FileMode.Create));

                }
                Logement l = LogementService.GetLogement(id);
                System.Diagnostics.Debug.WriteLine(" l'id du logement est " + l.LogementId);

                l.ExperienceId = id;
                l.Datedebut = model.Datedebut;
                l.DateFin = model.DateFin;
                l.Lieu = model.Lieu;
                l.NbJours = model.NbJours;
                l.Prix = model.Prix;
                l.Type = model.Type;

                if (uniqueFileName != null)
                    l.Image = uniqueFileName;


                await LogementService.Update(l);
                return RedirectToAction("MesExperiences");

            }
            return View(model);



        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult CreateLogement(int id)
        {
            if (id != 0)
            { ViewData["Id"] = id; }
            else
            {
                ViewData["Id"] = JsonConvert.DeserializeObject<int>((string)TempData.Peek("expID"));
                TempData.Keep("Id");
            }
            return View();
        }




        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateLogement(LogementViewmodel model, int id)
        {
            ViewData["Id"] = id;
            Experience experience = await ExperienceService.GetById(id);
            if (ModelState.IsValid)
            {

                string uniqueFileName = null;
                if (model.FileP != null)
                {

                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Files");

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.FileP.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    model.FileP.CopyTo(new FileStream(filePath, FileMode.Create));

                }


                Logement logement = new Logement

                {
                    Datedebut = model.Datedebut,
                    DateFin = model.DateFin,
                    Lieu = model.Lieu,
                    Image = uniqueFileName,
                    NbJours = model.NbJours,
                    Prix = model.Prix,
                    Type = model.Type,
                    ExperienceId = id
                };


                experience.Logement = logement;


                await ExperienceService.PutExperienceAsync(id, experience);
                await LogementService.Ajout(logement);
                TempData["expID"] = JsonConvert.SerializeObject(experience.ExperienceId);
                return RedirectToAction("CreateNourriture");

            }


            return View(model);
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
                TempData["expID"] = JsonConvert.SerializeObject(experience.ExperienceId);
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
            ViewData["ListeActivite"] = JsonConvert.DeserializeObject<ICollection<Activite>>((string)TempData.Peek("ListeActivite"));
            // ViewData["ListeActivite"] = (ICollection<Activite>)ActiviteService.GetActivite(ViewBag.experience1.ExperienceId);
            return View("ExperienceAffichage");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AffichageExperience(Experience model, IFormFile FileP, string details, int activiteId)
        {


            model.Activites = (IList<Activite>)ActiviteService.GetActivite(model.ExperienceId);
            if (activiteId != 0)
            {
                string uniqueFileName = null;
                if (FileP != null)
                {
                    // The image must be uploaded to the images folder in wwwroot
                    // To get the path of the wwwroot folder we are using the inject
                    // HostingEnvironment service provided by ASP.NET Core
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    // To make sure the file name is unique we are appending a new
                    // GUID value and and an underscore to the file name
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + FileP.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    // Use CopyTo() method provided by IFormFile interface to
                    // copy the file to wwwroot/images folder
                    FileP.CopyTo(new FileStream(filePath, FileMode.Create));

                }

                System.Diagnostics.Debug.WriteLine("l'id ta3 l'activité est " + activiteId);
                Activite a = await ActiviteService.GetActiviteById(activiteId);


                if (details != null)
                    a.Details = details;
                if (uniqueFileName != null)
                    a.Image = uniqueFileName;
                await ActiviteService.Update(a);
                if (details is null && uniqueFileName is null)
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
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Description(string src)
        {
            var ch = src.Remove(0, 8);
            var activite = ActiviteService.GetActiviteByImage(ch).Result;





            return PartialView("_Description", activite);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteExperience(int id)
        {
            await ExperienceService.DeleteExperienceAsync(id);
            return RedirectToAction("MesExperiences");
        }
    }
}

