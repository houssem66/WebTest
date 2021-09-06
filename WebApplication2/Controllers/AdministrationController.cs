using Domaine.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Implementation;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourMe.Data.Entities;
using TourMe.Web.Models;

namespace TourMe.Web.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<Utilisateur> UserManager;
        private readonly IUserService userService;
        private readonly ICommercantService commercantService;
        private readonly IFournisseurService fournisseurService;
        private readonly IExperienceService experienceService;
        private readonly ITransportExtService transportExtService;
        private readonly ILogementextService logementextService;
        private readonly INourritureExtService nourritureExtService;

        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<Utilisateur> userManager, IUserService _UserService, ICommercantService _CommercantService, IFournisseurService _FournisseurService, IExperienceService _experienceService, ITransportExtService _transportExtService, ILogementextService _logementextService, INourritureExtService _nourritureExtService)
        {
            this.roleManager = roleManager;
            UserManager = userManager;
            userService = _UserService;
            commercantService = _CommercantService;
            fournisseurService = _FournisseurService;
            experienceService = _experienceService;
            transportExtService = _transportExtService;
            logementextService = _logementextService;
            nourritureExtService = _nourritureExtService;
        }
        [HttpGet]
        public IActionResult GetAllRoles()
        {
            return View(roleManager.Roles.ToList());
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            // Find the role by Role ID
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            //Retrieve all the Users
            foreach (var user in UserManager.Users)
            {
                // If the user is in this role, add the username to
                // Users property of EditRoleViewModel. This model
                // object is then passed to the view for display
                if (await UserManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;

                // Update the Role using UpdateAsync
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("GetAllRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }


                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)

        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            foreach (var user in UserManager.Users)
            {
                // If the user is in this role, add the username to
                // Users property of EditRoleViewModel. This model
                // object is then passed to the view for display
                if (await UserManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }



            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EditRoleViewModel model)
        {

            var role = await roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {


                // Update the Role using UpdateAsync
                var result = await roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("GetAllRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }


                return View(model);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateViewModel model)
        {

            if (ModelState.IsValid)
            {
                IdentityRole identityrole = new IdentityRole
                {
                    Name = model.RoleName
                };

                IdentityResult result = await roleManager.CreateAsync(identityrole);
                if (result.Succeeded)
                {

                    return RedirectToAction("GetAllRoles", "Administration");
                }


            }
            return View(model);
        }



        [HttpGet]
        [Authorize(Roles = "Administrateur")]
        public async Task<IActionResult> GetAllUsers(string id)

        {

            IEnumerable<Utilisateur> utilisateurs = userService.GetAllUtilisateurDetails();
            List<UtilisateurViewModel> users = new List<UtilisateurViewModel>();
            foreach (var user in utilisateurs)
            {

                var mod = new UtilisateurViewModel
                {
                    Id = user.Id,
                    Nom = user.Nom,
                    Prenom = user.Prenom,
                    Email = user.Email,

                };
                if (await UserManager.IsInRoleAsync(user, "Administrateur"))
                { mod.role = "Administrateur"; }
                else
                {
                    if (await UserManager.IsInRoleAsync(user, "Utilisateur"))
                    {
                        mod.role = "Utilisateur";
                    }
                    else
                    {
                        mod.role = "Commercant";
                    }
                }
                users.Add(mod);

            }

            return View(users);
        }


        [HttpGet]
        [Authorize(Roles = "Administrateur")]
        public IActionResult GetALLExperienceNonVerifier(string src)
        {
            var list = experienceService.GetAllExperienceDetails("").Where(x => x.Commerçant.Verified == false).ToList();

            return View(list);

        }
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilisateur = await userService.GetById(id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            return View(utilisateur);
        }
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await userService.DeleteUtilisateurAsync(id);
            //    var utilisateur = await _context.User.FindAsync(id);
            //    _context.User.Remove(utilisateur);
            //    await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GetAllUsers));
        }

        public async Task<IActionResult> GetLogements()
        {
            var list = logementextService.GetAllLogements().ToList();

            return View(list);
        }
        //public async Task<IActionResult> GetNourriture()
        //{
        //    var list = nourritureExtService.GetAllNourriture().ToList();

        //    return View(list);
        //}
        //public async Task<IActionResult> GetTransport()
        //{
        //    var list = transportExtService.GetAllTransports();

        //    return View(list);
        //}
        public IActionResult GetALlHostes()
        {
            var list = commercantService.GetAllCommerçants().ToList();
            var listF = fournisseurService.GetAllFournisseurs().ToList();
            IList<Commerçant> listI = new List<Commerçant>();
            IList<Commerçant> listO = new List<Commerçant>();
            int i = 0;
            foreach (var item in list)
            {
                if (!(listF.Contains(item)))
                {
                    if (item.Type == "Organisme")
                    { listO.Add(item); }
                    else if (item.Type == "Individu")
                    { listI.Add(item); }
                }
                i++;
            }
            ViewBag.Orga = listO;
            ViewBag.Indi = listI;
            return View();
        }
        public IActionResult GetALlCommercant()
        {
            var list = fournisseurService.GetAllFournisseurs().ToList();

            return View(list);
        }
        public async Task<IActionResult> GetALLUtilisateur()
        {
            var list = userService.GetAllUtilisateurs().ToList();
            List<Utilisateur> users = new List<Utilisateur>();
            foreach (var item in list)
            {
                if (await UserManager.IsInRoleAsync(item, "Utilisateur"))
                {
                    users.Add(item);
                }

            }
            return View(users);
        }
        public IActionResult GetALLExp()
        {
            var list = experienceService.GetAllExperienceDetails("").ToList();

            return View(list);

        }

            if (!(UserManager.IsInRoleAsync(user, "Administrateur").Result))
            {
                await UserManager.AddToRoleAsync(user, "Administrateur");
                return RedirectToAction("GetAllUsers", "Administration");
            }
            return NoContent();
        }
    }
}
