using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RiskApp.Models
{
    public class RiskCatRisk
    {
        public int Id { get; set; }
        [ForeignKey("RiskCat")]
        public int RiskCatId { get; set; }
        public RiskCategory RiskCat { get; set; }
        [ForeignKey("Risk")]
        public int RiskId { get; set; }
        public Risk Risk { get; set; }
    }
}
