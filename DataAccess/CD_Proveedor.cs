using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CD_Proveedor
    {
        private CD_Conexion conexion = new CD_Conexion();

        SqlDataReader leer;
        DataTable tabla = new DataTable();
        SqlCommand comando = new SqlCommand();

        public DataTable Mostrar()
        {

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "MostrarProveedor";
            comando.CommandType = CommandType.StoredProcedure;
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conexion.CerrarConexion();
            return tabla;

        }
        public void Insertar(string Nombre, string Razon_Social, string Direccion, string Telefono, string Referencia, string Email)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "insert into Proveedor(Nombre , Razon_Social, Direccion ,Telefono ,Referencia ,Email) values ('" + Nombre + "','" + Razon_Social + "','" + Direccion + "','" + Telefono + "','"+Referencia+"','"+Email+"')";
            comando.CommandType = CommandType.Text;
            // comando.Parameters.AddWithValue("@nombre", Nombre);
            // comando.Parameters.AddWithValue("@apellido", Apellido);
            // comando.Parameters.AddWithValue("@direccion", Direccion);
            // comando.Parameters.AddWithValue("@telefono", Telefono);
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }
        public void Editar(string ID, string Nombre, string Razon_Social, string Direccion, string Telefono, string Referencia, string Email)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "update Proveedor set Nombre='" + Nombre + "',Razon_Social='" + Razon_Social + "',Direccion='" + Direccion + "',Telefono='" + Telefono + "',Referencia='"+ Referencia +"',Email='"+ Email+"' where ID='" + ID + "'";
            comando.CommandType = CommandType.Text;
            //  comando.Parameters.AddWithValue("@id",ID);
            // comando.Parameters.AddWithValue("@nombre", Nombre);
            //  comando.Parameters.AddWithValue("@apellido", Apellido);
            //  comando.Parameters.AddWithValue("@direccion", Direccion);
            //  comando.Parameters.AddWithValue("@telefono", Telefono);
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }
        public void Eliminar(int ID)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "delete from Proveedor where ID=" + ID;
            comando.CommandType = CommandType.Text;
            comando.Parameters.AddWithValue("@idpro", ID);
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }

    }
}
