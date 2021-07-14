using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TourMe.Web.Models
{
    public static class ClaimsStore
    {
        public static List<Claim> AllClaims = new List<Claim>() {
        new Claim("Create Experience", "Create Experience"),
        new Claim("Edit Experience","Edit Experience"),
        new Claim("Delete Experience","Delete Experience"),
        
        new Claim("Edit Profil","Edit Profil"),


        };
    }
}
