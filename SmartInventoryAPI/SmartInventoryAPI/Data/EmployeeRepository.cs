using SmartInventoryAPI.Interface;
using SmartInventoryAPI.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Dapper;
using System;

namespace SmartInventoryAPI.Data
{
    public class EmployeeRepository : IEmployee
    {
        //Used to read connectionString
        private string _connection = "Data Source=DESKTOP-7SJDS25\\THABANGMSSQLSERV;Initial Catalog=SmartInventoryDB;Integrated Security=True; TrustServerCertificate=True";
        
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
                case 'Y':
                    {
                        employees = OpenConnection().Query<Employee>(@"SELECT * FROM dbo.Employees
                                                WHERE EmpActive='" + charActive + "'");
                        if (employees != null)
                        {
                            ActiveEmployees = employees;
                        }
                        else
                        {
                            ActiveEmployees = null;
                        }
                        break;
                    }
                case 'N':
                    {
                        employees = OpenConnection().Query<Employee>(@"SELECT * FROM dbo.Employees
                                                WHERE EmpActive='" + charActive + "'");
                        if (employees != null)
                        {
                            ActiveEmployees = employees;
                        }
                        else
                        {
                            ActiveEmployees = null;
                        }
                        break;
                    }

                default:
                    Console.Error.WriteLine("Unexpected input. Error. Use 'Y' or 'N'");
                    break;
            }
            OpenConnection().Close();
            return ActiveEmployees;


        }

        //activate employee
        public int ActivateEmployee(int employeeId, char charActivate)
        {
            int result = 0;
            char empActive;
           
            //First find employee and their activation
            empActive = OpenConnection().QueryFirstOrDefault<char>(@"SELECT EmpActive FROM dbo.Employees WHERE EmpID="+employeeId+"");

            if(charActivate!=empActive)
            {
                //Returns 1 if execution completed
                result = OpenConnection().Execute(@"UPDATE dbo.Employees  
                                                 SET EmpActive='" + charActivate + "' " +
                                                 "WHERE EmpID=" + employeeId);
               
            }
            else if(charActivate==empActive)
            {
                //User is already activated / deactivated
                result = 0;
     
            }
            //Close connection
            OpenConnection().Close();

            //Result -1 would be an error
            return result;
        }

        

        public IEnumerable<Employee> GetEmployees()
        {
            IEnumerable<Employee> employees;
            //Used to controll nulls
            IEnumerable<Employee> existEmployees;
            
            employees = OpenConnection().Query<Employee>(@"SELECT * FROM dbo.Employees");
            OpenConnection().Close();

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

            employee = OpenConnection().QueryFirstOrDefault<Employee>(@"SELECT * FROM dbo.Employees
                                            WHERE EmpID='"+employeeId+"'");
            OpenConnection().Close();

            return employee;
        }


        //Pasword must be encrypted
        public int Register(Employee employee)
        {
            int control = -2;
            Employee existEmployee;
            //First check if employee exists
            existEmployee = OpenConnection().QueryFirstOrDefault<Employee>(@"SELECT * FROM dbo.Employees 
                                                            WHERE EmpEmailAddress='" + employee.EmpEmailAddress + "'");
            if (existEmployee != null)
            {   
                //Employee exists
                control = -1;
            } else if (existEmployee == null)
            {
                control = OpenConnection().Execute(@"INSERT INTO dbo.Employees
                                         VALUES('" + employee.EmpName + "','" + employee.EmpSurname + "'" +
                                         ",'" + employee.EmpDateOfEmplyt + "','" + employee.EmpPhoneNo + "'" +
                                         ",'"+employee.EmpEmailAddress+"','"+Encrypt.HashString(employee.EmpPassword)+"'," +
                                         "'"+employee.EmpRole+"','"+employee.EmpLocation+"','"+employee.EmpActive+"'," +
                                         "'"+employee.EmpImage+"')");
            }
            OpenConnection().Close();
            return control;
        }

        public int UpdateEmployee(Employee employee)
        {
            Employee existEmployee = null;
            int control = -2;

            //First find the employee
            existEmployee = OpenConnection().QueryFirstOrDefault<Employee>(@"SELECT * FROM dbo.Employees WHERE
                                                   EmpEmailAddress='"+employee.EmpEmailAddress+"'");

            if(existEmployee!=null)
            {
                control = OpenConnection().Execute(@"UPDATE dbo.Employees
                                                    SET EmpPhoneNo='"+employee.EmpPhoneNo+"', " +
                                                    "EmpEmailAddress='"+employee.EmpEmailAddress+"', " +
                                                    "EmpPassword='"+Encrypt.HashString(employee.EmpPassword)+"'," +
                                                    "EmpLocation='"+employee.EmpLocation+"'," +
                                                    "EmpImage='"+employee.EmpImage+"'");
            }
            else
            {
                control = -1;
            }

                OpenConnection().Close();
            return control;
        }


        //Helper method to avoid opening Sql
        private SqlConnection OpenConnection()
        {

            var con = new SqlConnection(_connection);
            try {
                con.Open();
             }catch(SqlException e)
            {
                e.GetBaseException();
            }
            
            return con;
        }
    }
}
