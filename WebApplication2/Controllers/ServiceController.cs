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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using TourMe.Data;
using TourMe.Data.Entities;
using TourMe.Data.Entities.Enum;
using TourMe.Web.Models;
using Twilio.Exceptions;
using Twilio.Rest.Lookups.V1;

namespace TourMe.Web.Controllers
{
    public class ServiceController : Controller

    {
        public List<SelectListItem> AvailableCountries { get; }
        private readonly UserManager<Utilisateur> userManager;
        private readonly ICommercantService commercantService;
        private readonly IUserService userService;
        private readonly IFournisseurService fournisseurService;
        private readonly SignInManager<Utilisateur> signInManager;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogementextService logementService;
        private readonly INourritureExtService nourritureService;
        private readonly ITransportExtService transportExtService;

        public ServiceController(UserManager<Utilisateur> userManager, ICommercantService _CommercantService, IUserService _UserService, IFournisseurService _FournisseurService, CountryService countryService, SignInManager<Utilisateur> signInManager, IWebHostEnvironment hostingEnvironment, TourMeContext context, RoleManager<IdentityRole> roleManager, ILogementextService _logementService, INourritureExtService _NourritureService, ITransportExtService _TransportExtService)
        {
            this.userManager = userManager;
            commercantService = _CommercantService;
            userService = _UserService;
            fournisseurService = _FournisseurService;
            this.signInManager = signInManager;
            this.hostingEnvironment = hostingEnvironment;
            this.roleManager = roleManager;
            logementService = _logementService;
            nourritureService = _NourritureService;
            transportExtService = _TransportExtService;
            AvailableCountries = countryService.GetCountries();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AjouterLogement()
        {
            string idx = userManager.GetUserId(User);


            return View();
        }
        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> AjouterLogement(LogementExtViewModel model)
        //{
        //    ViewBag.id = userManager.GetUserId(User);
        //    string uniqueFileName = null;
        //    if (ModelState.IsValid)
        //    {
        //        if (model.Documents != null)
        //        {
        //            // The image must be uploaded to the images folder in wwwroot
        //            // To get the path of the wwwroot folder we are using the inject
        //            // HostingEnvironment service provided by ASP.NET Core
        //            string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Files");
        //            // To make sure the file name is unique we are appending a new
        //            // GUID value and and an underscore to the file name
        //            uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Documents.FileName;
        //            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //            // Use CopyTo() method provided by IFormFile interface to
        //            // copy the file to wwwroot/images folder
        //            model.Documents.CopyTo(new FileStream(filePath, FileMode.Create));
        //        }
        //        var logment = new ServiceLogment
        //        {
        //            Adresse = model.Adresse,

        //            PrixParNuit = model.PrixParNuit,
        //            Titre = model.Titre,

        //            Documents = uniqueFileName,
        //            Category = model.Category,
        //            Fournisseur = fournisseurService.GetFournisseurById(userManager.GetUserId(User)).Result
        //        };
        //        var x = logment;
        //        await logementService.Ajout(logment);
        //        return RedirectToAction("Index", "Home");
        //    }
        //    return View(model);
        //}
        [HttpGet]
        [AllowAnonymous]
        public IActionResult TypeService()
        {
            string idx = userManager.GetUserId(User);
            //if (idx == null)
            //{

            //    return View("New");
            //}

            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ChangerEnFournisseur(CommercentViewModel model)
        {

            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult CommercantPersonne(CommercentViewModel model)
        {

            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CommercantPersonne(CommercentViewModel model, string Password, string email)
        {

            string idx = userManager.GetUserId(User);
            var com = commercantService.GetCommerçantById(idx).Result;
            Commerçant com2 = (Commerçant)userService.GetById(idx).Result;
            var fournisseur = new Fournisseur
            {
                UserName = com.Email,

                PhoneNumber = com.PhoneNumber,
                PersAContact = com.PersAContact,
                Email = com.Email,
                FormeJuridique = model.Forme,
                Secteur = model.Secteur,
                DomainActivite = model.Domaine,
                SituationEntreprise = model.SituationEntreprise,
                EffectFemme = model.EffectFemme,
                EffectHomme = model.EffectHomme,
                Type = com.Type,
                TypeService = (TypeService)Enum.Parse(typeof(TypeService), model.TypseService)

            };
            await commercantService.Delete(com);
            var result = await userManager.CreateAsync(fournisseur, Password);
            if (result.Succeeded)
            {

                Debug.WriteLine("cest un user");
                await userManager.AddToRoleAsync(fournisseur, "Commercant");
                await signInManager.SignInAsync(fournisseur, isPersistent: false);

            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ChangerEnFournisseur(string passowrd, string email, CommercentViewModel model)
        {
            string idx = userManager.GetUserId(User);
            var com = commercantService.GetCommerçantById(idx).Result;
            var fournisseur = new Fournisseur
            {
                UserName = com.Email,

                PhoneNumber = com.PhoneNumber,
                PersAContact = com.PersAContact,
                Email = com.Email,
                FormeJuridique = com.FormeJuridique,
                Secteur = com.Secteur,
                DomainActivite = com.DomainActivite,
                SituationEntreprise = com.SituationEntreprise,
                EffectFemme = com.EffectFemme,
                EffectHomme = com.EffectHomme,
                Type = com.Type,

                TypeService = (TypeService)Enum.Parse(typeof(TypeService), model.TypseService)

            };
            await commercantService.Delete(com);
            var result = await userManager.CreateAsync(fournisseur, passowrd);
            if (result.Succeeded)
            {

                Debug.WriteLine("cest un user");
                await userManager.AddToRoleAsync(fournisseur, "Commercant");
                await signInManager.SignInAsync(fournisseur, isPersistent: false);

            }
            return RedirectToAction("Index", "Home");
        }
        //[HttpGet]
        //[AllowAnonymous]
        //public IActionResult DevnirFournisseurAlimentaire(string id)
        //{
        //    ViewData["countries"] = AvailableCountries;

        //    string idx = userManager.GetUserId(User);
        //    if (idx == null)
        //    {
        //        var user = new CommercentViewModel { TypseService = id };
        //        return View("NewFournisseurAlimentaire", user);
        //    }
        //    var userCo = fournisseurService.GetFournisseurById(idx);
        //    if (idx != null && (userCo.Result != null))
        //    {

        //        return RedirectToAction("AjouterLogement", "Service");

        //    }
        //    else
        //    {
        //        var com = commercantService.GetCommerçantById(idx).Result;
        //        var user = new CommercentViewModel { TypseService = id };
        //        if (com.SituationEntreprise != null)
        //        {


        //            return RedirectToAction("ChangerEnFournisseur", "Service", user);
        //        }



        //        return RedirectToAction("CommercantPersonne", "service", user);
        //    }


        //}
        [HttpGet]
        [AllowAnonymous]
        public IActionResult DevnirFournisseurLogement(string id)
        {
            ViewData["countries"] = AvailableCountries;

            string idx = userManager.GetUserId(User);
            if (idx == null)
            {
                var user = new CommercentViewModel { TypseService = id };
                return View("New", user);
            }
            var userCo = fournisseurService.GetFournisseurById(idx);
            if (idx != null && (userCo.Result != null))
            {

                return RedirectToAction("AjouterLogement", "Service");

            }
            else
            {
                var com = commercantService.GetCommerçantById(idx).Result;
                var user = new CommercentViewModel { TypseService = id };
                if (com.SituationEntreprise != null)
                {


                    return RedirectToAction("ChangerEnFournisseur", "Service", user);
                }



                return RedirectToAction("CommercantPersonne", "service", user);
            }


        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> DevnirFournisseurLogement(CommercentViewModel model)
        {
            ViewData["countries"] = AvailableCountries;
            if (ModelState.IsValid)
            {
                Debug.WriteLine("valid" + ModelState.IsValid.ToString());
                string uniqueFileName = null;
                if (model.FileP != null)
                {
                    // The image must be uploaded to the images folder in wwwroot
                    // To get the path of the wwwroot folder we are using the inject
                    // HostingEnvironment service provided by ASP.NET Core
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Files");
                    // To make sure the file name is unique we are appending a new
                    // GUID value and and an underscore to the file name
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.FileP.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    // Use CopyTo() method provided by IFormFile interface to
                    // copy the file to wwwroot/images folder
                    model.FileP.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                //phone
                try
                {
                    var numberDetails = await PhoneNumberResource.FetchAsync(
                        pathPhoneNumber: new Twilio.Types.PhoneNumber(model.Telephone),
                        countryCode: model.PhoneNumberCountryCode,

                        type: new List<string> { "carrier" });
                    // only allow user to set phone number if capable of receiving SMS
                    if (numberDetails?.Carrier != null && numberDetails.Carrier.GetType().Equals(""))
                    {
                        ModelState.AddModelError($"{nameof(model.Telephone)}.{nameof(model.Telephone)}",
                            $"Le format du numero ne convient pas à votre pays");
                        return View();
                    }

                    var numberToSave = numberDetails.PhoneNumber.ToString();

                    var user = new Fournisseur
                    {
                        UserName = model.Email,

                        PhoneNumber = numberToSave,
                        PersAContact = model.PersAContact,
                        Email = model.Email,
                        FormeJuridique = model.Forme,
                        Secteur = model.Secteur,
                        DomainActivite = model.Domaine,
                        SituationEntreprise = model.SituationEntreprise,
                        EffectFemme = model.EffectFemme,
                        EffectHomme = model.EffectHomme,
                        Type = model.Type,
                        Patente = uniqueFileName,
                        TypeService = (TypeService)Enum.Parse(typeof(TypeService), model.TypseService)

                    };
                    var result = await userManager.CreateAsync(user, model.Password);


                    if (result.Succeeded)
                    {
                        System.Diagnostics.Debug.WriteLine("Country is" + model.PhoneNumberCountryCode);


                        if (await roleManager.RoleExistsAsync("Commercant"))
                        {
                            await userManager.AddToRoleAsync(user, "Commercant");
                        }
                        else
                        {
                            IdentityRole identityrole = new IdentityRole
                            {
                                Name = "Commercant"

                            };
                            await roleManager.CreateAsync(identityrole);
                            await userManager.AddToRoleAsync(user, "Commercant");

                        }
                        await signInManager.SignInAsync(user, isPersistent: false);

                        if (user.TypeService.ToString() == "Logement")
                        {

                            return RedirectToAction("AjouterLogement", "Service");
                        }
                        if (user.TypeService.ToString() == "Nourriture")
                        {
                            return RedirectToAction("AjouterNourriture", "Service");
                        }

                        if (user.TypeService.ToString() == "Transport")
                        {



                        }

                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(model);


                }
                catch (ApiException ex)
                {
                    ModelState.AddModelError($"{nameof(model.Telephone)}.{nameof(model.Telephone)}",
                        $"Le numéro entré n'est pas valide  (Code d'erreur {ex.Code})");
                    return View();
                }

            }
            Debug.WriteLine("ma5dmtch" + ModelState.IsValid.ToString());
            return View("New");
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Annuler()
        {
            string idx = userManager.GetUserId(User);
            var user = fournisseurService.GetFournisseurById(idx).Result;


            if (fournisseurService.Delete(user).Result == "success")
            {
                await signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
            else
            {

                return RedirectToAction("AjouterLogement", "Service");

            }




        }

        //[HttpGet]
        //[AllowAnonymous]
        //public IActionResult AjouterNourriture()
        //{
        //    string idx = userManager.GetUserId(User);


        //    return View();
        //}
        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> AjouterNourriture(NourritureExtViewModel model)
        //{
        //    ViewBag.id = userManager.GetUserId(User);
        //    string uniqueFileName = null;
        //    if (ModelState.IsValid)
        //    {
        //        if (model.Image != null)
        //        {
        //            // The image must be uploaded to the images folder in wwwroot
        //            // To get the path of the wwwroot folder we are using the inject
        //            // HostingEnvironment service provided by ASP.NET Core
        //            string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Files");
        //            // To make sure the file name is unique we are appending a new
        //            // GUID value and and an underscore to the file name
        //            uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
        //            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //            // Use CopyTo() method provided by IFormFile interface to
        //            // copy the file to wwwroot/images folder
        //            model.Image.CopyTo(new FileStream(filePath, FileMode.Create));
        //        }
        //        var nouritture = new ServiceNouritture
        //        {
        //            Description = model.Description,

        //            Plat = model.Plat,
        //            Prix = model.Prix,

        //            Image = uniqueFileName,
        //            Type = model.Type,
        //            Fournisseur = fournisseurService.GetFournisseurById(userManager.GetUserId(User)).Result
        //        };

        //        await nourritureService.Ajout(nouritture);
        //        return RedirectToAction("Index", "Home");
        //    }
        //    return View(model);
        //}
        [HttpGet]
        [AllowAnonymous]
        public IActionResult BecomeCommercant()
        {
            ViewData["countries"] = AvailableCountries;
            string idx = userManager.GetUserId(User);


            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> BecomeCommercant(CommercentViewModel model, string jobb)
        {
            ViewData["countries"] = AvailableCountries;
            string idx = userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                Debug.WriteLine("valid" + ModelState.IsValid.ToString());
                string uniqueFileName = null;
                List<EmployeDocuments> emp = new List<EmployeDocuments>();
                if (model.Documents != null && model.Documents.Count > 0)
                {

                    // Loop thru each selected file
                    foreach (IFormFile photo in model.Documents)
                    {
                        EmployeDocuments employe = new EmployeDocuments();
                        // The file must be uploaded to the images folder in wwwroot
                        // To get the path of the wwwroot folder we are using the injected
                        // IHostingEnvironment service provided by ASP.NET Core
                        string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Files");
                        // To make sure the file name is unique we are appending a new
                        // GUID value and and an underscore to the file name
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        // Use CopyTo() method provided by IFormFile interface to
                        // copy the file to wwwroot/images folder
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                        employe.Filepath = uniqueFileName;
                        emp.Add(employe);
                    }
                }
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
                //phone
                try
                {
                    var numberDetails = await PhoneNumberResource.FetchAsync(
                        pathPhoneNumber: new Twilio.Types.PhoneNumber(model.Telephone),
                        countryCode: model.PhoneNumberCountryCode,

                        type: new List<string> { "carrier" });
                    // only allow user to set phone number if capable of receiving SMS
                    if (numberDetails?.Carrier != null && numberDetails.Carrier.GetType().Equals(""))
                    {
                        ModelState.AddModelError($"{nameof(model.Telephone)}.{nameof(model.Telephone)}",
                            $"Le format du numero ne convient pas à votre pays");
                        return View();
                    }
                  
                    var numberToSave = numberDetails.PhoneNumber.ToString();
                    
                    var user = new Fournisseur
                    {
                        UserName = model.Email,

                        PhoneNumber = numberToSave,
                        PersAContact = model.PersAContact,
                        Email = model.Email,
                        Secteur = model.Secteur,
                        NomGerant = model.NomGerant,
                        DomainActivite = model.Domaine,
                        Identifiant_fiscale = model.Identifiant_fiscale,
                        Titre = model.Titre,
                        EffectFemme = model.EffectFemme,
                        EffectHomme = model.EffectHomme,
                        //Type = model.Type,
                        ProfilePhoto = uniqueFileName,
                        TypeService = (TypeService)Enum.Parse(typeof(TypeService), jobb),
                        Adresse = model.Adresse,
                        EmployeDocuments = emp

                    };
                    var result = await userManager.CreateAsync(user, model.Password);


                    if (result.Succeeded)
                    {



                        if (await roleManager.RoleExistsAsync("Commercant"))
                        {
                            await userManager.AddToRoleAsync(user, "Commercant");
                        }
                        else
                        {
                            IdentityRole identityrole = new IdentityRole
                            {
                                Name = "Commercant"

                            };
                            await roleManager.CreateAsync(identityrole);
                            await userManager.AddToRoleAsync(user, "Commercant");

                        }
                        await signInManager.SignInAsync(user, isPersistent: false);


                    }
                  
                    if(user.TypeService.ToString().ToLower()=="transport")
                    {
                        return RedirectToAction("AjouterTransport", "Service");

                    }
                  else  if(user.TypeService.ToString().ToLower()=="logement")
                    {
                        return RedirectToAction("AddLogement", "Service");

                    } 
                    else  if(user.TypeService.ToString().ToLower()=="nourriture")
                    {
                        return RedirectToAction("AddRestaurant", "Service");

                    }

                    


                }
                catch (ApiException ex)
                {
                    ModelState.AddModelError($"{nameof(model.Telephone)}.{nameof(model.Telephone)}",
                        $"Le numéro entré n'est pas valide  (Code d'erreur {ex.Code})");
                    return View();
                }

            }

            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult RedirectTO()
        {



            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AjouterTransport()
        {
            string idx = userManager.GetUserId(User);


            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AjouterTransport(TransportExtViewModel model)
        {
            string idx = userManager.GetUserId(User);
            ViewBag.id = userManager.GetUserId(User);
            string uniqueFileName = null;
            if (ModelState.IsValid)
            {
                if (model.Images != null)
                {
                    // The image must be uploaded to the images folder in wwwroot
                    // To get the path of the wwwroot folder we are using the inject
                    // HostingEnvironment service provided by ASP.NET Core
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Files");
                    // To make sure the file name is unique we are appending a new
                    // GUID value and and an underscore to the file name
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Images.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    // Use CopyTo() method provided by IFormFile interface to
                    // copy the file to wwwroot/images folder
                    model.Images.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                var transport = new ServiceTransport
                {
                    NbrPlaces = model.NbrPlaces,

                    Load = model.Load,
                    Region = model.Region,
                    
                    Image = uniqueFileName,
                    ReservationPrive = model.TypeTransport,
                    TypeTransport=model.TypeTransport,
                    Fournisseur = fournisseurService.GetFournisseurById(userManager.GetUserId(User)).Result
                };

                await transportExtService.Ajout(transport);
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AddLogement()
        {
            string idx = userManager.GetUserId(User);


            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddLogement(LogementExtViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;


                List<LNDocuments> emp = new List<LNDocuments>();
                if (model.FileP != null && model.FileP.Count > 0)
                {
                    // Loop thru each selected file
                    foreach (IFormFile photo in model.FileP)
                    {
                        LNDocuments employe = new LNDocuments();
                        // The file must be uploaded to the images folder in wwwroot
                        // To get the path of the wwwroot folder we are using the injected
                        // IHostingEnvironment service provided by ASP.NET Core
                        string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Files");
                        // To make sure the file name is unique we are appending a new
                        // GUID value and and an underscore to the file name
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        // Use CopyTo() method provided by IFormFile interface to
                        // copy the file to wwwroot/images folder
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                        employe.Filepath = uniqueFileName;
                        emp.Add(employe);
                    }
                }



                Fournisseur f = (Fournisseur)userManager.GetUserAsync(User).Result;
                ServiceLogment serviceLogment = new ServiceLogment
                {

                    Adresse = model.Adresse,
                    Description = model.Description,
                    PrixParNuit = model.PrixParNuit,
                    Titre = model.Titre,
                    Category = model.Category
                    ,

                    Type = model.Type,
                    Fournisseur = f,
                    Documents = emp



                };





                await logementService.Ajout(serviceLogment);

                TempData["id"] = JsonConvert.SerializeObject(serviceLogment.Id);

                return RedirectToAction("DetailsLogement", "Service");
            }


            return View(model);

        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult AddRestaurant()
        {
            string idx = userManager.GetUserId(User);


            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddRestaurant(NourritureExtViewModel model)
        {


            //ViewData["Message"] = JsonConvert.DeserializeObject<List<Activite>>((string)TempData.Peek("Message"));
            //TempData.Keep("Message");
            //Activites = (ICollection<Activite>)ViewData["Message"];
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                string uniqueFileName2 = null;

                List<LNDocuments> emp = new List<LNDocuments>();
                if (model.FileP != null && model.FileP.Count > 0)
                {
                    // Loop thru each selected file
                    foreach (IFormFile photo in model.FileP)
                    {
                        LNDocuments employe = new LNDocuments();
                        // The file must be uploaded to the images folder in wwwroot
                        // To get the path of the wwwroot folder we are using the injected
                        // IHostingEnvironment service provided by ASP.NET Core
                        string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Files");
                        // To make sure the file name is unique we are appending a new
                        // GUID value and and an underscore to the file name
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        // Use CopyTo() method provided by IFormFile interface to
                        // copy the file to wwwroot/images folder
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                        employe.Filepath = uniqueFileName;
                        emp.Add(employe);
                    }
                }


                if (model.FilePp != null)
                {

                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Files");

                    uniqueFileName2 = Guid.NewGuid().ToString() + "_" + model.FilePp.FileName;
                    string filePath1 = Path.Combine(uploadsFolder, uniqueFileName2);

                    model.FilePp.CopyTo(new FileStream(filePath1, FileMode.Create));

                }


                string id = userManager.GetUserId(User);
                ServiceNouritture nourriture = new ServiceNouritture
                {
                    FournisseurId = id,
                    TypeResto = model.TypeResto,
                    SpecialeResto = model.SpecialeResto,
                    NomRestau = model.NomRestau,

                    Description = model.Description,
                    Plat = model.Plat,

                    Prix = model.Prix,
                    Menu = uniqueFileName2,
                    regles = model.regles,
                    Adresse = model.Adresse,
                    Slogon = model.Slogon
                    ,
                    Site = model.Site,
                    Rating = model.Rating,
                    Documents = emp,





                };





                await nourritureService.Ajout(nourriture);

                TempData["id"] = JsonConvert.SerializeObject(nourriture.Id);

                return RedirectToAction("DetailsResto", "Service");
            }


            return View(model);

        }


        [HttpGet]
        [AllowAnonymous]

        public IActionResult MesServices()

        {
            ViewBag.user = userManager.GetUserAsync(User).Result;
            // ViewBag.Best = ExperienceService.BestExperience();
            var list = nourritureService.GetNourritureByUser(userManager.GetUserId(User));
            return View(list);

        }


        [HttpGet]
        [AllowAnonymous]

        public IActionResult DetailsLogement(int id)

        {
            if (id == 0)
            {

                ViewData["id"] = JsonConvert.DeserializeObject<int>((string)TempData["id"]);
                TempData.Keep("id");

                id = (int)ViewData["id"];

            }


            ViewBag.user = userManager.GetUserAsync(User).Result;
            // ViewBag.Best = ExperienceService.BestExperience();

            ServiceLogment logment = logementService.GetById(id).Result;
            return View(logment);

        }

        [HttpGet]
        [AllowAnonymous]

        public IActionResult DetailsResto(int id)

        {
            if (id == 0)
            {

                ViewData["id"] = JsonConvert.DeserializeObject<int>((string)TempData["id"]);
                TempData.Keep("id");

                id = (int)ViewData["id"];

            }


            ViewBag.user = userManager.GetUserAsync(User).Result;
            // ViewBag.Best = ExperienceService.BestExperience();

            ServiceNouritture nouritture = nourritureService.GetById(id).Result;
            return View(nouritture);

        }
        [HttpGet]
        [AllowAnonymous]

        public IActionResult GetAllNourriture()

        {



            List<ServiceNouritture> nourittures = new List<ServiceNouritture>();
            nourittures = (List<ServiceNouritture>)nourritureService.GetAllLogements();
            return View(nourittures);

        }
        [HttpGet]
        [AllowAnonymous]

        public IActionResult GetAllLogements()

        {



            List<ServiceLogment> nourittures = new List<ServiceLogment>();
            nourittures = (List<ServiceLogment>)logementService.GetAllLogements();
            return View(nourittures);

        }





    }
}
