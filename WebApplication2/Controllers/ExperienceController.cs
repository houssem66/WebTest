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
        private readonly ICommercantService commercantService;
        public ICollection<Activite> Activites = new Collection<Activite>();

        readonly private IActiviteService ActiviteService;
        readonly private INourritureService NourritureService;
        readonly private ILogementService LogementService;
        readonly private ITransportService TransportService;

        private readonly UserManager<Utilisateur> userManager;

        private readonly IUserService userService;
        private readonly IRatingService ratingService;
        readonly private IUserService UserService;

        public ExperienceController(ITransportService transportService, ILogementService _LogementService, IUserService _UserService, INourritureService _NourritureService, IWebHostEnvironment hostingEnvironment, IExperienceService experienceService, IActiviteService activiteService, UserManager<Utilisateur> userManager, IRatingService ratingService, SignInManager<Utilisateur> signInManager,ICommercantService _commercantService)
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
            commercantService = _commercantService;
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
                { try
                    { var list2 = ExperienceService.GetAllExperienceDetails(ch).ToList();

                        list = list.Concat(list2).ToList();
                    }
                    catch(Exception e)
                    {
                          var list2= ExperienceService.GetAllExperienceDetails(null).ToList();
                        list = list.Concat(list2).ToList();

                    }
                  
                   
                
                }
                ;

                return View(list);
            }
            return View(ExperienceService.GetAllExperienceDetails("").ToList());

        }

        [HttpGet]
       
         [Authorize(Policy = "CreateExperiencePolicy")]
        public IActionResult CreateExperience()
        {
            if (TempData["list"] != null) 
            { ViewData["list"] = JsonConvert.DeserializeObject<IList<Activite>>((string)TempData.Peek("list")); }
            if (TempData["Nourriture"] != null)
            { ViewData["Nourriture"] = JsonConvert.DeserializeObject<Nourriture>((string)TempData.Peek("Nourriture")); }
            if (TempData["Transport"] != null)
            { ViewData["Transport"] = JsonConvert.DeserializeObject<Logement>((string)TempData.Peek("Transport")); }
            if (TempData["Logement"] != null)
            { ViewData["Logement"] = JsonConvert.DeserializeObject<Logement>((string)TempData.Peek("Logement")); }

            //    IList<Activite> list = (IList<Activite>)ViewData["list"];
            //    if (list.Count() > 0) {

            //        TempData["list"] = JsonConvert.SerializeObject(list);

            //    }
            //    else
            //    {
            //        IList<Activite> activites = new List<Activite>();
            //        TempData["list"] = JsonConvert.SerializeObject(activites);
            //    }


            // ViewBag.Message = TempData["Message"];
            return View();
        }

        [HttpPost]

        [Authorize(Policy = "CreateExperiencePolicy")]
        public async Task<IActionResult> CreateExperience(ExperienceViewModel model,string Programmed,string obligatoire)
        { Boolean bol =false;
            if (TempData["Nourriture"] != null)
            { ViewData["supp"] = JsonConvert.DeserializeObject<Nourriture>((string)TempData.Peek("Nourriture")); }
            if (TempData["Transport"] != null)
            { ViewData["suppT"] = JsonConvert.DeserializeObject<Transport>((string)TempData.Peek("Transport")); }
            if (TempData["Logement"] != null)
            { ViewData["suppL"] = JsonConvert.DeserializeObject<Logement>((string)TempData.Peek("Logement")); }

            if (TempData["list"] != null)
            { ViewData["ok"] = JsonConvert.DeserializeObject<IList<Activite>>((string)TempData.Peek("list")); }
           if (Programmed == "Oui")
            {
                if (ModelState.IsValid)
                {
                    ViewData["list"] = JsonConvert.DeserializeObject<IList<Activite>>((string)TempData.Peek("list"));
                    IList<Activite> list = (IList<Activite>)ViewData["list"];
                    string id = userManager.GetUserId(User);
                    if (Programmed == "Oui")
                    { bol = true; }
                    Experience experience = new Experience
                    {Theme=obligatoire,
                        Titre = model.Titre,
                        Lieu = model.Lieu,
                        TypeExperience = model.TypeExperience,
                        dateDebut = model.dateDebut,
                        dateFin = model.dateFin,
                        Saison = model.Saison,
                        NbPlaces = model.NbPlaces,
                        tarif = model.tarif,
                        Activites = list,
                        Description = model.Description,
                        Commerçant = await commercantService.GetCommerçantById(id),
                        Programmed = bol
                    };
                    var result = await ExperienceService.InsertExperience(experience);
                    Experience experience1 = await ExperienceService.GetById((int)result);
                    if (TempData["Nourriture"] != null)
                    {
                        ViewData["Nourriture"] = JsonConvert.DeserializeObject<Nourriture>((string)TempData.Peek("Nourriture"));
                        var nourriture = (Nourriture)ViewData["Nourriture"];
                        nourriture.ExperienceId = experience1.ExperienceId;
                        if (nourriture.Prix > 0)
                        {
                            await NourritureService.Ajout(nourriture);
                        }

                    }
                    if (TempData["Logement"] != null)
                    {
                        ViewData["Logement"] = JsonConvert.DeserializeObject<Logement>((string)TempData.Peek("Logement"));
                        var logement = (Logement)ViewData["Logement"];
                        logement.ExperienceId = experience1.ExperienceId;
                        if (logement.Prix > 0)
                        {
                            await LogementService.Ajout(logement);
                        }

                    }
                    if (TempData["Transport"] != null)
                    {
                        ViewData["Transport"] = JsonConvert.DeserializeObject<Transport>((string)TempData.Peek("Transport"));
                        var transport = (Transport)ViewData["Transport"];
                        transport.ExperienceId = experience1.ExperienceId;
                        if (transport.Prix > 0)
                        {
                            await TransportService.Ajout(transport);
                        }

                    }





                    return RedirectToAction("Details", new { id = experience1.ExperienceId });
                }
            }
               else if (Programmed == "Non")
            {
                model.dateDebut = DateTime.Today;
                model.dateFin = DateTime.Today;
                if (ModelState.IsValid)
                {
                    ViewData["list"] = JsonConvert.DeserializeObject<IList<Activite>>((string)TempData.Peek("list"));
                    IList<Activite> list = (IList<Activite>)ViewData["list"];
                    string id = userManager.GetUserId(User);
                    if (Programmed == "Oui")
                    { bol = true; }
                    Experience experience = new Experience
                    { Theme = obligatoire,
                        Titre = model.Titre,
                        Lieu = model.Lieu,
                        TypeExperience = model.TypeExperience,
                        dateDebut = model.dateDebut,
                        dateFin = model.dateFin,
                        Saison = model.Saison,
                        NbPlaces = model.NbPlaces,
                        tarif = model.tarif,
                        Activites = list,
                        Description = model.Description,
                        Commerçant = await commercantService.GetCommerçantById(id),
                        Programmed = bol
                    };
                    var result = await ExperienceService.InsertExperience(experience);
                    Experience experience1 = await ExperienceService.GetById((int)result);
                    if (TempData["Nourriture"] != null)
                    {
                        ViewData["Nourriture"] = JsonConvert.DeserializeObject<Nourriture>((string)TempData.Peek("Nourriture"));
                        var nourriture = (Nourriture)ViewData["Nourriture"];
                        nourriture.ExperienceId = experience1.ExperienceId;
                        if (nourriture.Prix > 0)
                        {
                            await NourritureService.Ajout(nourriture);
                        }

                    }
                    if (TempData["Logement"] != null)
                    {
                        ViewData["Logement"] = JsonConvert.DeserializeObject<Logement>((string)TempData.Peek("Logement"));
                        var logement = (Logement)ViewData["Logement"];
                        logement.ExperienceId = experience1.ExperienceId;
                        if (logement.Prix > 0)
                        {
                            await LogementService.Ajout(logement);
                        }

                    }
                    if (TempData["Transport"] != null)
                    {
                        ViewData["Transport"] = JsonConvert.DeserializeObject<Transport>((string)TempData.Peek("Transport"));
                        var transport = (Transport)ViewData["Transport"];
                        transport.ExperienceId = experience1.ExperienceId;
                        if (transport.Prix > 0)
                        {
                            await TransportService.Ajout(transport);
                        }

                    }





                    return RedirectToAction("Details", new { id = experience1.ExperienceId });
                }
            }


            return View(model);

        }
        [Authorize(Policy = "CreateExperiencePolicy")]
        public IActionResult CreateActivite()
        {

            return View();

        }
        [HttpPost]

        [Authorize(Policy = "CreateExperiencePolicy")]
        public IActionResult CreateActivite(ActiviteViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.FileP != null)
                {
                    // The image must be uploaded to the images folder in wwwroot
                    // To get the path of the wwwroot folder we are using the inject
                    // HostingEnvironment service provided by ASP.NET Core
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    // To make sure the file name is unique we are appending a new
                    // GUID value and and an underscore to the file name
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.FileP.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    // Use CopyTo() method provided by IFormFile interface to
                    // copy the file to wwwroot/images folder
                    model.FileP.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                Activite activite = new Activite
                {
                    Details = model.Details,
                    dateDebut = model.dateDebut,
                    dateFin = model.dateFin,
                    Titre = model.Titre,
                    Duree = model.Duree,
                    Image = uniqueFileName,
                };
                if (TempData["list"] == null)
                {
                    IList<Activite> activites = new List<Activite>();
                    activites.Add(activite);
                    TempData["list"] = JsonConvert.SerializeObject(activites);
                    System.Diagnostics.Debug.WriteLine("Nouveau liste :" + activites.Count());
                }
                else
                { 
                    ViewData["list"] = JsonConvert.DeserializeObject<IList<Activite>>((string)TempData.Peek("list"));
                    IList<Activite> list = (IList<Activite>)ViewData["list"];
                    if (list.Count() < 6)
                    {
                        list.Add(activite);
                        TempData["list"] = JsonConvert.SerializeObject(list);
                    }
                    else { return View("Max"); }
                   
                  
                    
                    System.Diagnostics.Debug.WriteLine("Ancien:" + list.Count());
                }        
            }

            return NoContent();

        }
        public IActionResult ModifierActvite()
        {

            return View();

        }
        [HttpPost]
        [Authorize(Policy = "CreateExperiencePolicy")]
        public IActionResult ModifierActvite(ActiviteViewModel model)
        {
            ViewData["Modification"] = JsonConvert.DeserializeObject<IList<Activite>>((string)TempData.Peek("list"));
            IList<Activite> list = (IList<Activite>)ViewData["Modification"];
           list[model.Index - 4].Titre=model.Titre;
           list[model.Index - 4].Details = model.Details;
            list[model.Index - 4].Duree = model.Duree;

            TempData["list"] = JsonConvert.SerializeObject(list);
            ViewData["show"] = JsonConvert.DeserializeObject<IList<Activite>>((string)TempData.Peek("list"));
            TempData.Keep("list");
            return NoContent();

        }
        public IActionResult DeleteActivite()
        {

            return View();

        }
        [HttpPost]
        [Authorize(Policy = "CreateExperiencePolicy")]
        public IActionResult DeleteActivite(ActiviteViewModel model)
        {
            ViewData["Modification"] = JsonConvert.DeserializeObject<IList<Activite>>((string)TempData.Peek("list"));
            IList<Activite> list = (IList<Activite>)ViewData["Modification"];
            list.RemoveAt(model.Index - 4);

            TempData["list"] = JsonConvert.SerializeObject(list);
            ViewData["show"] = JsonConvert.DeserializeObject<IList<Activite>>((string)TempData.Peek("list"));
            TempData.Keep("list");
            return NoContent();

        }
        [HttpPost]
        [Authorize(Policy = "CreateExperiencePolicy")]
        public ActionResult AfficherActivite(string type)
        {
               ViewData["show"] = JsonConvert.DeserializeObject<IList<Activite>>((string)TempData.Peek("list"));
                TempData.Keep("list");
          
            return PartialView("_Activité");

        }
        [HttpPost]
        [Authorize(Policy = "CreateExperiencePolicy")]
        public ActionResult AfficherNourriture(string type)
        {
            

            return PartialView("_Nourriture");

        }
        public ActionResult AfficherTransport(string type)
        {


            return PartialView("_Transport");

        }
        public ActionResult AfficherLogement(string type)
        {


            return PartialView("_Logement");

        }
        [HttpPost]
        [Authorize(Policy = "CreateExperiencePolicy")]
        public ActionResult AfficherModal(string type)
        {

                ViewData["show"] = JsonConvert.DeserializeObject<IList<Activite>>((string)TempData.Peek("list"));
                TempData.Keep("list");
            
            return PartialView("_Modal");

        }
        [HttpGet]
        [Authorize(Policy = "CreateExperiencePolicy")]
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



        [Authorize(Policy = "CreateExperiencePolicy")]
        [HttpGet]
        public IActionResult CreateTransport(int id)
        {
            return View();
        }
        [Authorize(Policy = "CreateExperiencePolicy")]
        public IActionResult CreateTransport(TransportViewModel model, int id)
        {
           
            
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.FileP != null)
                {
                    // The image must be uploaded to the images folder in wwwroot
                    // To get the path of the wwwroot folder we are using the inject
                    // HostingEnvironment service provided by ASP.NET Core
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    // To make sure the file name is unique we are appending a new
                    // GUID value and and an underscore to the file name
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.FileP.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    // Use CopyTo() method provided by IFormFile interface to
                    // copy the file to wwwroot/images folder
                    model.FileP.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                Transport transport = new Transport
                {
                    DateDisp = model.DateDisp,
                    TypeTransport = model.TypeTransport,
                    Prix = model.Prix,
                   
                    Image = uniqueFileName,
                };
                if (TempData["Transport"] == null)
                {
                    TempData["Transport"] = JsonConvert.SerializeObject(transport);
                    ;
                }
                else
                {
                    ViewData["Trans"] = JsonConvert.DeserializeObject<Transport>((string)TempData.Peek("Transport"));
                    Transport nourriture1 = (Transport)ViewData["Trans"];


                    TempData["Transport"] = JsonConvert.SerializeObject(nourriture1);

                   
                }
            }



            return NoContent();
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult CreateNourriture(int id)
        {
           
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult CreateNourriture(NourritureViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.ImageNourriture != null)
                {
                    // The image must be uploaded to the images folder in wwwroot
                    // To get the path of the wwwroot folder we are using the inject
                    // HostingEnvironment service provided by ASP.NET Core
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    // To make sure the file name is unique we are appending a new
                    // GUID value and and an underscore to the file name
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageNourriture.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    // Use CopyTo() method provided by IFormFile interface to
                    // copy the file to wwwroot/images folder
                    model.ImageNourriture.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                Nourriture nourriture = new Nourriture
                {
                    Description = model.DescriptionNourriture,
                    Plat = model.Plat,
                    Prix = model.Prix,
                    Type = model.Type,
                    Image = uniqueFileName,
                };
                if (TempData["Nourriture"] == null)
                {
                    TempData["Nourriture"] = JsonConvert.SerializeObject(nourriture);
                    System.Diagnostics.Debug.WriteLine("Nouveau nourriture :" + nourriture.Plat);
                }
                else
                {
                    ViewData["Nour"] = JsonConvert.DeserializeObject<Nourriture>((string)TempData.Peek("Nourriture"));
                   Nourriture nourriture1 = (Nourriture)ViewData["Nour"];
                  
                        
                        TempData["Nourriture"] = JsonConvert.SerializeObject(nourriture1);
              
                    System.Diagnostics.Debug.WriteLine("Ancien Nourriture:" + nourriture1.ToString());
                }
            }

            return NoContent();
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
           
            return View();
        }




        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateLogement(LogementViewmodel model, int id)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.FileP != null)
                {
                    // The image must be uploaded to the images folder in wwwroot
                    // To get the path of the wwwroot folder we are using the inject
                    // HostingEnvironment service provided by ASP.NET Core
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    // To make sure the file name is unique we are appending a new
                    // GUID value and and an underscore to the file name
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.FileP.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    // Use CopyTo() method provided by IFormFile interface to
                    // copy the file to wwwroot/images folder
                    model.FileP.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                Logement logement = new Logement
                {
                   Datedebut=model.Datedebut,
                   Lieu=model.Lieu,
                    NbJours = model.NbJours,
                    SubCategory = model.SubCategory,
                    Type = model.Type,
                    Prix = model.Prix,

                    Image = uniqueFileName,
                };
                if (TempData["Logement"] == null)
                {
                    TempData["Logement"] = JsonConvert.SerializeObject(logement);
                    ;
                }
                else
                {
                    ViewData["Log"] = JsonConvert.DeserializeObject<Logement>((string)TempData.Peek("Logement"));
                    Logement Logement1 = (Logement)ViewData["Log"];


                    TempData["Logement"] = JsonConvert.SerializeObject(Logement1);


                }
            }


            return NoContent();
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
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Orgasnime(string src)
        {


            if (src.Equals("Organisme"))
            {



                return PartialView("_Organisme");
            }

            else if (src.Equals("Individu"))
            {

                return new EmptyResult();
            }


            return PartialView("_Individu");

        }

        [HttpGet]
        public async Task<IActionResult> ModifierExperience(int id)
        {
            var exp = await ExperienceService.GetById(id);
            ExperienceViewModel experienceViewModel = new ExperienceViewModel { dateDebut = exp.dateDebut,
                Titre = exp.Titre,
                
                Lieu = exp.Lieu,
                TypeExperience = exp.TypeExperience,
              
                dateFin = exp.dateFin,
                Saison = exp.Saison,
                NbPlaces = exp.NbPlaces,
                tarif = exp.tarif,
                
                Description = exp.Description,
               
            };
            return View(experienceViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> ModifierExperience(Experience model)
        {
            return View(); 
        }
    }
  
}



