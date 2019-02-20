using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RiskApp.Models
{
    public class Organization
    {
        public int Id { get; set; }
        public string OrgName { get; set; }
        public string OrgDesc { get; set; }
        public IList<Department> Departments { get; set; }
        public IList<UserOrg> Users { get; set; }
    }
}
