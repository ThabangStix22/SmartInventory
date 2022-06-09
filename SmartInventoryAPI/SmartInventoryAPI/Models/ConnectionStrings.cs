using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartInventoryAPI.Models
{
    public class ConnectionStrings
    {
        private readonly string defaultConnection = "Data Source=DESKTOP-7SJDS25\\THABANGMSSQLSERV;Initial Catalog=SmartInventoryDB;Integrated Security=True; TrustServerCertificate=True";

        public string DefaultConnection
        {
            get { return defaultConnection; }
        }
    }
}
