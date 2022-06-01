using SmartInventoryAPI.Models;
using System.Collections.Generic;

namespace SmartInventoryAPI.Interface
{
    public interface IEmployee
    {
        //Get all employees
        IEnumerable<Employee> GetEmployees();
        
        IEnumerable<Employee> GetActiveEmployees(char charActive);

        int ActivateEmployee(int employeeId,char charActivate);

        Employee GetEmployeeById(int employeeId);
        // Employee FindEmployee(string employee);

        int Register(Employee employee);

        //Update Employee details
        int UpdateEmployee(Employee employee);


    }
}
