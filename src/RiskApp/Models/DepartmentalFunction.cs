using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RiskApp.Models
{
    public class DepartmentalFunction
    {
        public int Id { get; set; }
        public string DepartFuncName { get; set; }
        public string DepartFuncDesc { get; set; }
        public Boolean MissionCritical { get; set; }
        public IList<RiskAssessment> RiskAssessments { get; set; }
    }
}
