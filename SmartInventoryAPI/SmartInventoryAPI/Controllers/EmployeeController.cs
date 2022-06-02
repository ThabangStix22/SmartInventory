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

        //Get all employees{For manager}
        [HttpGet]
        [Route("GetEmployees")]
        public IEnumerable<Employee> GetEmpolyees()
        {
         
            //EmployeeRepository employee = new EmployeeRepository();
            return employeeRepo.GetEmployees();
        }

        // Activate / Deactivate Employee
        [HttpPut("ActivateEmployee/{id}/{charActivate}")]
        public int ActivateEmployee(int id,char charActivate)
        {
            return employeeRepo.ActivateEmployee(id, charActivate);
        }

        //Get employee by ID
        [HttpGet("GetEmployee{EmpId}")]
        public Employee GetEmployeeById(int EmpId)
        {
            return employeeRepo.GetEmployeeById(EmpId);
        }

        //Get all active Employees {For Manager}
        [HttpGet("GetActiveEmployees/{charActive}")]
        public IEnumerable<Employee> GetActiveEmployees(char charActive)
        {
            return employeeRepo.GetActiveEmployees(charActive);
        }

        //Register Employee
        [HttpPost("RegisterEmployee")]
        public int Register([FromBody]Employee newEmployee)
        {
            return employeeRepo.Register(newEmployee);
        }


        //Update Employee Details
        [HttpPut("UpdateEmployee")]
        public int UpdateEmployee([FromBody] Employee existEmployee)
        {
            return employeeRepo.UpdateEmployee(existEmployee);
        }
       

        
    }
}
