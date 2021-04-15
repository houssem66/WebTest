using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourMe.Web.Models
{

    public class UserClaimsViewModel
    {
        public UserClaimsViewModel()
        {
            Cliams = new List<UserClaim>();
        }

        public string id { get; set; }
        public List<UserClaim> Cliams { get; set; }
    }

}
