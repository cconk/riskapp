using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RiskApp.Models
{
    public class UserOrg
    {
        public int Id { get; set; }
        [ForeignKey("Organization")]
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }

        [ForeignKey("AppUser")]
        public string AppUserName { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
