using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CD_VentasItems
    {
        private CD_Conexion conexion = new CD_Conexion();
        SqlDataReader leer;
        DataTable tabla = new DataTable();
        SqlCommand comando = new SqlCommand();

        public DataTable MostrarDetVent(int IdVenta)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "select Nombre,Precio,Cantidad,PrecioTotal from ventasitems join productos on IDProducto=productos.ID where IDVenta= " + IdVenta;
            comando.CommandType = CommandType.Text;
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conexion.CerrarConexion();
            return tabla;
        }

        public void Insertar(int IDVenta,int IDProducto,float PrecioUnitario,int Cantidad,float PrecioTotal)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "insert into ventasitems values (" + IDVenta.ToString() + "," + IDProducto + "," + PrecioUnitario + "," + Cantidad + "," + PrecioTotal + ");";
            comando.CommandType = CommandType.Text;
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }

        public void EliminarDetalllesVenta(int Id)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "delete from ventas where IDVenta=" + Id;
            comando.CommandType = CommandType.Text;
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }
    }
}
