using Domaine.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication2.Models;
using Services.Implementation;
using Microsoft.AspNetCore.Hosting;
using Twilio.Exceptions;
using Microsoft.AspNetCore.Mvc.Rendering;
using TourMe.Web;
using Twilio.Rest.Lookups.V1;
using System.IO;

namespace Finance.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Utilisateur> userManager;
        private readonly SignInManager<Utilisateur> signInManager;
        readonly private IUserService UserService;
        private readonly IWebHostEnvironment hostingEnvironment;

        public List<SelectListItem> AvailableCountries { get; }

        
        public AccountController(UserManager<Utilisateur> userManager,SignInManager<Utilisateur> signInManager, IUserService _UserService, CountryService countryService, IWebHostEnvironment hostingEnvironment)
        {
            UserService = _UserService;
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
            this.signInManager = signInManager;
            AvailableCountries = countryService.GetCountries();

        }


        [HttpPost]
        public ActionResult ModalPopUp()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");

        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return View(UserService.GetAllUtilisateurs());
           
            
        }

        [HttpGet]
        public async  Task<ViewResult> ChangerPhoto(string id)
        {
            Utilisateur utilisateur = await UserService.GetById(id);
            UtilisateurViewModel modelUser = new UtilisateurViewModel
            {  Id=utilisateur.Id,
               Nom= utilisateur.Nom,
               Prenom= utilisateur.Prenom,
               ExistingPhotoPath =utilisateur.ProfilePhoto
            };

            return View(modelUser);
        }

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
          await  UserService.DeleteUtilisateurAsync(id);
        //    var utilisateur = await _context.User.FindAsync(id);
        //    _context.User.Remove(utilisateur);
        //    await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GetAll));
        }
        [HttpGet]
       
        public async Task<IActionResult> Profil()
        {
            string id =  userManager.GetUserId(User);
           
           Utilisateur  us = await UserService.GetById(id);
            if (us == null)
            { return RedirectToAction("index", "home"); }

            return View(us);
        }


        [HttpGet]

        public async  Task<IActionResult> RegisterUser(string returnUrl)
        {
            ViewData["countries"] = AvailableCountries;
            UtilisateurViewModel model = new UtilisateurViewModel {
                ReturnUrl = returnUrl, ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList() };
            return View(model);
        }
        [HttpGet]
        public IActionResult RegisterCommercant()
        {
            ViewData["countries"] = AvailableCountries;

            return View();
        }
        [HttpPost]
       
        public async Task<IActionResult> RegisterCommercant(CommercentViewModel model)
        {
            ViewData["countries"] = AvailableCountries;
            if (ModelState.IsValid)
            {

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
                        Nom = model.Nom,
                        Prenom = model.PreNom,
                        PhoneNumber = numberToSave,
                        CIN = model.CIN,
                        Email = model.Email,
                        FormeJuridique = model.Forme,
                        Secteur = model.Secteur,
                        DomainActivite = model.Domaine,
                        SituationEntreprise = model.SituationEntreprise,
                        EffectFemme = model.EffectFemme,
                        EffectHomme = model.EffectHomme
                    };
                    var result = await userManager.CreateAsync(user, model.Password);


                    if (result.Succeeded)
                    {
                        
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(UtilisateurViewModel model,string returnUrl)
        {
            ViewData["countries"] = AvailableCountries;
            model.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            model.ReturnUrl = returnUrl;
            if (ModelState.IsValid) {
                try {
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

                  
                 
                       var user = new Utilisateur {
                          
                    UserName = model.Email,
                    Email = model.Email,
                    Nom = model.Nom,
                    Prenom = model.Prenom,
                    PhoneNumber = numberToSave



                       };
             var result =  await userManager.CreateAsync(user, model.Password);
                if(result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }
                foreach(var error in result.Errors)
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
            LoginViewModel model = new LoginViewModel

            { ReturnUrl= returnUrl ,
            
              ExternalLogins= (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            
            
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model,string returUrl)
        {
             
             model.ExternalLogins= (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            model.ReturnUrl = returUrl;

             
            if (ModelState.IsValid)
            {

                var result = await signInManager
                .PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    
                    return RedirectToAction("index", "home");
                }
             
                
                    ModelState.AddModelError(string.Empty,"Invalid Login Attempt");
                
            }
            return View(model);
        }
        
        
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

    UtilisateurViewModel utilisateurViewModel = new UtilisateurViewModel
    {
        ReturnUrl = returnUrl,
        ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
    };

    if (remoteError != null)
    {
        ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");

        return View("Login", utilisateurViewModel);
    }

    var info = await signInManager.GetExternalLoginInfoAsync();
    if (info == null)
    {
        ModelState.AddModelError(string.Empty, "Error loading external login information.");

        return View("Login", utilisateurViewModel);
    }

    var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                                info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

    if (signInResult.Succeeded)
    {
        return LocalRedirect(returnUrl);
    }
    else
    {
                //string uniqueFileName = null;
                //if (model.Photo != null)
                //{
                //    string uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                //    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                //    string FilePath = Path.Combine(uploadFolder, uniqueFileName);
                //    model.Photo.CopyTo(new FileStream(FilePath, FileMode.Create));


                    var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                var identifier = info.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
                var photo = $"https://graph.facebook.com/{identifier}/?fields=picture&type=large&access_token=EAAMmA4PXqUcBAJK4iZBGt3vjeGVLykTRgT54wbubJuNLC948ZAZCH03DrxMzyYNiMO5etScP3KKYd6iK5HqIcGuiCdp2ED0t5QwxeGwoZBbcOeZBdLVZCM6HuRc9Lo4uMf3IZBHXghkM2LGd72geEQW1WVXeZCJPUgkCtSSek6TszQZDZD";
                 
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
            await signInManager.SignInAsync(user, isPersistent: false);

            return LocalRedirect(returnUrl);
        }

        ViewBag.ErrorTitle = $"Email claim not received from: {info.LoginProvider}";
        ViewBag.ErrorMessage = "Please contact support on Pragim@PragimTech.com";

        return View("Error");
    }
}


    }
}
