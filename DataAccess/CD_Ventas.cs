using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DataAccess
{
    public class CD_Ventas
    {
        private CD_Conexion conexion = new CD_Conexion();
        SqlDataReader leer;
        DataTable tabla = new DataTable();
        SqlCommand comando = new SqlCommand();

        public DataTable MostrarHistVent(int IdCliente)
        { 
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "select* from ventas where IDCliente=" + IdCliente + ";";
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

        public object Insertar(int Idcliente,DateTime fecha,float total) {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "insert into ventas values (" + Idcliente + ",'" + fecha.ToString("dd/MM/yyyy") +"'," + total.ToString() + "); SELECT SCOPE_IDENTITY();";
            comando.CommandType = CommandType.Text;
            object IdVenta;
            IdVenta = comando.ExecuteScalar();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
            return IdVenta;
        }

    //public void Editar(int Id,string nombre,float precio,string categoria)
    //    { 
    //    comando.Connection = conexion.AbrirConexion();
    //    comando.CommandText = "update productos set Nombre='" + nombre + "',Precio='" + precio + "',Categoria='" + categoria + "' where ID='" + Id + "'";
    //    comando.CommandType = CommandType.Text;
    //    comando.ExecuteNonQuery();
    //    comando.Parameters.Clear();
    //    conexion.CerrarConexion();
    //    }

    public void EliminarHist(int Id)
        { 
        comando.Connection = conexion.AbrirConexion();
        comando.CommandText = "delete from ventas where ID=" + Id;
        comando.CommandType = CommandType.Text;
        comando.ExecuteNonQuery();
        comando.Parameters.Clear();
        conexion.CerrarConexion();
        }
}
}
