﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public abstract class ConnectionToSql
    {
        private readonly string connectionString;
        public ConnectionToSql()
        {
            connectionString = "server=DESKTOP-15K1INI\\SQLEXPRESS ; database=Beta ;  integrated security = true";
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
