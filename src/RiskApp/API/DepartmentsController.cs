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
    public class DepartmentsController : Controller
    {
        //name controllers after related model pluralized
        private DepartmentsService _departmentsService;

        public DepartmentsController(DepartmentsService departmentsService)
        {
            _departmentsService = departmentsService;
        }

        // GET: api/values
        [HttpGet]
        public IList<DepartmentVM> Get()
        {
            return _departmentsService.ListDepartments();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IList<DepartmentVM> Get(int orgId)
        {
            return _departmentsService.ListDepartmentsByOrganization(orgId);
        }

        // POST api/values
        [HttpPost("{orgId}")]
        public IActionResult Post(int orgId, [FromBody] Department newDepartment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }
            if (newDepartment.DeptName == "")
            {
                return NotFound();
            }
            else
            {
                _departmentsService.UpdateDepartment(orgId, newDepartment);
                return Ok(newDepartment);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(this.ModelState);

            }
            else
            {
                _departmentsService.DeleteDepartments(id);
                return Ok();
            }
        }
    }
}
