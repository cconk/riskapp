using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RiskApp.Models
{
    public class RiskAssessment
    {
        public int Id { get; set; }

        [ForeignKey("DepartmentalFunction")]
        public int DepartmentalFunctionId { get; set; }
        public DepartmentalFunction DepartmentalFunction { get; set; }

        [ForeignKey("Risk")]
        public int RiskId { get; set; }
        public Risk Risk { get; set; }

        public int Probability { get; set; }
        public int Impact { get; set; }
        public int MitigationLevel { get; set; }
       

    }
}
