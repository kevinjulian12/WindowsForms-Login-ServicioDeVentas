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
            connectionString = "server=sql10.freesqldatabase.com ; database=sql10355581 ; User=sql10355581 ; password=tMq84eHwti ;  integrated security = true";
        }
        protected MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
