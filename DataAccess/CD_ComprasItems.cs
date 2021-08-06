using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
   public class CD_ComprasItems
    {
        private CD_Conexion conexion = new CD_Conexion();
        SqlDataReader leer;
        DataTable tabla = new DataTable();
        SqlCommand comando = new SqlCommand();

        public DataTable MostrarDetallesCompras(int IdCompra)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "select Nombre,Marca,Descripcion,PrecioCompra,PrecioVenta,Cantidad,SubTotal from ComprasItems join productos on IDProducto=productos.ID where IDCompra= " + IdCompra;
            comando.CommandType = CommandType.Text;
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conexion.CerrarConexion();
            return tabla;
        }

        public void Insertar(int IDCompra, int IDProducto, float PrecioCompra, float PrecioVenta, int Cantidad, float SubTotal)
        {
            comando.Connection = conexion.AbrirConexion();
            string total1 = SubTotal.ToString().Replace(',', '.');
            string precio1 = PrecioCompra.ToString().Replace(',', '.');
            string precio2 = PrecioVenta.ToString().Replace(',', '.');
            comando.CommandText = "insert into ComprasItems values (" + IDCompra.ToString() + "," + IDProducto + "," + precio1 + "," + precio2 + "," + Cantidad + "," + total1 + ");";
            comando.CommandType = CommandType.Text;
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }

        public void EliminarDetalllesCompra(int Id)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "delete from ComprasItems where IDCompra=" + Id;
            comando.CommandType = CommandType.Text;
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }
    }
}
