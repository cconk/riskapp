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
    public class OrganizationsService
    {
        private IGenericRepository _repo;

        public OrganizationsService(IGenericRepository repo)
        {
            _repo = repo;
        }

        public IList<ApplicationUserVM> ListUsers()
        {
            var Users = (from i in _repo.Query<ApplicationUser>()
                                  select new ApplicationUserVM()
                                  {
                                        UserName = i.UserName
                                  }).ToList();
            return Users;
        }

        public IList<OrganizationVM> ListOrganizationsByUser(string userName)
        {
            var selectedUserOrganizations = (from i in _repo.Query<UserOrg>()
                                where i.AppUserName == userName
                                select new OrganizationVM()
                                {
                                    Id = i.Id,
                                    OrgName = i.Organization.OrgName,
                                    OrgDesc = i.Organization.OrgDesc
                                }).ToList();
            return selectedUserOrganizations;
        }

        public IList<OrganizationVM> ListSelectedOrg (int id)
        {
            var selectedOrganizations = (from i in _repo.Query<Organization>()
                                             where i.Id == id
                                             select new OrganizationVM()
                                             {
                                                 Id = i.Id,
                                                 OrgName = i.OrgName,
                                                 OrgDesc = i.OrgDesc
                                             }).ToList();
            return selectedOrganizations;
        }

        public void AddNewOrganization(string userName, Organization newOrganization)
        {
            var organizationToAdd = new Organization()
            {
                OrgDesc = newOrganization.OrgDesc,
                OrgName = newOrganization.OrgName
            };

            _repo.Add(organizationToAdd);
            _repo.SaveChanges();

            var UserOrg = new UserOrg()
            {
                AppUserName = userName,
                OrganizationId = organizationToAdd.Id
            };

            _repo.Add(UserOrg);
            _repo.SaveChanges();
        }

        public void UpdateOrganization(Organization selectedOrganization)
        {
            var organizationToUpdate = (from o in _repo.Query<Organization>()
                                        where o.Id == selectedOrganization.Id
                                        select new Organization()
                                        {
                                            Id = o.Id,
                                            OrgName = o.OrgName,
                                            OrgDesc = o.OrgDesc,
                                            Users = (from u in o.Users
                                                     select new UserOrg()
                                            {
                                                ApplicationUser = new ApplicationUser()
                                                {
                                                    UserName = u.ApplicationUser.UserName
                                                }    
                                            }).ToList()
                                        }).FirstOrDefault();

            var user = organizationToUpdate.Users[0].ApplicationUser.UserName;
            organizationToUpdate.Users = null;
            organizationToUpdate.OrgName = selectedOrganization.OrgName;
            organizationToUpdate.OrgDesc = selectedOrganization.OrgDesc;
            _repo.Update(organizationToUpdate);
            _repo.SaveChanges();
        }

        public IList<OrganizationVM> ListOrganizations() // may not ever need this method
        {
            var organizations = (from o in _repo.Query<Organization>()
                              select new OrganizationVM()
                              {
                                  OrgName = o.OrgName,
                                  OrgDesc = o.OrgDesc
                              }).ToList();
            return organizations;
        }

        public void DeleteOrganization(int id)
        {
            var organizationToDelete = (from o in _repo.Query<Organization>()
                                        where o.Id == id
                                        select o).FirstOrDefault();

            _repo.Delete(organizationToDelete);
        }
    }
}

