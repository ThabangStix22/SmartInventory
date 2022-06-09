using SmartInventoryAPI.Interface;
using SmartInventoryAPI.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Dapper;
using System;
using System.Text.Json;
using SmartInventoryAPI.Controllers;

namespace SmartInventoryAPI.Data
{
    public class EmployeeRepository : IEmployee
    {
        //Used to read connectionString
        internal ConnectionRepository conn = new ConnectionRepository(); 

        //Default Constructor
        public EmployeeRepository()
        {

        }

        public IEnumerable<Employee> GetActiveEmployees(char charActive)
        {
            IEnumerable<Employee> employees;
            IEnumerable<Employee> ActiveEmployees = null;

            switch (charActive)
            {
                case 'Y' or 'y':
                    {
                        employees = conn.OpenConnection().Query<Employee>(@"SELECT * FROM dbo.Employees
                                                WHERE EmpActive='" + charActive + "'");

                        if (employees.AsList<Employee>().Count > 0)
                        {
                            ActiveEmployees = employees;
                        }
                        else if(employees.AsList<Employee>().Count==0)
                        {
                            ActiveEmployees = null;
                        }
                        break;
                    }
                case 'N' or 'n':
                    {
                        employees = conn.OpenConnection().Query<Employee>(@"SELECT * FROM dbo.Employees WHERE EmpActive = '"+charActive+"'");
                        if (employees.AsList<Employee>().Count > 0)
                        {
                            ActiveEmployees = employees;
                        }
                        else if(employees.AsList<Employee>().Count==0)
                        {
                            ActiveEmployees = null;
                        }
                        break;
                    }

                default:
                    Console.Error.WriteLine("Unexpected input. Error. Use 'Y' or 'N'");
                    break;
            }
            conn.OpenConnection().Close();
            return ActiveEmployees;


        }

        //activate employee
          public int ActivateEmployee(int employeeId, char charActivate)
          {
              int result = 0;
              char empActive;

              //First find employee and their activation
              empActive = conn.OpenConnection().QuerySingleOrDefault<char>(@"SELECT EmpActive FROM dbo.Employees WHERE EmpID="+employeeId+"");

              if(charActivate!=empActive)
              {
                  //Returns 1 if execution completed
                  result = conn.OpenConnection().Execute(@"UPDATE dbo.Employees  
                                                   SET EmpActive='" + charActivate + "' " +
                                                   "WHERE EmpID=" + employeeId);

              }
              else if(charActivate==empActive)
              {
                  //User is already activated / deactivated
                  result = 0;

              }
              //Close connection
              conn.OpenConnection().Close();

              //Result -1 would be an error
              return result;
          }



          public IEnumerable<Employee> GetEmployees()
          {
              IEnumerable<Employee> employees;
              //Used to controll nulls
              IEnumerable<Employee> existEmployees;

             employees =conn. OpenConnection().Query<Employee>(@"SELECT * FROM dbo.Employees");
              conn.OpenConnection().Close();

              if (employees!=null)
              {
                  existEmployees = employees;
              }
              else
              {
                  existEmployees = null;
              }

              return existEmployees;
          }

          public Employee GetEmployeeById(int employeeId)
          {
              Employee employee = null;

              employee = conn.OpenConnection().QueryFirstOrDefault<Employee>(@"SELECT * FROM dbo.Employees
                                              WHERE EmpID='"+employeeId+"'");
             conn.OpenConnection().Close();

              return employee;
          }


          //Pasword must be encrypted
          public int Register(Employee employee)
          {
              int control = -2;
              Employee existEmployee;
              //First check if employee exists
              existEmployee = conn.OpenConnection().QueryFirstOrDefault<Employee>(@"SELECT * FROM dbo.Employees 
                                                              WHERE EmpEmailAddress='" + employee.EmpEmailAddress + "'");
              if (existEmployee != null)
              {   
                  //Employee exists
                  control = -1;
              } else if (existEmployee == null)
              {
                  control = conn.OpenConnection().Execute(@"INSERT INTO dbo.Employees
                                           VALUES('" + employee.EmpName + "','" + employee.EmpSurname + "'" +
                                           ",'" + employee.EmpDateOfEmplyt + "','" + employee.EmpPhoneNo + "'" +
                                           ",'"+employee.EmpEmailAddress+"','"+Encrypt.HashString(employee.EmpPassword)+"'," +
                                           "'"+employee.EmpRole+"','"+employee.EmpLocation+"','"+employee.EmpActive+"'," +
                                           "'"+employee.EmpImage+"','"+employee.WareID+"')",employee);
              }
              conn.OpenConnection().Close();
              return control;
          }

          public int UpdateEmployee(Employee employee)
          {
              Employee existEmployee = null;
              int control = -2;

              //First find the employee
              existEmployee = conn.OpenConnection().QueryFirstOrDefault<Employee>(@"SELECT * FROM dbo.Employees WHERE
                                                     EmpEmailAddress='"+employee.EmpEmailAddress+"'");

              if(existEmployee!=null)
              {
                  control = conn.OpenConnection().Execute(@"UPDATE dbo.Employees
                                                      SET EmpPhoneNo='"+employee.EmpPhoneNo+"', " +
                                                      "EmpEmailAddress='"+employee.EmpEmailAddress+"', " +
                                                      "EmpPassword='"+Encrypt.HashString(employee.EmpPassword)+"'," +
                                                      "EmpLocation='"+employee.EmpLocation+"'," +
                                                      "EmpImage='"+employee.EmpImage+"'," +
                                                      "WareID='"+employee.WareID+"'");
              }
              else
              {
                  control = -1;
              }

                 conn.OpenConnection().Close();
              return control;
          }


    }
}
