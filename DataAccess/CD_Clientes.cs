using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace DataAccess
{
    public class CD_Clientes
    {
        private CD_Conexion conexion = new CD_Conexion();

        SqlDataReader leer;
        DataTable tabla = new DataTable();
        SqlCommand comando = new SqlCommand();

        public DataTable Read()
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "MostrarClientes";
            comando.CommandType = CommandType.StoredProcedure;
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conexion.CerrarConexion();
            return tabla;

        }
        public void Create(string Nombre, string Apellido, string Direccion, string Telefono,string Localidad)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "insert into Clientes(Nombre , Apellido, Direccion ,Telefono,Localidad ) values ('" + Nombre+"','"+Apellido+"','"+Direccion+"','"+Telefono+"','"+Localidad+"')";
            comando.CommandType = CommandType.Text;
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }
        public void Update(string ID, string Nombre, string Apellido, string Direccion, string Telefono,string Localidad)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "update Clientes set Nombre='"+Nombre+"',Apellido='"+Apellido+"',Direccion='"+Direccion+"',Telefono='"+Telefono+"',Localidad='"+Localidad+"' where ID='"+ID+"'";
            comando.CommandType = CommandType.Text;
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }
        public void Delete(int ID)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "delete from Clientes where ID="+ID;
            comando.CommandType = CommandType.Text;
            comando.Parameters.AddWithValue("@idpro", ID);
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }

    }
}
