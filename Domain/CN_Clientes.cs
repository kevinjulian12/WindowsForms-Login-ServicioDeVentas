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

        private String elApellido;
        private String elNombre;
        private String elTelefono;
        private String laDireccion;
        private String laLocalidad;

        public String Apellido
        {
            get { return elApellido; }
            set { elApellido = value.ToString(); }
        }
        public String Nombre
        {
            get { return elNombre; }
            set { elNombre = value.ToString(); }
        }
        public String Telefono
        {
            get { return elTelefono; }
            set { elTelefono = value.ToString(); }
        }
        public String Direccion
        {
            get { return laDireccion; }
            set { laDireccion = value.ToString(); }
        }
        public String Localidad
        {
            get { return laLocalidad; }
            set { laLocalidad = value.ToString(); }
        }

        public DataTable MostrarClien()
        {
            DataTable tabla = new DataTable();
            tabla = objetoCD.Mostrar();
            return tabla;
        }
        public void InsertarClien()
        {
            objetoCD.Insertar(elNombre, elApellido, laDireccion, elTelefono,laLocalidad);
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
