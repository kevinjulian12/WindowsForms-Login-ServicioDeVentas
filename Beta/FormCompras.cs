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

        public string idProveedor;
        public string idProducto;
        public string Stock;

        private void txtCaracter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void btnNuevoArt_Click(object sender, EventArgs e)
        {
            limpiarFormProducto();
            txtCategoria.Enabled = true;
            txtDescripcion.Enabled = true;
            txtMarca.Enabled = true;
            txtCantidad.Enabled = true;
            txtCosto.Enabled = true;
            txtPrecio.Enabled = true;
        }

        private void limpiarFormProducto()
        {
            idProducto = null;
            Stock = null;
            txtMarca.Clear();
            txtCategoria.Clear();
            txtCantidad.Clear();
            txtPrecio.Clear();
            txtCosto.Clear();
            txtDescripcion.Clear();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            
           if (txtMarca.TextLength == 0 || txtCategoria.TextLength == 0 || txtDescripcion.TextLength == 0)
           {
               MessageBox.Show("Complete todos los campos");
           }
           else
           {
               if (txtCantidad.TextLength == 0)
               {
                   MessageBox.Show("Ingrese un valor a cantidad");
               }
               else
               {
                   if (txtPrecio.TextLength == 0)
                   {
                       MessageBox.Show("Ingrese un valor a precio");
                   }
                   else
                   {
                       if (txtCosto.TextLength == 0)
                       {
                           MessageBox.Show("Ingrese un valor a costo");
                       }
                       else
                       {
                           dataGridView1.Rows.Add(null,txtCategoria.Text, txtDescripcion.Text, txtMarca.Text, txtCosto.Text, txtPrecio.Text, txtCantidad.Text, (Convert.ToSingle(txtCosto.Text) * Convert.ToInt32(txtCantidad.Text)),1);

                            limpiarFormProducto();

                       }
                   }
               }
           }
        }

        private void btnBuscarArt_Click(object sender, EventArgs e)
        {
            ListaProductosCompra listaDeProductos = new ListaProductosCompra();
            listaDeProductos.ShowDialog();
        }

        private void btnBuscarProv_Click(object sender, EventArgs e)
        {
            ListaDeProveedor listaDeProveedor = new ListaDeProveedor();
            listaDeProveedor.ShowDialog();
        }
    }
}
