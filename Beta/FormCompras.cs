using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beta
{
    public partial class FormCompras : Form
    {
        public FormCompras()
        {
            InitializeComponent();
        }

        private void btnNuevoProv_Click(object sender, EventArgs e)
        {
            txtNombre.Enabled = true;
            txtDireccion.Enabled = true;
            txtEmail.Enabled = true;
            txtRazon_Soccial.Enabled = true;
            txtTelefono.Enabled = true;
            txtReferencia.Enabled = true;
        }

        private void btnNuevoArt_Click(object sender, EventArgs e)
        {
            txtCategoria.Enabled = true;
            txtDescripcion.Enabled = true;
            txtMarca.Enabled = true;
            txtCantidad.Enabled = true;
            txtCosto.Enabled = true;
            txtPrecio.Enabled = true;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            
        }

 
    }
}
