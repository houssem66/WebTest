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

        public ReservationController(IExperienceService _experienceService)
        {
            experienceService = _experienceService;
        }
        [HttpGet]
        public async Task<IActionResult> Reserver(int id)
        { var model = await experienceService.GetExperienceByIdAsync(id);
            ViewBag.img = model.Activites.First().Image;
            return View(model);
        }
    }
}
