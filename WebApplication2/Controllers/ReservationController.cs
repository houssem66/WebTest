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

using X.PagedList;



namespace TourMe.Web.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ITransportExtService transportExtService;
        private readonly INourritureExtService nourritureExtService1;
        private readonly IExperienceService experienceService;
        private readonly UserManager<Utilisateur> userManager;
        private readonly IReservationService reservationService;
        private readonly IPanierService panierService;
        private readonly ILogementextService logementService;

        public ReservationController(ITransportExtService transportExtService,INourritureExtService nourritureExtService1,IExperienceService _experienceService, UserManager<Utilisateur> userManager,IReservationService _reservationService,IPanierService panierService, ILogementextService _logementService)
        {
            this.transportExtService = transportExtService;
            this.nourritureExtService1 = nourritureExtService1;
            experienceService = _experienceService;
            this.userManager = userManager;
            reservationService = _reservationService;
            this.panierService = panierService;
            logementService = _logementService;
        }
        [HttpGet]
        public async Task<IActionResult> Reserver(int id)
        { var exp = await experienceService.GetExperienceByIdAsync(id);
            ViewBag.exp = exp;
            ViewBag.num = experienceService.GetExperienceByIdAsync(id).Result.Ratings.Where(x => x.Commentaire != null).Count();
            ViewBag.img = exp.Activites.First().Image;
            ReservationModel model = new ReservationModel { idExp = id };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Reserver( ReservationModel model)
        {
            var exp = await experienceService.GetExperienceByIdAsync(model.idExp);
            ViewBag.exp = exp;
            ViewBag.num = experienceService.GetExperienceByIdAsync(model.idExp).Result.Ratings.Where(x => x.Commentaire != null).Count();
            ViewBag.img = exp.Activites.First().Image;
            string idx = userManager.GetUserId(User);
            
            if (ModelState.IsValid)
            {
                DateTime time = new DateTime();
                Reservation reservation = new Reservation { 
                    CardType = model.CardType,
                    DateReservation=exp.dateDebut,
                    CCV=model.CCV,
                    ExperienceId=exp.ExperienceId,
                    UtilisateurId= idx,
                    ExpirationDate=time,
                    NbrReservation=model.NbrReservation,
                     CodePostale=model.CodePostale,
                     NumeroCarte=model.NumeroCarte,
                     Tariff=model.Tariff
                };
                var queuery =  reservationService.Ajout(reservation);
                if (queuery.IsCompleted)
                {
                    return RedirectToAction("profil", "account");

                }
                
            }
           
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> AddToCart(int id)
        {
            List<Experience> experiences = new List<Experience>();
            var exp = await experienceService.GetExperienceByIdAsync(id);
            int experienceCount = 0;
            try
            {
                var liste = JsonConvert.DeserializeObject<IList<Experience>>(HttpContext.Session.GetString("Experience"));
                experiences = (List<Experience>)liste;
            
                if (experiences.Find(e=>e.ExperienceId==id)==null)
                {
                    experiences.Add(exp);

                }
              
                experienceCount = experiences.Count;
                HttpContext.Session.SetString("Experience", JsonConvert.SerializeObject(experiences));
                HttpContext.Session.SetString("ExperienceCount", JsonConvert.SerializeObject(experienceCount));
            }

            catch
            {
                experiences.Add(exp);
                experienceCount = experiences.Count;
                HttpContext.Session.SetString("Experience", JsonConvert.SerializeObject(experiences));
                HttpContext.Session.SetString("ExperienceCount", JsonConvert.SerializeObject(experienceCount));
            }




            return RedirectToAction("GetAll", "Experience"); ;
        }



        [HttpGet]
        public  IActionResult ItemList()
        { PanierViewModel p=new PanierViewModel();
          
            try {

                var liste = JsonConvert.DeserializeObject<IList<Experience>>(HttpContext.Session.GetString("Experience"));
                p.Experiences = liste;

                 }
           catch
            {
                return RedirectToAction("PanierVide", "Reservation");
            }


            return View(p);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ItemList(decimal prix,int ide)
        {   
            Experience x = await experienceService.GetExperienceByIdAsync(ide);
            List<Experience> list = new List<Experience>();
         



          
           
                Panier panier = new Panier

                {
                  
                    Prix = prix,
                User=await userManager.GetUserAsync(User),
                Experiences=new List<Experience>()

                };
              await panierService.InsertAsync(panier,ide);
             return RedirectToAction("ReserverService", "Reservation");
        }





        [HttpGet]
        public IActionResult PanierVide()
        {
            

            return View();
        }



        [HttpGet]
        [AllowAnonymous]
        public IActionResult ReserverService()
        {
           
            
            Panier p = panierService.GetPanierByuserId(userManager.GetUserId(User)).Last();
            HttpContext.Session.SetString("Panier", JsonConvert.SerializeObject(p, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        })); 
            ViewData["data"] = p;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ReserverService(PanierViewModel model)
        {
            Panier panier = JsonConvert.DeserializeObject<Panier>(HttpContext.Session.GetString("Panier"));
            panier.DatedebutL = model.DatedebutL;
            panier.DateDispoTrans = model.DateDispoTrans;
            panier.DateReservationRes = model.DateReservationRes;
            panier.RemarquesLogement = model.RemarquesLogement;
            panier.RemarquesNourriture = model.RemarquesNourriture;
            panier.RemarquesTransport = model.RemarquesTransport;
            await panierService.Update(panier,null);
            return RedirectToAction("Index", "Home");

        }



        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetDetailLogement(int? page)
        {
          
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            return PartialView("_Detail", logementService.GetAllLogements().ToPagedList(pageNumber, pageSize));
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetDetailNourriture(int? page)
        {
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            return PartialView("_DetailNourriture", nourritureExtService1.GetAllLogements().ToPagedList(pageNumber, pageSize));
        }



        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetDetailTransport(int? page)
        {
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            return PartialView("_DetailTransport", transportExtService.GetAllLogements().ToPagedList(pageNumber, pageSize));
        }


      


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AjoutLogement(int id)
        {
            
            ServiceLogment logment  =await logementService.GetById(id);
            // Panier panier = JsonConvert.DeserializeObject<Panier>(HttpContext.Session.GetString("Panier"));

            Panier panier = await panierService.GetPan(panierService.GetPanierByuserId(userManager.GetUserId(User)).Last().Id);

            try
            {
                panier.Logments = JsonConvert.DeserializeObject<IList<ServiceLogment>>(HttpContext.Session.GetString("Logements"));
                
                HttpContext.Session.SetString("Logements", JsonConvert.SerializeObject(panier.Logments, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }));
                await panierService.Update(panier,logment);
                //TempData["tet"] = logment.PrixParNuit.ToString();
               
                //ViewBag["L"] = logment.PrixParNuit;
                //  var a = 0;
                //HttpContext.Session.SetString("Panier", JsonConvert.SerializeObject(panier, Formatting.None,
                //        new JsonSerializerSettings()
                //        {
                //            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                //        }));
            }
            catch
            {

                panier.Logments = new List<ServiceLogment>();
                
                HttpContext.Session.SetString("Logements", JsonConvert.SerializeObject(panier.Logments, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }));



                await panierService.Update(panier,logment);
               
                //ViewBag["L"] = logment.PrixParNuit;
              
                //HttpContext.Session.SetString("Panier", JsonConvert.SerializeObject(panier, Formatting.None,
                //       new JsonSerializerSettings()
                //       {
                //           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                //       }));
            }

            //panier.Logments.Add(logment);
            //  HttpContext.Session.SetString("Logements", JsonConvert.SerializeObject(panier.Logments));
            //await panierService.Update(panier);

            TempData["tet"] = logment.PrixParNuit.ToString();
            return NoContent();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AjoutNourriture(int id)
        {

            ServiceNouritture nouritture = await nourritureExtService1.GetById(id);
            // Panier panier = JsonConvert.DeserializeObject<Panier>(HttpContext.Session.GetString("Panier"));




            Panier panier = await panierService.GetPan(panierService.GetPanierByuserId(userManager.GetUserId(User)).Last().Id);





            try
            {
                panier.Nourittures = JsonConvert.DeserializeObject<IList<ServiceNouritture>>(HttpContext.Session.GetString("Nourritures"));

                HttpContext.Session.SetString("Nourritures", JsonConvert.SerializeObject(panier.Nourittures, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }));
                await panierService.UpdateNourriture(panier, nouritture);
                //HttpContext.Session.SetString("Panier", JsonConvert.SerializeObject(panier, Formatting.None,
                //        new JsonSerializerSettings()
                //        {
                //            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                //        }));
            }
            catch
            {

                panier.Logments = new List<ServiceLogment>();

                HttpContext.Session.SetString("Nourritures", JsonConvert.SerializeObject(panier.Logments, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }));



                await panierService.UpdateNourriture(panier, nouritture);
                //HttpContext.Session.SetString("Panier", JsonConvert.SerializeObject(panier, Formatting.None,
                //       new JsonSerializerSettings()
                //       {
                //           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                //       }));
            }

            //panier.Logments.Add(logment);
            //  HttpContext.Session.SetString("Logements", JsonConvert.SerializeObject(panier.Logments));
            //await panierService.Update(panier);


            return NoContent();
        }




        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AjoutTransport(int id)
        {

            ServiceTransport transport = await transportExtService.GetLogementById(id);
            // Panier panier = JsonConvert.DeserializeObject<Panier>(HttpContext.Session.GetString("Panier"));




            Panier panier = await panierService.GetPan(panierService.GetPanierByuserId(userManager.GetUserId(User)).Last().Id);





            try
            {
                panier.Transports = JsonConvert.DeserializeObject<IList<ServiceTransport>>(HttpContext.Session.GetString("Transports"));

                HttpContext.Session.SetString("Transports", JsonConvert.SerializeObject(panier.Nourittures, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }));
                await panierService.UpdateTransport(panier, transport);
                //HttpContext.Session.SetString("Panier", JsonConvert.SerializeObject(panier, Formatting.None,
                //        new JsonSerializerSettings()
                //        {
                //            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                //        }));
            }
            catch
            {

                panier.Transports = new List<ServiceTransport>();

                HttpContext.Session.SetString("Transports", JsonConvert.SerializeObject(panier.Transports, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }));



                await panierService.UpdateTransport(panier, transport);
                //HttpContext.Session.SetString("Panier", JsonConvert.SerializeObject(panier, Formatting.None,
                //       new JsonSerializerSettings()
                //       {
                //           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                //       }));
            }

            //panier.Logments.Add(logment);
            //  HttpContext.Session.SetString("Logements", JsonConvert.SerializeObject(panier.Logments));
            //await panierService.Update(panier);


            return NoContent();
        }




        [HttpPost]
        [AllowAnonymous]
        public ActionResult Test(string type)
        {
            List<ServiceLogment> logments = new List<ServiceLogment>();
            logments = (List<ServiceLogment>)logementService.GetAllLogements();


            if (type.ToLower().Equals("oui"))
            {


                System.Diagnostics.Debug.WriteLine("Le type est" + type);
                return PartialView("_Logement",logments);
            }

            else if (type.Equals("non"))
            {

                return new EmptyResult();
            }
            return null;
        }

  


        




    }
}
