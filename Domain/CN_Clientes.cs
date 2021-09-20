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

        private string _id;
        private String elApellido;
        private String elNombre;
        private String elTelefono;
        private String laDireccion;
        private String laLocalidad;

        public string id
        {
            get { return _id; }
            set { _id = value.ToString(); }
        }

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

        public DataTable MostrarClientes()
        {
            DataTable tabla = new DataTable();
            tabla = objetoCD.Read();
            return tabla;
        }

        public void InsertarCliente()
        {
            objetoCD.Create(elNombre, elApellido, laDireccion, elTelefono,laLocalidad);
        }

        public void EditarCliente()
        {
            objetoCD.Update(_id,elNombre, elApellido, laDireccion, elTelefono, laLocalidad);
        }

        public void EliminarCliente()
        {
            objetoCD.Delete(Convert.ToInt32(_id));
        }

    }
}
