using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    class CD_Conexion
    {
        private SqlConnection Conexion = new SqlConnection("server=KEVIN\\SQLEXPRESS ; database=Trabajos ;  integrated security = true");
        public SqlConnection AbrirConexion()
        {
            if (Conexion.State == ConnectionState.Closed)
                Conexion.Open();
            return Conexion;
        }
        public SqlConnection CerrarConexion()
        {
            if (Conexion.State == ConnectionState.Open)
                Conexion.Close();
            return Conexion;
        }

    }
}
