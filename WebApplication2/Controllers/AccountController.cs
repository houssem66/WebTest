﻿using Domaine.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Services.Implementation;
using Microsoft.AspNetCore.Hosting;
using Twilio.Exceptions;
using Microsoft.AspNetCore.Mvc.Rendering;
using TourMe.Web;
using Twilio.Rest.Lookups.V1;

using System.IO;
using TourMe.Data;
using TourMe.Web.Models;

namespace Finance.Controllers
{
   

    public class AccountController : Controller
    {   private readonly TourMeContext _context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<Utilisateur> userManager;
        private readonly SignInManager<Utilisateur> signInManager;
        readonly private IUserService UserService;
        private readonly IWebHostEnvironment hostingEnvironment;
        public List<SelectListItem> AvailableCountries { get; }
        public AccountController(UserManager<Utilisateur> userManager, SignInManager<Utilisateur> signInManager,  IWebHostEnvironment hostingEnvironment,IUserService _UserService, CountryService countryService,TourMeContext context,RoleManager<IdentityRole> roleManager)
        {
            UserService = _UserService;
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
            this.signInManager = signInManager;
            AvailableCountries = countryService.GetCountries();

            _context = context;
            this.roleManager = roleManager;
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");

        }
        [HttpGet]
        [Authorize(Roles = "Administrateur")]
        public async Task<IActionResult> GetAll()

        {
            IEnumerable<Utilisateur> utilisateurs = UserService.GetAllUtilisateurs();
            List<UtilisateurViewModel> users= new List<UtilisateurViewModel>();
            foreach (var user in utilisateurs) {

                var mod = new UtilisateurViewModel
                {
                    Id = user.Id,
                    Nom = user.Nom,
                    Prenom = user.Prenom,
                    Email = user.Email,

                };
                if (await userManager.IsInRoleAsync(user, "Administrateur"))
                { mod.role = "Administrateur"; }
                else
                {
                    if (await userManager.IsInRoleAsync(user, "Utilisateur"))
                    {

                        mod.role = "Utilisateur";

                    }
                    else
                    { mod.role = "Commercant"; }

               
            }
                users.Add(mod);
          
            }

            ViewBag.users = users;
            //ViewBag.UsersRoles = userManager.GetUsersForClaimAsync();
            return View();

           
        }

        
        
        public async Task<IActionResult> Profil()
        {
            ViewData["countries"] = AvailableCountries;
            string id = userManager.GetUserId(User);

            Utilisateur us = await UserService.GetById(id);
            if (us == null)
            { return RedirectToAction("index", "home"); }

            return View(us);
        }
        [HttpGet]
        public async Task<ViewResult> ChangerPhoto(string id)
        {
            Utilisateur utilisateur = await UserService.GetById(id);
            UtilisateurViewModel modelUser = new UtilisateurViewModel
            {
                Id = utilisateur.Id,
                Nom = utilisateur.Nom,
                Prenom = utilisateur.Prenom,
                ExistingPhotoPath = utilisateur.ProfilePhoto
            };

            return View(modelUser);
        }
        [HttpPost]
        public async Task<IActionResult> ChangerPhoto(UtilisateurViewModel modelUser)
        {
            Utilisateur utilisateur = await UserService.GetUtilisateurByIdAsync(modelUser.Id);





            // If a new photo is uploaded, the existing photo must be
            // deleted. So check if there is an existing photo and delete
            if (modelUser.ExistingPhotoPath != null)
            {
                string filePath = Path.Combine(hostingEnvironment.WebRootPath,
                    "images", modelUser.ExistingPhotoPath);
                System.IO.File.Delete(filePath);
            }
            // Save the new photo in wwwroot/images folder and update
            // PhotoPath property of the employee object which will be
            // eventually saved in the database
            utilisateur.ProfilePhoto = ProcessUploadedFile(modelUser);
            utilisateur.Nom = modelUser.Nom;


            // Call update method on the repository service passing it the
            // employee object to update the data in the database table
            await UserService.PutUtilisateurAsync(modelUser.Id, utilisateur);



            return View("Profil", utilisateur);
        }
        [Authorize]

        private string ProcessUploadedFile(UtilisateurViewModel modelUser)
        {
            string uniqueFileName = null;

            if (modelUser.Photo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + modelUser.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    modelUser.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }


        [HttpGet]
       
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilisateur = await UserService.GetById(id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            return View(utilisateur);
        }

        // POST: Utilisateurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await UserService.DeleteUtilisateurAsync(id);
            //    var utilisateur = await _context.User.FindAsync(id);
            //    _context.User.Remove(utilisateur);
            //    await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GetAll));
        }
        [HttpGet]
        
        public async Task<IActionResult> ProfilCommerc()
        {

            string id = userManager.GetUserId(User);

            Utilisateur us = await UserService.GetById(id);
            if (us == null)
            { return RedirectToAction("index", "home"); }

            return View(us);
        }


        [HttpGet]
        [AllowAnonymous]
       
        public async Task<IActionResult> RegisterUser(string returnUrl)
        {
            ViewData["countries"] = AvailableCountries;
            UtilisateurViewModel model = new UtilisateurViewModel { ReturnUrl = returnUrl, ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList() };
            return View(model);

        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterCommercant()
        {
            ViewData["countries"] = AvailableCountries;

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterCommercant(CommercentViewModel model)
        {
           
              ViewData["countries"] = AvailableCountries;
            if (ModelState.IsValid)
            {
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



                   

                    var user = new Commerçant
                    {
                        UserName = model.Email,
                   
                        PhoneNumber = numberToSave,
                        PersAContact=model.PersAContact,
                        Email = model.Email,
                        FormeJuridique = model.Forme,
                        Secteur = model.Secteur,
                        DomainActivite = model.Domaine,
                        SituationEntreprise = model.SituationEntreprise,
                        EffectFemme = model.EffectFemme,
                        EffectHomme = model.EffectHomme,
                        Type = model.Type,
                        Patente=uniqueFileName,
                        Country=model.PhoneNumberCountryCode
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
                        

                        return RedirectToAction("CreateExperience", "Experience");
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
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser(UtilisateurViewModel model, string returnUrl)
        {
            ViewData["countries"] = AvailableCountries;
            model.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            model.ReturnUrl = returnUrl;
            if (ModelState.IsValid)
            {
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



                    var user = new Utilisateur
                    {
                        BirthDate=model.BirthDate,
                        Country=model.PhoneNumberCountryCode,
                        UserName = model.Email,
                        Email = model.Email,
                        Nom = model.Nom,
                        Prenom = model.Prenom,
                        PhoneNumber = numberToSave



                    };
                    var result = await userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        if (await roleManager.RoleExistsAsync("Utilisateur"))
                        {
                            await userManager.AddToRoleAsync(user, "Utilisateur");
                        }
                        else
                        {
                            IdentityRole identityrole = new IdentityRole
                            {
                                Name = "Utilisateur"

                            };
                            await roleManager.CreateAsync(identityrole);
                            await userManager.AddToRoleAsync(user, "Utilisateur");

                        }
                        await signInManager.SignInAsync(user, isPersistent: false);
                       
                        return RedirectToAction("index", "home");
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
                        $"The number you entered was not valid (Twilio code {ex.Code}), please check it and try again");
                    return View();

                }
            }
            return View(model);
        }



        //sign In

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            LoginViewModel model = new LoginViewModel { ReturnUrl = returnUrl, ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList() };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            model.ReturnUrl = returnUrl;
            model.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {

                var result = await signInManager
                .PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);
                if (result.Succeeded)
                {

                    return RedirectToAction("index", "home");
                }


                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(model);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account",
                                    new { ReturnUrl = returnUrl });

            var properties =
                signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return new ChallengeResult(provider, properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult>
            ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            LoginViewModel loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");

                return View("Login", loginViewModel);
            }

            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external login information.");

                return View("Login", loginViewModel);
            }

            var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                                        info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                var identifier = info.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
                var photo = $"https://graph.facebook.com/{identifier}/picture";
                if (email != null)
                {
                    var user = await userManager.FindByEmailAsync(email);

                    if (user == null)
                    {
                        user = new Utilisateur
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Nom = info.Principal.FindFirstValue(ClaimTypes.Name),

                            ProfilePhoto = photo
                        };

                        await userManager.CreateAsync(user);
                    }

                    await userManager.AddLoginAsync(user, info);
                    if (await roleManager.RoleExistsAsync("Utilisateur"))
                    {
                        await userManager.AddToRoleAsync(user, "Utilisateur");
                    }
                    else
                    {
                        IdentityRole identityrole = new IdentityRole
                        {
                            Name = "Utilisateur"

                        };
                        await roleManager.CreateAsync(identityrole);
                        await userManager.AddToRoleAsync(user, "Utilisateur");

                    }
                    await signInManager.SignInAsync(user, isPersistent: false);

                    if (UserService.GetAllUtilisateurs().Count()==1)
                    {
                        System.Diagnostics.Debug.WriteLine("the count of :"+ UserService.GetAllUtilisateurs().Count());

                        IdentityRole identityrole2 = new IdentityRole
                        {
                            Name = "Administrateur"

                        };
                        await roleManager.CreateAsync(identityrole2);

                        await userManager.AddToRoleAsync(user, "Administrateur");

                    }
                   

                    return LocalRedirect(returnUrl);
                }

                ViewBag.ErrorTitle = $"Email claim not received from: {info.LoginProvider}";
                ViewBag.ErrorMessage = "Please contact support on Pragim@PragimTech.com";

                return View("Error");
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult test(string type)
        {



            if (type.Equals("Organisme"))
            {


                System.Diagnostics.Debug.WriteLine("Le type est" + type);
                return PartialView("_Organisme");
            }

            else if(type.Equals("Individu"))
            {

                return new EmptyResult();
            }
            return null;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> EditUser(Commerçant model)
        {
            
            string id = userManager.GetUserId(User);
            Commerçant user = (Commerçant)await UserService.GetUtilisateurByIdAsync(id);
         
            user.PersAContact = model.PersAContact;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            
           
            await UserService.PutUtilisateurAsync(id, user);
          
                return RedirectToAction("ProfilCommerc");



        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UtilisateurViewModel model)
        {

            string id = userManager.GetUserId(User);
            Utilisateur user = await UserService.GetUtilisateurByIdAsync(id);

            user.Nom = model.Nom;
            user.Email = model.Email;
            user.Carte = model.carte;
            user.Prenom = model.Prenom;
            user.BirthDate = model.BirthDate;
            user.NickName = model.NickName;
            user.Country = model.Country;
            user.PhoneNumber = model.Telephone;



            await UserService.PutUtilisateurAsync(id, user);

            return RedirectToAction("Profil");



        }
        [HttpGet]
       
        public async Task<IActionResult> Details(string id)
        {
            var user = await UserService.GetById(id);
            var User = await userManager.FindByIdAsync(id);
            var existingUserClaims = await userManager.GetClaimsAsync(User);
            ViewBag.Claims = existingUserClaims;
            
            return View(user);

        }
        [HttpGet]
        public async Task<IActionResult> ManageUserClaims(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }

            // UserManager service GetClaimsAsync method gets all the current claims of the user
            var existingUserClaims = await userManager.GetClaimsAsync(user);

            var model = new UserClaimsViewModel
            {
                id = id
            };

            // Loop through each claim we have in our application
            foreach (Claim claim in ClaimsStore.AllClaims)
            {
                UserClaim userClaim = new UserClaim
                {
                    ClaimType = claim.Type
                };

                // If the user has the claim, set IsSelected property to true, so the checkbox
                // next to the claim is checked on the UI
                if (existingUserClaims.Any(c => c.Type == claim.Type))
                {
                    userClaim.IsSelected = true;
                }

                model.Cliams.Add(userClaim);
            }

            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> ManageUserClaims(UserClaimsViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.id} cannot be found";
                return View("NotFound");
            }

            // Get all the user existing claims and delete them
            var claims = await userManager.GetClaimsAsync(user);
            var result = await userManager.RemoveClaimsAsync(user, claims);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing claims");
                return View(model);
            }

            // Add all the claims that are selected on the UI
            result = await userManager.AddClaimsAsync(user,
                model.Cliams.Where(c => c.IsSelected).Select(c => new Claim(c.ClaimType, c.ClaimType)));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected claims to user");
                return View(model);
            }

            return RedirectToAction("Details", new { Id = model.id });

        }
    }
}
