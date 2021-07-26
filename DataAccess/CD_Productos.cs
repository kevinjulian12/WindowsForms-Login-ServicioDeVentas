using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class CD_Productos
    {
        private CD_Conexion conexion = new CD_Conexion();
        SqlDataReader leer;
        DataTable tabla = new DataTable();
        SqlCommand comando = new SqlCommand();
        public DataTable Mostrar()
        {

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "MostrarProductos";
            comando.CommandType = CommandType.StoredProcedure;
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conexion.CerrarConexion();
            return tabla;

        }
        public void Insertar(string nombre, string desc, string marca,double costo, double precio, int stock)
        {
            comando.Connection = conexion.AbrirConexion();
            //comando.CommandText = "insert into Productos(Nombre , Descripcion , Marca , Precio , Stock) values('" + nombre+"', '"+desc+"', '"+marca+"', '"+precio+"','"+stock+"')";
            comando.CommandText = "InsetarProductos";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@nombre", nombre);
            comando.Parameters.AddWithValue("@descrip", desc);
            comando.Parameters.AddWithValue("@Marca", marca);
            comando.Parameters.AddWithValue("@costo", costo);
            comando.Parameters.AddWithValue("@precio", precio);
            comando.Parameters.AddWithValue("@stock", stock);
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }
        public void Editar(string nombre, string desc, string marca, double precio, int stock, int id)
        {
            comando.Connection = conexion.AbrirConexion();
            //comando.CommandText = "update Productos set Nombre='" + nombre + "',Descripcion='" + desc + "',Marca='" + marca + "',Precio='" + precio + "',Stock='"+stock+"' where ID='" + id + "'";
            comando.CommandText = "EditarProductos";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@nombre", nombre);
            comando.Parameters.AddWithValue("@descrip", desc);
            comando.Parameters.AddWithValue("@Marca", marca);
            comando.Parameters.AddWithValue("@precio", precio);
            comando.Parameters.AddWithValue("@stock", stock);
            comando.Parameters.AddWithValue("@id", id);
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }
        public void Eliminar(int id)
        {
            comando.Connection = conexion.AbrirConexion();
            // comando.CommandText = "delete from Productos where ID=" + id;
            comando.CommandText = "EliminarProducto";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@idpro", id);
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }

        public void RestaStock(int id, int cantidad)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "RestarStock";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@idpro", id);
            comando.Parameters.AddWithValue("@cant", cantidad);
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }

        public object consultarStock(int id)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "select Stock from Productos where Id=" + id;
            comando.CommandType = CommandType.Text;
            object stock;
            stock = comando.ExecuteScalar();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
            return stock;
        }
    }
}
