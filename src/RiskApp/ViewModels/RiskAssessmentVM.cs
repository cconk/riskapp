using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskApp.ViewModels
{
    public class RiskAssessmentVM
    {
        public int Id { get; set; }
        public RiskVM Risk { get; set; }
        public int Probability { get; set; }
        public int Impact { get; set; }
        public int MitigationLevel { get; set; }

    }
}
