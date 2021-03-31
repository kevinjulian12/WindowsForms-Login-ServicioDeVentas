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
        public int IDCliente { get; set; }
        public int idVenta { get; set; }

        public DataTable MostraHistVent()
        {
            return Ventas.MostrarHistVent(IDCliente);
        }
        public DataTable mostrarReporte()
        {
            return Ventas.MostrarReporte();
        }
      
        public object InsertarVenta(int idcliente , DateTime fecha ,float total)
        {
            return Ventas.Insertar(idcliente, fecha, total);
        }
   
        
        public void Eliminar()
        {
            Ventas.EliminarHist(idVenta);
        }
       
    
    }
}
