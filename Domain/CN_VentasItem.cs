using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using DataAccess;
using System.Data;

namespace Domain
{
    public class CN_VentasItem
    {
        CD_VentasItems VentasItems = new CD_VentasItems();

        public int idVenta { get; set; }

        public void Eliminar()
        {
            VentasItems.EliminarDetalllesVenta(idVenta);  
        }

        public DataTable MostrarVentaItem()
        {
           return VentasItems.MostrarDetVent(idVenta);
        }

        public void InsertarItems(int idVenta,int idproducto,float precioUnitario,int cantidad,float precioTotal)
        {
            VentasItems.Insertar(idVenta, idproducto, precioUnitario, cantidad, precioTotal);
        }
    }
}
