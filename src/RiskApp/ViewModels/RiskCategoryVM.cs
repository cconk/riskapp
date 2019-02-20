using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RiskApp.Models;

namespace RiskApp.ViewModels
{
    public class RiskCategoryVM
    {
        public int Id { get; set; }
        public string RiskCatName { get; set; }
        public string RiskCatDesc { get; set; }

        public IList<RiskCatRisk> Risks { get; set; }
    }
}
