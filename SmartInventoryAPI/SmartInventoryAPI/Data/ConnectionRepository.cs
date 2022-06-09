using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SmartInventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartInventoryAPI.Data
{
    public class ConnectionRepository
    {
        

        public  ConnectionRepository()
        {
        }

        public SqlConnection OpenConnection()
        {
            ConnectionStrings _connection = new ConnectionStrings();
            var con = new SqlConnection(_connection.DefaultConnection);
            try
            {
                con.Open();
            }
            catch (SqlException e)
            {
                e.GetBaseException();
            }

            return con;
        }

        
    }
}
