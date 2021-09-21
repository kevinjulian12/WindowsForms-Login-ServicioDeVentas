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

        public DataTable Read()
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "MostrarProductos";
            comando.CommandType = CommandType.StoredProcedure;
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conexion.CerrarConexion();
            return tabla;
        }

        public void Create(string nombre, string desc, string marca,double costo, double precio, int stock)
        {
            comando.Connection = conexion.AbrirConexion();
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

        public object InsertarUsoCompras(string nombre, string desc, string marca, double costo, double precio, int stock)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "insert into Productos(Producto , Descripcion , Marca , Costo ,Precio , Stock) values('" + nombre + "', '" + desc + "', '" + marca + "', '" + costo + "', '" + precio + "','" + stock + "'); SELECT SCOPE_IDENTITY();";
            comando.CommandType = CommandType.Text;
            object Idproducto;
            Idproducto = comando.ExecuteScalar();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
            return Idproducto;
        }

        public void Update(string nombre, string desc, string marca,double costo , double precio, int stock, int id)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "EditarProductos";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@nombre", nombre);
            comando.Parameters.AddWithValue("@descrip", desc);
            comando.Parameters.AddWithValue("@Marca", marca);
            comando.Parameters.AddWithValue("@Costo", costo);
            comando.Parameters.AddWithValue("@precio", precio);
            comando.Parameters.AddWithValue("@stock", stock);
            comando.Parameters.AddWithValue("@id", id);
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }

        public void Delete(int id)
        {
            comando.Connection = conexion.AbrirConexion();
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

        public DataTable NotificacionStock()
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "select Stock,Producto,Descripcion,Marca from Productos ";
            comando.CommandType = CommandType.Text;
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conexion.CerrarConexion();
            return tabla;
        }
    }
}
