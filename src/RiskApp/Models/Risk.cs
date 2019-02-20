using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskApp.Models
{
    public class Risk
    {
        public int Id { get; set; }
        public string RiskTitle { get; set; }
        public string RiskDesc { get; set; }
        
        public IList<RiskAssessment> DepartFunctions { get; set; }

        public IList<RiskCatRisk> RiskCategories { get; set; }
        
    }
}
