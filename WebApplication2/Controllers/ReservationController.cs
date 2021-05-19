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
    public class ReservationController : Controller
    {
        private readonly IExperienceService experienceService;
        private readonly UserManager<Utilisateur> userManager;
        private readonly IReservationService reservationService;

        public ReservationController(IExperienceService _experienceService, UserManager<Utilisateur> userManager,IReservationService _reservationService)
        {
            experienceService = _experienceService;
            this.userManager = userManager;
            reservationService = _reservationService;
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
    }
}
