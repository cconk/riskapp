using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskApp.Models
{
    public class RiskCategory
    {
        public int Id { get; set; }
        public string RiskCatName { get; set; }
        public string RiskCatDesc { get; set; }

        public IList<RiskCatRisk> Risks { get; set; }

    }
}
