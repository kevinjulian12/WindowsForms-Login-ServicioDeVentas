using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataAccess
{
    public abstract class ConnectionToMySql
    {
        private readonly string connectionString;
        public ConnectionToMySql()
        {
            connectionString = "server=sql10.freesqldatabase.com ; database=sql ; User=sql; password= ;  integrated security = true";
        }
        protected MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
