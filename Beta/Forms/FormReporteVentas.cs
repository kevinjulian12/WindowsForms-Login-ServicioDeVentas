using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain;

namespace Beta
{
    public partial class FormReporteVentas : Form
    {
        public FormReporteVentas()
        {
            InitializeComponent();
        }

        CN_Ventas ventas = new CN_Ventas();

        private void FormEstadisticasVentas_Load(object sender, EventArgs e)
        {
            Dashboard();
        }

        
        private void Dashboard()
        {
            //string productos = Convert.ToString(ventas.Los5ProductosMasVendidos().Columns[0]);
            //int nroVentas = Convert.ToInt32(ventas.Los5ProductosMasVendidos().Columns[1]);
            int contador = 0;
            foreach (DataRow item in ventas.Los5ProductosMasVendidos().Rows)
            {
                
                chart1.Series[0].Points.AddY(item[1]);
                chart1.Series[0].Points[contador].LegendText = Convert.ToString(item[0]);
                contador += 1;
            }


            
        }
    }
}
