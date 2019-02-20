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
    public class DepartmentalFunctionsController : Controller
    {
        private DepartmentalFunctionsService _departmentalFunctionsService;

        public DepartmentalFunctionsController(DepartmentalFunctionsService departmentalFunctionsService)
        {
            _departmentalFunctionsService = departmentalFunctionsService;
        }
        // GET: api/values
        [HttpGet]
        public IList<DepartmentalFunctionVM> Get()
        {
            return _departmentalFunctionsService.ListDepartmentalFunctions();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IList<DepartmentalFunctionVM> Get(int deptId)
        {
            return _departmentalFunctionsService.ListDepartmentaFunctionsByDepartment(deptId);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post(int deptId, [FromBody]DepartmentalFunction newDepartmentalFunction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }
            if (newDepartmentalFunction.DepartFuncName == "")
            {
                return NotFound();
            }
            else
            {
                _departmentalFunctionsService.UpdateDepartmentalFunction(deptId, newDepartmentalFunction);
                return Ok(newDepartmentalFunction);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(this.ModelState);

            }
            else
            {
                _departmentalFunctionsService.DeleteDepartmentalFunctions(id);
                return Ok();
            }
        }
    }
}
