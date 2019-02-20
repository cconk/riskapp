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
    public class DepartmentsService
    {
        private IGenericRepository _repo;

        public DepartmentsService(IGenericRepository repo)
        {
            _repo = repo;
        }

        public IList<DepartmentVM> ListDepartmentsByOrganization(int orgId)
        {
            var selectedOrganization = (from o in _repo.Query<Organization>()
                                       where o.Id == orgId
                                       select new OrganizationVM()
                                       {
                                           Departments = (from d in o.Departments
                                                          select new DepartmentVM()
                                                          {
                                                              Id = d.Id,
                                                              DeptName = d.DeptName,
                                                              DeptDesc = d.DeptDesc,
                                                              DepartmentalFunctions = (from df in d.DepartmentalFunctions
                                                                                       select new DepartmentalFunctionVM()
                                                                                       {
                                                                                           Id = df.Id,
                                                                                           DepartFuncName = df.DepartFuncName,
                                                                                           DepartFuncDesc = df.DepartFuncDesc,
                                                                                           MissionCritical = df.MissionCritical
                                                                                       }).ToList()
                                                          }).ToList()
                                       }).FirstOrDefault(); 
            return selectedOrganization.Departments;
        }

        public void AddNewDepartment (int orgId, Department newDepartment )
        {
            var selectedOrganization = (from o in _repo.Query<Organization>().Include(d => d.Departments)
                                        where o.Id == orgId
                                        select o).FirstOrDefault();
            var deparment = new Department()
            {
                DeptName = newDepartment.DeptName,
                DeptDesc = newDepartment.DeptDesc
            };

            selectedOrganization.Departments.Add(deparment);
            _repo.Update(selectedOrganization);
        }

        public void UpdateDepartment (int deptId, Department selectedDepartment )
        {
            var departmentToUpdate = (from d in _repo.Query<Department>()
                                      where d.Id == deptId
                                      select new Department()
                                      {
                                          Id = d.Id,
                                          DeptName = d.DeptName,
                                          DeptDesc = d.DeptDesc
                                      }).FirstOrDefault();
            departmentToUpdate.DeptName = selectedDepartment.DeptName;
            departmentToUpdate.DeptDesc = selectedDepartment.DeptDesc;
            _repo.Update(departmentToUpdate);
            _repo.SaveChanges();
        }

        public IList<DepartmentVM> ListDepartments() 
        {
            var departments = (from d in _repo.Query<Department>()
                                 select new DepartmentVM()
                                 {
                                     DeptName = d.DeptName,
                                     DeptDesc = d.DeptDesc
                                 }).ToList();
            return departments;
        }

        public void DeleteDepartments(int id)
        {
            var departmentToDelete = (from o in _repo.Query<Department>()
                                      where o.Id == id
                                      select o).FirstOrDefault();

            _repo.Delete(departmentToDelete);
        }

    }
}
