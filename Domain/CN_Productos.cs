using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Domain
{
    public class CN_Productos
    {
        private CD_Productos objetoCD = new CD_Productos();

        public DataTable MostrarProductos()
        {
            DataTable tabla = new DataTable();
            tabla = objetoCD.Read();
            return tabla;
        }

        public DataTable NotificacionStock()
        {
            DataTable tabla = new DataTable();
            tabla = objetoCD.NotificacionStock();
            return tabla;
        }

        public void InsertarProducto(string nombre, string desc, string marca,string costo ,string precio, string stock)
        {
            objetoCD.Create(nombre, desc, marca, Convert.ToDouble(costo), Convert.ToDouble(precio), Convert.ToInt32(stock));
        }

        public void EditarProducto(string nombre, string desc, string marca,string costo , string precio, string stock, string id)
        {
            objetoCD.Update(nombre, desc, marca, Convert.ToDouble(costo), Convert.ToDouble(precio), Convert.ToInt32(stock), Convert.ToInt32(id));
        }

        public void EliminarProducto(string id)
        {
            objetoCD.Delete(Convert.ToInt32(id));
        }

        public void RestarStock(int id, int cantidad)
        {
            objetoCD.RestaStock(Convert.ToInt32(id), Convert.ToInt32(cantidad));
        }

        public object consultarStock(int id)
        {
            return objetoCD.consultarStock(id);
        }

        public object InsertarProductosEnCompras(string nombre, string desc, string marca, string costo, string precio, string stock)
        {
          return  objetoCD.InsertarUsoCompras(nombre, desc, marca, Convert.ToDouble(costo), Convert.ToDouble(precio), Convert.ToInt32(stock));
        }
    }
}
