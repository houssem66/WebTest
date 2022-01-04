using Domaine.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TourMe.Data.Entities;
using TourMe.Web.Models;

namespace TourMe.Web.Controllers
{
    public class HebergementController : Controller
    {
        private readonly UserManager<Utilisateur> userManager;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IHebergementService hebergementService;
        private readonly ICommercantService commercantService;

        public HebergementController(UserManager<Utilisateur> userManager, IWebHostEnvironment hostingEnvironment,IHebergementService _HebergementService,ICommercantService _commercantService)
        {
            this.userManager = userManager;
            this.hostingEnvironment = hostingEnvironment;
            hebergementService = _HebergementService;
            commercantService = _commercantService;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AddHebergement()
        {
            string idx = userManager.GetUserId(User);


            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddHebergement(HebergementViewModel model)
        {
            string idx = userManager.GetUserId(User);
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
                Hebergement logement = new Hebergement
                {
                   
                    Lieu = model.Lieu,
                   Titre=model.Titre,
                  
                    SubCategory = model.SubCategory,
                    Type = model.Type,
                    Prix = model.Prix,
                    Commercant = commercantService.GetCommerçantById(idx).Result,
                    Image = uniqueFileName,
                };
                await hebergementService.Ajout(logement);
            }


            return View(model);

        }
        //[HttpGet]
        //[AllowAnonymous]

        //public IActionResult GetAll(string[] searchTerm)

        //{

        //    string[] search = new string[10];
        //    List<Hebergement> list = new List<Hebergement>();
        //    if (!(searchTerm.Count() == 0))
        //    {
        //        foreach (var ch in searchTerm)
        //        {
        //            try
        //            {
        //                var list2 = ExperienceService.GetAllExperienceDetails(ch).ToList();

        //                list = list.Concat(list2).ToList();
        //            }
        //            catch (Exception e)
        //            {
        //                var list2 = ExperienceService.GetAllExperienceDetails(null).ToList();
        //                list = list.Concat(list2).ToList();

        //            }



        //        }
        //        ;

        //        return View(list);
        //    }
        //    return View(ExperienceService.GetAllExperienceDetails("").ToList());

        //}
        public IActionResult MesHebergements()

        {
            ViewBag.user = userManager.GetUserAsync(User).Result;       
            var list = commercantService.GetCommerçantById(userManager.GetUserId(User));
            return View(list.Result.Hebergements);

        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteHebergement(int id)
        {
            var hebergement = await hebergementService.GetHebergementById(id);
            try { await hebergementService.DeleteHebergementAsnc(id); }

            catch (Exception e)
            { RedirectToAction("MesHebergements", "Hebergements"); }


            return RedirectToAction("MesHebergements", "Hebergements");

        }
        public async Task<IActionResult> ModifierHebergement(int id)
        {
            var hebergement = await hebergementService.GetHebergementById(id);
           

            ViewBag.Id = id;
            return View(hebergement);
        }
    }

}
