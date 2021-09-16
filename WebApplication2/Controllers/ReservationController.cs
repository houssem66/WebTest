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
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Helpers;
using TourMe.Data.Entities;
using TourMe.Web.Models;

using X.PagedList;



namespace TourMe.Web.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ITransportExtService transportExtService;
        private readonly INourritureExtService nourritureExtService1;
        private readonly IExperienceService experienceService;
        private readonly UserManager<Utilisateur> userManager;
        private readonly IReservationService reservationService;
        private readonly IPanierService panierService;
        private readonly ILogementextService logementService;

        public ReservationController(IHttpClientFactory clientFactory, ITransportExtService transportExtService,INourritureExtService nourritureExtService1,IExperienceService _experienceService, UserManager<Utilisateur> userManager,IReservationService _reservationService,IPanierService panierService, ILogementextService _logementService)
        {
            this.clientFactory = clientFactory;
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
            //var exp = await experienceService.GetAllExperienceDetails(null).Single(e=>e.ExperienceId==id).re;
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
                //HttpContext.Session.SetString("Experience", JsonConvert.SerializeObject(experiences));
                //HttpContext.Session.SetString("ExperienceCount", JsonConvert.SerializeObject(experienceCount));
            }

            catch
            {
                experiences.Add(exp);
                experienceCount = experiences.Count;
              
            }


            HttpContext.Session.SetString("Experience", JsonConvert.SerializeObject(experiences, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }));
            HttpContext.Session.SetString("ExperienceCount", JsonConvert.SerializeObject(experienceCount));

            return RedirectToAction("GetAll", "Experience"); ;
        }



        [HttpGet]
        public  IActionResult ItemList()
        { PanierViewModel p=new PanierViewModel();
          
            try {

                var liste = JsonConvert.DeserializeObject<IList<Experience>>(HttpContext.Session.GetString("Experience"));
                p.Experiences = liste;
                if (liste.Count == 0)

                    return RedirectToAction("PanierVide", "Reservation");

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
            Panier panier;
            try
            {
                panier = JsonConvert.DeserializeObject<Panier>(HttpContext.Session.GetString("Panier"));


            }
            catch
            {

                panier = panierService.GetPanierByuserId(userManager.GetUserId(User)).Last();
            }
             
            HttpContext.Session.SetString("Panier", JsonConvert.SerializeObject(panier, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        })); 
            ViewData["data"] = panier;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ReserverService(PanierViewModel model)
        { decimal countPlat = 0;
            decimal countTrans = 0;
            decimal countlogement = 0;
            Panier panier = JsonConvert.DeserializeObject<Panier>(HttpContext.Session.GetString("Panier"));
            panier.DatedebutL = model.DatedebutL;
            panier.DateDispoTrans = model.DateDispoTrans;
            panier.DateReservationRes = model.DateReservationRes;
            panier.RemarquesLogement = model.RemarquesLogement;
            panier.RemarquesNourriture = model.RemarquesNourriture;
            panier.RemarquesTransport = model.RemarquesTransport;
            panier.NbrJoursLogement = model.NbrJoursLogement;
            panier.NbrJoursTrans = model.NbrJoursTrans;
            panier.NbrPlat = model.NbrPlat;
            panier.DateReservationRes = model.DateReservationRes;
            panier.Quantite =(int)( panier.Prix / panier.Experiences.LastOrDefault().tarif);
            if (panier.Nourittures.Count != 0) countPlat = (int)model.NbrPlat* panier.Nourittures.LastOrDefault().Prix; 
            if (panier.Logments.Count != 0) countlogement = (int)model.NbrJoursLogement* panier.Logments.LastOrDefault().PrixParNuit; 
            if (panier.Transports.Count != 0) countTrans = (int)model.NbrJoursTrans* panier.Transports.LastOrDefault().Prix;

            panier.Prix+= countPlat+  countTrans +  countlogement;
            
            await panierService.UpdatePanier(panier);
            return RedirectToAction("Facture", "Reservation");

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
            return PartialView("_DetailNourriture", nourritureExtService1.GetAllNourriture().ToPagedList(pageNumber, pageSize));
        }



        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetDetailTransport(int? page)
        {
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            return PartialView("_DetailTransport", transportExtService.GetAllTransports().ToPagedList(pageNumber, pageSize));
        }


      


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AjoutLogement(int id)
        {
            
            ServiceLogment logment  =await logementService.GetById(id);
            Panier panier;
            // Panier

       

            try
            {
                panier = JsonConvert.DeserializeObject<Panier>(HttpContext.Session.GetString("Panier"));
                panier.Logments = JsonConvert.DeserializeObject<IList<ServiceLogment>>(HttpContext.Session.GetString("Logements"));
                
                HttpContext.Session.SetString("Logements", JsonConvert.SerializeObject(panier.Logments, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }));
               
            }
            catch
            {
                panier = await panierService.GetPan(panierService.GetPanierByuserId(userManager.GetUserId(User)).Last().Id);
                panier.Logments = new List<ServiceLogment>();
                
                HttpContext.Session.SetString("Logements", JsonConvert.SerializeObject(panier.Logments, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }));



               
               
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
            await panierService.Update(panier, logment);
            HttpContext.Session.SetString("Panier", JsonConvert.SerializeObject(panier, Formatting.None,
                       new JsonSerializerSettings()
                       {
                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                       }));
            TempData["tet"] = logment.PrixParNuit.ToString();
            return NoContent();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AjoutNourriture(int id)
        {

            ServiceNouritture nouritture = await nourritureExtService1.GetById(id);
            // Panier panier = JsonConvert.DeserializeObject<Panier>(HttpContext.Session.GetString("Panier"));


            Panier panier ;








            try
            {
                 panier = JsonConvert.DeserializeObject<Panier>(HttpContext.Session.GetString("Panier"));
                panier.Nourittures = JsonConvert.DeserializeObject<IList<ServiceNouritture>>(HttpContext.Session.GetString("Nourritures"));

                HttpContext.Session.SetString("Nourritures", JsonConvert.SerializeObject(panier.Nourittures, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }));
              
                //HttpContext.Session.SetString("Panier", JsonConvert.SerializeObject(panier, Formatting.None,
                //        new JsonSerializerSettings()
                //        {
                //            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                //        }));
            }
            catch
            {
                 panier = await panierService.GetPan(panierService.GetPanierByuserId(userManager.GetUserId(User)).Last().Id);
                panier.Logments = new List<ServiceLogment>();

                HttpContext.Session.SetString("Nourritures", JsonConvert.SerializeObject(panier.Logments, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }));



              
                //HttpContext.Session.SetString("Panier", JsonConvert.SerializeObject(panier, Formatting.None,
                //       new JsonSerializerSettings()
                //       {
                //           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                //       }));
            }

            //panier.Logments.Add(logment);
            //  HttpContext.Session.SetString("Logements", JsonConvert.SerializeObject(panier.Logments));
            //await panierService.Update(panier);
            await panierService.UpdateNourriture(panier, nouritture);
            HttpContext.Session.SetString("Panier", JsonConvert.SerializeObject(panier, Formatting.None,
                 new JsonSerializerSettings()
                 {
                     ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                 }));
            return NoContent();
        }




        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AjoutTransport(int id)
        {

            ServiceTransport transport = await transportExtService.GetLogementById(id);
            // Panier panier = JsonConvert.DeserializeObject<Panier>(HttpContext.Session.GetString("Panier"));




            Panier panier;





            try
            {
                panier = JsonConvert.DeserializeObject<Panier>(HttpContext.Session.GetString("Panier"));
                panier.Transports = JsonConvert.DeserializeObject<IList<ServiceTransport>>(HttpContext.Session.GetString("Transports"));

                HttpContext.Session.SetString("Transports", JsonConvert.SerializeObject(panier.Nourittures, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }));
               
                //HttpContext.Session.SetString("Panier", JsonConvert.SerializeObject(panier, Formatting.None,
                //        new JsonSerializerSettings()
                //        {
                //            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                //        }));
            }
            catch
            {
                panier = await panierService.GetPan(panierService.GetPanierByuserId(userManager.GetUserId(User)).Last().Id);
                panier.Transports = new List<ServiceTransport>();

                HttpContext.Session.SetString("Transports", JsonConvert.SerializeObject(panier.Transports, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }));



              
                //HttpContext.Session.SetString("Panier", JsonConvert.SerializeObject(panier, Formatting.None,
                //       new JsonSerializerSettings()
                //       {
                //           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                //       }));
            }

            //panier.Logments.Add(logment);
            //  HttpContext.Session.SetString("Logements", JsonConvert.SerializeObject(panier.Logments));
            //await panierService.Update(panier);
            await panierService.UpdateTransport(panier, transport);
            HttpContext.Session.SetString("Panier", JsonConvert.SerializeObject(panier, Formatting.None,
               new JsonSerializerSettings()
               {
                   ReferenceLoopHandling = ReferenceLoopHandling.Ignore
               }));

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



        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Facture()
        {
            var count =  HttpContext.Session.GetString("ExperienceCount");
          
            var xd = int.Parse(count);
            xd--;
           
            HttpContext.Session.SetString("ExperienceCount", JsonConvert.SerializeObject(xd));
            Panier panier =  panierService.GetPanierByuserId(userManager.GetUserId(User)).LastOrDefault();
            var liste = JsonConvert.DeserializeObject<IList<Experience>>(HttpContext.Session.GetString("Experience"));
            Experience e =liste.SingleOrDefault(e=>e.ExperienceId== panier.Experiences.LastOrDefault().ExperienceId) ;
            liste.Remove(e);

            HttpContext.Session.SetString("Experience", JsonConvert.SerializeObject(liste));


            var request = new HttpRequestMessage(HttpMethod.Post,
      "https://sandbox.paymee.tn/api/v1/payments/create");
            request.Headers.Add("Authorization", "Token af8b516edd51a1ba30c0b049c8781a1152c4e30f");
            //request.Headers.Add("Content-Type", "application/json");
            Payme obj = new Payme { vendor = 1925, amount = (float)panier.Prix, note = "command" };
            // request.Content.CopyToAsync("data");
            request.Content = new StringContent(JsonConvert.SerializeObject(obj), System.Text.Encoding.UTF8, "application/json");


            var client = clientFactory.CreateClient();
            var response = await client.SendAsync(request);


            if (response.IsSuccessStatusCode)
            {

                int i = 62;
                var x = response.Content.ReadAsStringAsync().Result;
                ////Output output =
                //JsonSerializer.Deserialize<Output>(response.Content);
                //var x = response.RequestMessage;
                var ch = "";
                while (x[i] != '"')
                {

                    ch = ch + x[i];
                    i++;

                }

                ViewBag.token = ch;
            }
            else
            {

            }




            return View(panier);
        }

        [HttpGet]
        [AllowAnonymous]

        public IActionResult MesReservations()

        {
            ViewBag.user = userManager.GetUserAsync(User).Result;
            // ViewBag.Best = ExperienceService.BestExperience();
            var list = panierService.GetPanierByUser(userManager.GetUserId(User));
            return View(list);

        }




    }
}
