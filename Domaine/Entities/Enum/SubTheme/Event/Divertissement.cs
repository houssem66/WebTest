using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Data.Entities.Enum.SubTheme
{
    public enum Divertissement
    {
        [Display(Name = "Feux d'artifice")] a,
        [Display(Name = "Comédie")] x,
        [Display(Name = "Jeux")] Jeux,
        [Display(Name = "Films,télèvision ou radio")] n,
        [Display(Name = "Surnaturel")]b,
        [Display(Name = "Vie Nocturne")]c,
        [Display(Name = "Evénement sportif")]d,
        [Display(Name = "Cirque")]f,
        [Display(Name = "Danse")]t,
        [Display(Name = "Magie")]y,
        [Display(Name = "Musique")]u,
        [Display(Name = "Théâtre")]i,
        [Display(Name = "Autre Divertissement")]h
    }
}
