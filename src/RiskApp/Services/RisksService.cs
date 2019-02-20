using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RiskApp.Infrastructure;
using RiskApp.ViewModels;
using RiskApp.Models;
using RiskApp.Controllers;

namespace RiskApp.Services
{
    public class RisksService
    {
        private IGenericRepository _repo;

        public RisksService(IGenericRepository repo)
        {
            _repo = repo;
        }

        public IList<RiskVM> ListRisksByRiskCategory(int riskCatId)
        {
            var selectedRisks = (from rc in _repo.Query<RiskCatRisk>()
                                      where rc.Id == riskCatId
                                      select new RiskVM()
                                      {
                                          Id = rc.Risk.Id,
                                          RiskTitle = rc.Risk.RiskTitle,
                                          RiskDesc = rc.Risk.RiskDesc,
                                          RiskAssessments = (from ra in _repo.Query<RiskAssessment>()
                                                             where ra.Id == rc.Risk.Id
                                                             select new RiskAssessment()
                                                             {
                                                                 Id = ra.Id,
                                                                 Impact = ra.Impact,
                                                                 Probability = ra.Probability,
                                                                 MitigationLevel = ra.MitigationLevel
                                                             }).ToList()
                                      }).ToList();
            return selectedRisks;
        }

    }
}
