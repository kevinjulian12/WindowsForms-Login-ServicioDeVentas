using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CD_Compras
    {
        private CD_Conexion conexion = new CD_Conexion();
        SqlDataReader leer;
        DataTable tabla = new DataTable();
        SqlCommand comando = new SqlCommand();

        public DataTable MostrarHistComp(int IdProveedor)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "select* from Compras where IDProveedor=" + IdProveedor + ";";
            comando.CommandType = CommandType.Text;
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conexion.CerrarConexion();
            return tabla;
        }

        public DataTable MostrarReporte()
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "select Cliente,Telefono,Correo,Nombre,Categoria,Precio,Cantidad,PrecioTotal from ventasitems inner join ventas v on IDVenta = v.ID inner join clientes c on IDCliente = c.ID inner join productos p on IDProducto = p.ID ";
            comando.CommandType = CommandType.Text;
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conexion.CerrarConexion();
            return tabla;
        }

        public object Insertar(int idProveedor, DateTime fecha, float total)
        {
            comando.Connection = conexion.AbrirConexion();
            string total1 = total.ToString().Replace(',', '.');
            comando.CommandText = "insert into Compras values (" + idProveedor + ",'" + fecha.ToString("dd/MM/yyyy") + "'," + total1 + "); SELECT SCOPE_IDENTITY();";
            comando.CommandType = CommandType.Text;
            object IdProveedor;
            IdProveedor = comando.ExecuteScalar();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
            return IdProveedor;
        }



        public void EliminarHist(int Id)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "delete from Compras where ID=" + Id;
            comando.CommandType = CommandType.Text;
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }
    }
}
