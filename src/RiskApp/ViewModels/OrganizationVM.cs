using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using RiskApp.Models;

namespace RiskApp.ViewModels
{
    public class OrganizationVM
    {
        public int Id { get; set; }
        public string OrgName { get; set; }
        public string OrgDesc { get; set; }
        public IList<DepartmentVM> Departments { get; set; }
        public IList<UserOrg> Users { get; set; }
    }
}
