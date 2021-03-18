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
using System.Diagnostics;

namespace Finance.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Utilisateur> userManager;
        private readonly SignInManager<Utilisateur> signInManager;
        readonly private IUserService UserService;
        public AccountController(UserManager<Utilisateur> userManager,SignInManager<Utilisateur> signInManager, IUserService _UserService)
        {
            UserService = _UserService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        [HttpPost]
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
        [AllowAnonymous]

        public async  Task<IActionResult> RegisterUser(string returnUrl)
        {
            UtilisateurViewModel model = new UtilisateurViewModel { ReturnUrl = returnUrl, ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList() };
            return View(model);
        }
        [HttpGet]
        public IActionResult RegisterCommercant()
        {
            return View();
        }
        [HttpPost]
       
        public async Task<IActionResult> RegisterCommercant(CommercentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Commerçant { UserName = model.Email, Nom = model.Nom, Prenom = model.PreNom, PhoneNumber = model.Telephone, CIN = model.CIN, Email = model.Email };
                
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
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(UtilisateurViewModel model)
        {
            if (ModelState.IsValid) {
                var user = new Utilisateur {
                    UserName = model.Email,
                    Email = model.Email,
                    Nom = model.Nom,
                    Prenom = model.Prenom
                
                
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
            }
            return View(model);
        }


        //sign In

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            LoginViewModel model = new LoginViewModel { ReturnUrl= returnUrl ,ExternalLogins= (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()};
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
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
