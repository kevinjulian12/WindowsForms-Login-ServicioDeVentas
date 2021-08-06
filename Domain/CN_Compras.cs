using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using System.Data;

namespace Domain
{
   public class CN_Compras
    {
        CD_Compras compras = new CD_Compras();


        public DataTable MostraHistComp(int IdProveedor)
        {
            return compras.MostrarHistComp(IdProveedor);
        }
        public DataTable mostrarReporte()
        {
            return compras.MostrarReporte();
        }

        public object InsertarCompra(int idproveedor, DateTime fecha, float total)
        {
            return compras.Insertar(idproveedor, fecha, total);
        }


        public void Eliminar(int idCompra)
        {
            compras.EliminarHist(idCompra);
        }

    }
}
