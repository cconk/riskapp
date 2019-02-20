using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using RiskApp.Models;

namespace RiskApp.ViewModels
{
    public class RiskVM
    {

        public int Id { get; set; }
        public string RiskTitle { get; set; }
        public string RiskDesc { get; set; }
        public IList<RiskAssessment> RiskAssessments { get; set; }

        public IList<RiskCatRisk> RiskCategories { get; set; }

    }
}
