using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CN_Proveedor
    {
        private CD_Proveedor objetoCD = new CD_Proveedor();
        public DataTable MostrarProv()
        {
            DataTable tabla = new DataTable();
            tabla = objetoCD.Mostrar();
            return tabla;
        }
        public void InsertarPRov(string nombre, string razon, string direccion, string telefon, string referencia, string email)
        {
            objetoCD.Insertar(nombre, razon, direccion, telefon, referencia,email);
        }
        public void EditarProv(string nombre, string razon, string direccion, string telefon, string referencia, string email, string id)
        {
            objetoCD.Editar(nombre, razon, direccion, telefon, referencia, email,id);
        }
        public void EliminarPRov(string id)
        {
            objetoCD.Eliminar(Convert.ToInt32(id));
        }
    }
}
