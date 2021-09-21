using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using System.Data;

namespace Domain
{
    public class CN_Ventas
    {
        CD_Ventas Ventas = new CD_Ventas();

        public int idVenta { get; set; }

        private string idCliente;

        public string IDCliente
        {
            get { return idCliente; }
            set { idCliente = value.ToString(); }
        }

        public DataTable MostraHistVent()
        {
            return Ventas.read(Convert.ToInt32(idCliente));
        }

        public DataTable mostrarReporte()
        {
            return Ventas.MostrarReporte();
        }
      
        public object InsertarVenta(int idcliente , DateTime fecha ,float total)
        {
            return Ventas.Insertar(idcliente, fecha, total);
        }
   
        
        public void Eliminar(int idVenta)
        {
            Ventas.delete(idVenta);
        }

        public DataTable Los5ProductosMasVendidos()
        {
            return Ventas.Los5ProductosMasVendidos();
        }
    
    }
}
