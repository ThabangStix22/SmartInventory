using Microsoft.AspNetCore.Mvc;
using SmartInventoryAPI.Data;
using SmartInventoryAPI.Models;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartInventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        //instantiate class internally
        internal EmployeeRepository employeeRepo = new EmployeeRepository();



        // GET: api/<EmployeeController>
        [HttpGet]
        [Route("GetEmployees")]
        public IEnumerable<Employee> GetEmpolyees()
        {
         
            //EmployeeRepository employee = new EmployeeRepository();
            return employeeRepo.GetEmployees();
        }

        // GET api/<EmployeeController>/5
        [HttpPut("ActivateEmployee/{id}/{charActivate}")]
        public int ActivateEmployee(int id,char charActivate)
        {
            return employeeRepo.ActivateEmployee(id, charActivate);
        }


        [HttpGet("GetEmployee{EmpId}")]
       // [Route("GetEmployeeId")]
        public Employee GetEmployeeById(int EmpId)
        {
            return employeeRepo.GetEmployeeById(EmpId);
        }

        [HttpGet("GetActiveEmployees/{charActive}")]
        public IEnumerable<Employee> GetActiveEmployees(char charActive)
        {
            return employeeRepo.GetActiveEmployees(charActive);
        }

        [HttpPost("RegisterEmployee")]
        public int Register([FromBody]Employee newEmployee)
        {
            return employeeRepo.Register(newEmployee);
        }
        // PUT api/<EmployeeController>/5
        /*[HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }*/

        
    }
}
