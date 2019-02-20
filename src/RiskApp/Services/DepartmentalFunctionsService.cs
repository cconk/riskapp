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
    public class DepartmentalFunctionsService
    {
        private IGenericRepository _repo;

        public DepartmentalFunctionsService(IGenericRepository repo)
        {
            _repo = repo;
        }

        public IList<DepartmentalFunctionVM> ListDepartmentaFunctionsByDepartment(int deptId)
        {
            var selectedDepartment = (from d in _repo.Query<Department>()
                                        where d.Id == deptId
                                        select new DepartmentVM()
                                        {
                                            DepartmentalFunctions = (from df in d.DepartmentalFunctions
                                                           select new DepartmentalFunctionVM()
                                                           {
                                                               Id = df.Id,
                                                               DepartFuncName = df.DepartFuncName,
                                                               DepartFuncDesc = df.DepartFuncDesc,
                                                               MissionCritical = df.MissionCritical,
                                                               RiskAssessments = (from ra in df.RiskAssessments
                                                                                  select new RiskAssessment()
                                                                                  {
                                                                                      Id = ra.RiskId,
                                                                                      Impact = ra.Impact,
                                                                                      Probability = ra.Probability,
                                                                                      MitigationLevel = ra.MitigationLevel,
                                                                                      Risk = new Risk
                                                                                      {
                                                                                          RiskTitle = ra.Risk.RiskTitle
                                                                                      }
                                                                                  }).ToList()
                                                           }).ToList()
                                        }).FirstOrDefault();
            return selectedDepartment.DepartmentalFunctions;
        }
        public void AddNewDepartmentalFunction(int deptId, DepartmentalFunction newDepartmentalFunction)
        {
            var selectedDepartment = (from d in _repo.Query<Department>().Include(d => d.DepartmentalFunctions)
                                        where d.Id == deptId
                                        select d).FirstOrDefault();
            var deparmentalFunction = new DepartmentalFunction()
            {
                DepartFuncName = newDepartmentalFunction.DepartFuncName,
                DepartFuncDesc = newDepartmentalFunction.DepartFuncDesc,
                MissionCritical = newDepartmentalFunction.MissionCritical
            };

            selectedDepartment.DepartmentalFunctions.Add(deparmentalFunction);
            _repo.Update(selectedDepartment);
        }

        public void UpdateDepartmentalFunction(int deptFuncId, DepartmentalFunction selectedDepartmentalFunction)
        {
            var departmentalFunctionToUpdate = (from df in _repo.Query<DepartmentalFunction>()
                                      where df.Id == deptFuncId
                                      select new DepartmentalFunction()
                                      {
                                          Id = df.Id,
                                          DepartFuncName = df.DepartFuncName,
                                          DepartFuncDesc = df.DepartFuncDesc,
                                          MissionCritical = df.MissionCritical
                                      }).FirstOrDefault();
            departmentalFunctionToUpdate.DepartFuncName = selectedDepartmentalFunction.DepartFuncName;
            departmentalFunctionToUpdate.DepartFuncDesc = selectedDepartmentalFunction.DepartFuncDesc;
            _repo.Update(departmentalFunctionToUpdate);
            _repo.SaveChanges();
        }

        public IList<DepartmentalFunctionVM> ListDepartmentalFunctions()
        {
            var departments = (from df in _repo.Query<DepartmentalFunction>()
                               select new DepartmentalFunctionVM()
                               {
                                   DepartFuncName = df.DepartFuncName,
                                   DepartFuncDesc = df.DepartFuncDesc
                               }).ToList();
            return departments;
        }

        public void DeleteDepartmentalFunctions(int id)
        {
            var departmentalFunctionToDelete = (from df in _repo.Query<DepartmentalFunction>()
                                      where df.Id == id
                                      select df).FirstOrDefault();

            _repo.Delete(departmentalFunctionToDelete);
        }
    }
}
