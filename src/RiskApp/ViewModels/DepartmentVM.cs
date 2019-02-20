using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using RiskApp.Models;

namespace RiskApp.ViewModels
{
    public class DepartmentVM
    {
        public int Id { get; set; }
        public string DeptName { get; set; }
        public string DeptDesc { get; set; }
        public IList<DepartmentalFunctionVM> DepartmentalFunctions { get; set; }

    }
}
