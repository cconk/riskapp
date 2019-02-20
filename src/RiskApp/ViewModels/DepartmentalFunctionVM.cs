using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using RiskApp.Models;
using RiskApp.ViewModels;

namespace RiskApp.ViewModels
{
    public class DepartmentalFunctionVM
    {
        public int Id { get; set; }
        public string DepartFuncName { get; set; }
        public string DepartFuncDesc { get; set; }
        public Boolean MissionCritical { get; set; }
        public IList<RiskAssessment> RiskAssessments { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
    }
}
