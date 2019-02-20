using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RiskApp.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string DeptName { get; set; }
        public string DeptDesc { get; set; }
        public IList<DepartmentalFunction> DepartmentalFunctions { get; set; }
     
    }
}
