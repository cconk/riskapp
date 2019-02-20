using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RiskApp.Services;
using RiskApp.Models;
using RiskApp.ViewModels;


// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RiskApp.API
{
    [Route("api/[controller]")]
    public class OrganizationsController : Controller
    {
        //name controllers after related model pluralized
        private OrganizationsService _organizationsService;

        public OrganizationsController(OrganizationsService organizationsService)
        {
            _organizationsService = organizationsService;
        }


        // GET: api/values
        [HttpGet]
        public IList<ApplicationUserVM> Get()
        {
            return _organizationsService.ListUsers();
        }

        // GET api/values/5
        [HttpGet("{userName}")]
        public IList<OrganizationVM> Get(string userName)
        {
            return _organizationsService.ListOrganizationsByUser(userName);
        }

       
        // POST api/values
        [HttpPost("{userName}")]
        public IActionResult Post(string userName, [FromBody]Organization newOrganization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }
            if (newOrganization.OrgName == "")
            {
                return NotFound();
            }
            else
            {
                _organizationsService.AddNewOrganization(userName, newOrganization);
                return Ok();
            }
        }
   

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Organization organization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }
            if (organization.OrgName == "")
            {
                return NotFound();
            }
            else
            {
                var userName = User.Identity.Name;
                _organizationsService.UpdateOrganization(organization);
                return Ok(organization);
            }
        }


        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [Authorize(Policy ="AdminOnly")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(this.ModelState);

            }
            else
            {
                _organizationsService.DeleteOrganization(id);
                return Ok();
            }
        }
    }
}
