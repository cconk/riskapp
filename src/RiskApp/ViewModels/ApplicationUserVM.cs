using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RiskApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RiskApp.ViewModels
{
    public class ApplicationUserVM : IdentityUser
    {
        public IList<UserOrg> Organizations { get; set; }
    }
}
