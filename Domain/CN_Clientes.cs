using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Domain
{
    public class CN_Clientes
    {
        private CD_Clientes objetoCD = new CD_Clientes();

        public DataTable MostrarClien()
        {
            DataTable tabla = new DataTable();
            tabla = objetoCD.Mostrar();
            return tabla;
        }
        public void InsertarClien(string Nombre, string Apellido, string Direccion, string Telefono,string Localidad)
        {
            objetoCD.Insertar(Nombre, Apellido, Direccion, Telefono,Localidad);
        }
        public void EditarClien(string ID, string Nombre, string Apellido, string Direccion, string Telefono,string Localidad)
        {
            objetoCD.Editar(ID,Nombre, Apellido, Direccion, Telefono,Localidad);
        }
        public void EliminarClien(string ID)
        {
            objetoCD.Eliminar(Convert.ToInt32(ID));
        }

    }
}
