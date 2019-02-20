using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using RiskApp.Models;

namespace RiskApp.Data
{
    public class SampleData
    {
        public async static Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            // Ensure db
            context.Database.EnsureCreated();

            // Ensure chad
            var chad = await userManager.FindByNameAsync("chadconklin@outlook.com");
            if (chad == null)
            {
                // create user
                chad = new ApplicationUser
                {
                    UserName = "chadconklin@outlook.com",
                    Email = "chadconklin@outlook.com"
                };
                await userManager.CreateAsync(chad, "Secret123!");

                // add claims
                await userManager.AddClaimAsync(chad, new Claim("IsAdmin", "true"));
            }

            // Ensure Ben
            var ben = await userManager.FindByNameAsync("bkoehler@recipe.com");
            if (ben == null)
            {
                // create user
                ben = new ApplicationUser
                {
                    UserName = "bkoehler@recipe.com",
                    Email = "bkoehler@recipe.com"
                };
                await userManager.CreateAsync(ben, "Secret234!");

                // add claims
                await userManager.AddClaimAsync(ben, new Claim("IsAdmin", "false"));
            }

            // Ensure Chris (not IsAdmin)
            var chris = await userManager.FindByNameAsync("chris.attaway@codercamps.com");
            if (chris == null)
            {
                // create user
                chris = new ApplicationUser
                {
                    UserName = "chris.attaway@codercamps.com",
                    Email = "chris.attaway@codercamps.com"
                };
                await userManager.CreateAsync(chris, "Secret345!");
            }

            var db = serviceProvider.GetService<ApplicationDbContext>();
            await db.Database.EnsureCreatedAsync();

            db.SaveChanges();

            if (!db.Organizations.Any())
            {
                db.Organizations.AddRange(
                        new Organization
                        {
                            OrgName ="Conk.com",
                            OrgDesc ="Internet Service Provider",
                            Departments = new List<Department>
                        {
                            new Department {DeptName = "Finance", DeptDesc="Includes Accounting and Financial functions", DepartmentalFunctions = new List<DepartmentalFunction>
                            {
                                new DepartmentalFunction {DepartFuncName = "Month-end close", DepartFuncDesc="All processes included in periodic close of accounting records", MissionCritical = true },
                                new DepartmentalFunction {DepartFuncName = "Month-end reporting", DepartFuncDesc="All reporting included in periodic close of accounting records", MissionCritical = false },
                                new DepartmentalFunction {DepartFuncName = "Payroll", DepartFuncDesc="All processing related to payroll", MissionCritical = true }
                            } },
                            new Department {DeptName = "Information Technology", DeptDesc="Includes information technology functions", DepartmentalFunctions = new List<DepartmentalFunction>
                            {
                                new DepartmentalFunction {DepartFuncName = "System Maintenance", DepartFuncDesc="All processes included in maintaining an operational systems environment", MissionCritical = true },
                                new DepartmentalFunction {DepartFuncName = "System Backup", DepartFuncDesc="All processes related to backing up operating systems", MissionCritical = true },
                                new DepartmentalFunction {DepartFuncName = "Application Development", DepartFuncDesc="All processing internal applications development", MissionCritical = false }
                            } }
                        } },
                        new Organization
                        {
                            OrgName = "Ben's Recipes",
                            OrgDesc = "Provider of awesome recipes",
                            Departments = new List<Department>
                        {
                            new Department {DeptName = "Accounting", DeptDesc="Includes Accounting and Financial functions", DepartmentalFunctions = new List<DepartmentalFunction>
                            {
                                new DepartmentalFunction {DepartFuncName = "Reconciliations",DepartFuncDesc="All processes included in periodic close of accounting records", MissionCritical = true },
                                new DepartmentalFunction {DepartFuncName = "Journal entry posting",DepartFuncDesc="All reporting included in periodic close of accounting records", MissionCritical = false },
                                new DepartmentalFunction {DepartFuncName = "Payroll",DepartFuncDesc="All processing related to payroll", MissionCritical = true }
                            } },
                            new Department {DeptName = "Product Development", DeptDesc="Includes functions related to developing new recipes for publication", DepartmentalFunctions = new List<DepartmentalFunction>
                            {
                                new DepartmentalFunction {DepartFuncName = "Production systems",DepartFuncDesc="All processes included in maintaining an operational recipe development environment", MissionCritical = true },
                                new DepartmentalFunction {DepartFuncName = "Product review",DepartFuncDesc="All processes related to reviewing products before release", MissionCritical = true },
                                new DepartmentalFunction {DepartFuncName = "Recipe publication and posting",DepartFuncDesc="All processes related to maintaining product quality following release", MissionCritical = false }
                            } }
                        }
                        },
                         new Organization
                         {
                             OrgName = "Code School",
                             OrgDesc = "Coding bootcamp provider",
                             Departments = new List<Department>
                            {
                                new Department {DeptName = "Operations", DeptDesc="Includes all functions not related to curriculum development and delivery", DepartmentalFunctions = new List<DepartmentalFunction>
                                {
                                    new DepartmentalFunction {DepartFuncName = "Accounting", DepartFuncDesc="All processes included in periodic close of accounting records", MissionCritical = true },
                                    new DepartmentalFunction {DepartFuncName = "Financial Reporting", DepartFuncDesc="All reporting included in periodic close of accounting records", MissionCritical = false },
                                    new DepartmentalFunction {DepartFuncName = "Payroll", DepartFuncDesc="All processing related to payroll", MissionCritical = true }
                                } },
                                new Department {DeptName = "Curriculumn Production", DeptDesc="Includes all functions related to curriculum development and delivery", DepartmentalFunctions = new List<DepartmentalFunction>
                                {
                                    new DepartmentalFunction {DepartFuncName = "Curriculum Development", DepartFuncDesc="All processes included in maintaining an operational development environment", MissionCritical = true },
                                    new DepartmentalFunction {DepartFuncName = "Curriculum Delivery", DepartFuncDesc="All processes related to delivery of content", MissionCritical = true },
                                    new DepartmentalFunction {DepartFuncName = "Curriculum Maintenance", DepartFuncDesc="All curriculum maintenance related activities", MissionCritical = false }
                                } }
                            }
                         }

                    );
                db.SaveChanges();
            }

            if (!db.UserOrgs.Any())
            {
                db.UserOrgs.AddRange(
                new UserOrg { OrganizationId = 1, AppUserName = "chadconklin@outlook.com" },
                new UserOrg { OrganizationId = 2, AppUserName = "bkoehler@recipe.com" },
                new UserOrg { OrganizationId = 3, AppUserName = "chris.attaway@codercamps.com" }
                );
                db.SaveChanges();
            }
            
            if (!db.Risks.Any())
            {
                db.Risks.AddRange(
                    new Risk { RiskTitle = "Tornado", RiskDesc= "Risk from hurricane or similar event." }, 
                    new Risk { RiskTitle = "Hurricane", RiskDesc = "Risk from hurricane or similar event."}, 
                    new Risk { RiskTitle = "Ice Storm", RiskDesc = "Risk from ice storm or similar event." }, 
                    new Risk { RiskTitle = "Blizzard", RiskDesc = "Risk from blizzard or similar event." }, 
                    new Risk { RiskTitle = "Cyber attack", RiskDesc = "Risk from cyber attack or similar event." },
                    new Risk { RiskTitle = "Social engineering attack", RiskDesc = "Risk from social engineering attack or similar event." },
                    new Risk { RiskTitle = "Earthquake", RiskDesc = "Risk from eartquake or similar event." },
                    new Risk { RiskTitle = "Volcanic eruption", RiskDesc = "Risk from volcanic eruption or similar event." },
                    new Risk { RiskTitle = "Personnel strike", RiskDesc = "Risk from personnel strike or similar event." },
                    new Risk { RiskTitle = "Class action lawsuit", RiskDesc = "Risk from class action lawsuit or similar event." },
                    new Risk { RiskTitle = "Wide-spread power outage", RiskDesc = "Risk from wide-spread power outage or similar event." },
                    new Risk { RiskTitle = "Alien invasion", RiskDesc = "Risk from alien invasion or similar event"},
                    new Risk { RiskTitle = "Zombie Apocalypse", RiskDesc = "Risk from zombie apocalypse or similar event" }
                    );
                db.SaveChanges();
            }

            if (!db.RiskCategories.Any())
            {
                db.RiskCategories.AddRange(
                    new RiskCategory { RiskCatName = "Economic", RiskCatDesc = "e.g. currency, investment" },
                    new RiskCategory { RiskCatName = "Environmental", RiskCatDesc = "e.g. political, pollution" },
                    new RiskCategory { RiskCatName = "Financial", RiskCatDesc = "e.g. revenue, increased costs, competition" },
                    new RiskCategory { RiskCatName = "Governance", RiskCatDesc = "e.g. management lapses, social responsibility" },
                    new RiskCategory { RiskCatName = "Liability", RiskCatDesc = "e.g. contractual, regulatory, litigation" },
                    new RiskCategory { RiskCatName = "Natural", RiskCatDesc = "e.g. climate, earthquake, fire, water" },
                    new RiskCategory { RiskCatName = "People", RiskCatDesc = "e.g. terrorism, strikes, sabotage, error, key persons, absence, pandemic, succession" },
                    new RiskCategory { RiskCatName = "Product liability", RiskCatDesc = "e.g. product containment/recall" },
                    new RiskCategory { RiskCatName = "Reputation", RiskCatDesc = "e.g. social responsibility, adverse publicity, allegations, rumors" },
                    new RiskCategory { RiskCatName = "Security", RiskCatDesc = "e.g. physical, systems, information" },
                    new RiskCategory { RiskCatName = "Supply Chain", RiskCatDesc = "e.g. outsourcing failure, telecommunications, power" },
                    new RiskCategory { RiskCatName = "Technology", RiskCatDesc = "e.g. systems, data, internet, telecommunictions, power" }
                    );
                db.SaveChanges();
            }

            if (!db.RiskCatRisks.Any())
            {
                db.RiskCatRisks.AddRange(
                    new RiskCatRisk { RiskId = 1, RiskCatId = 6},
                    new RiskCatRisk { RiskId = 2, RiskCatId = 6 },
                    new RiskCatRisk { RiskId = 3, RiskCatId = 6 },
                    new RiskCatRisk { RiskId = 4, RiskCatId = 6 },
                    new RiskCatRisk { RiskId = 5, RiskCatId = 7 },
                    new RiskCatRisk { RiskId = 5, RiskCatId = 10 },
                    new RiskCatRisk { RiskId = 5, RiskCatId = 12 },
                    new RiskCatRisk { RiskId = 6, RiskCatId = 7 },
                    new RiskCatRisk { RiskId = 6, RiskCatId = 10 },
                    new RiskCatRisk { RiskId = 6, RiskCatId = 12 },
                    new RiskCatRisk { RiskId = 7, RiskCatId = 6 },
                    new RiskCatRisk { RiskId = 8, RiskCatId = 6 },
                    new RiskCatRisk { RiskId = 9, RiskCatId = 7 },
                    new RiskCatRisk { RiskId = 10, RiskCatId = 7 },
                    new RiskCatRisk { RiskId = 11, RiskCatId = 11 },
                    new RiskCatRisk { RiskId = 11, RiskCatId = 12 },
                    new RiskCatRisk { RiskId = 12, RiskCatId = 7 },
                    new RiskCatRisk { RiskId = 12, RiskCatId = 12 },
                    new RiskCatRisk { RiskId = 13, RiskCatId = 7 },
                    new RiskCatRisk { RiskId = 13, RiskCatId = 12 }
                    );
                db.SaveChanges();
            }

            if (!db.RiskAssessments.Any())
            {
                db.RiskAssessments.AddRange(
                    new RiskAssessment { DepartmentalFunctionId = 1, RiskId = 1, Probability = 3, Impact = 3, MitigationLevel = 5},
                    new RiskAssessment { DepartmentalFunctionId = 2, RiskId = 2, Probability = 2, Impact = 1, MitigationLevel = 2 },
                    new RiskAssessment { DepartmentalFunctionId = 3, RiskId = 3, Probability = 4, Impact = 2, MitigationLevel = 4 },
                    new RiskAssessment { DepartmentalFunctionId = 4, RiskId = 4, Probability = 3, Impact = 3, MitigationLevel = 5 },
                    new RiskAssessment { DepartmentalFunctionId = 5, RiskId = 5, Probability = 3, Impact = 3, MitigationLevel = 5 },
                    new RiskAssessment { DepartmentalFunctionId = 6, RiskId = 6, Probability = 3, Impact = 3, MitigationLevel = 5 },
                    new RiskAssessment { DepartmentalFunctionId = 7, RiskId = 7, Probability = 3, Impact = 3, MitigationLevel = 5 },
                    new RiskAssessment { DepartmentalFunctionId = 8, RiskId = 8, Probability = 3, Impact = 3, MitigationLevel = 5 },
                    new RiskAssessment { DepartmentalFunctionId = 9, RiskId = 9, Probability = 3, Impact = 3, MitigationLevel = 5 },
                    new RiskAssessment { DepartmentalFunctionId = 10, RiskId = 10, Probability = 3, Impact = 3, MitigationLevel = 5 },
                    new RiskAssessment { DepartmentalFunctionId = 11, RiskId = 11, Probability = 3, Impact = 3, MitigationLevel = 5 },
                    new RiskAssessment { DepartmentalFunctionId = 12, RiskId = 13, Probability = 3, Impact = 3, MitigationLevel = 5 },
                    new RiskAssessment { DepartmentalFunctionId = 13, RiskId = 1, Probability = 3, Impact = 3, MitigationLevel = 5 },
                    new RiskAssessment { DepartmentalFunctionId = 14, RiskId = 2, Probability = 3, Impact = 3, MitigationLevel = 5 },
                    new RiskAssessment { DepartmentalFunctionId = 15, RiskId = 3, Probability = 3, Impact = 3, MitigationLevel = 5 },
                    new RiskAssessment { DepartmentalFunctionId = 16, RiskId = 4, Probability = 3, Impact = 3, MitigationLevel = 5 },
                    new RiskAssessment { DepartmentalFunctionId = 17, RiskId = 5, Probability = 3, Impact = 3, MitigationLevel = 5 },
                    new RiskAssessment { DepartmentalFunctionId = 18, RiskId = 6, Probability = 3, Impact = 3, MitigationLevel = 5 },
                    new RiskAssessment { DepartmentalFunctionId = 1, RiskId = 12, Probability = 3, Impact = 3, MitigationLevel = 5 },
                    new RiskAssessment { DepartmentalFunctionId = 2, RiskId = 13, Probability = 3, Impact = 3, MitigationLevel = 5 }
                    );
                db.SaveChanges();
            }
        }

    }
}
