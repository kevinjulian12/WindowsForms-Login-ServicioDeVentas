using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using Domain;

namespace Beta
{
    public partial class FormVentas : Form
    {
        public FormVentas()
        {
            InitializeComponent();
        }

        CN_Ventas ventas = new CN_Ventas();
        CN_VentasItem ventasItem = new CN_VentasItem();
        private void txtCaracter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtIDC.Text == "") {
                MessageBox.Show("seleccionar un cliente");
            }
            else {
                DateTime hora = DateTime.Today;
                float total = Convert.ToSingle(labelTotal.Text);
                var idVenta = ventas.InsertarVenta(Convert.ToInt32(txtIDC.Text), hora, total);
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[0].Value != null) {
                        ventasItem.InsertarItems(Convert.ToInt32(idVenta),
                                             Convert.ToInt32(row.Cells[0].Value),
                                             Convert.ToSingle(row.Cells[2].Value),
                                             Convert.ToInt32(row.Cells[3].Value),
                                             Convert.ToSingle(row.Cells[4].Value));
                    }
                }
                MessageBox.Show("se inserto correctamente");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            labelTotal.Text = Sumar();
                    }
            else
                MessageBox.Show("seleccione una fila por favor");
        
        }
        
        public string Sumar()
        {
            const int columna = 4;

            int suma = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                suma += (int)row.Cells[columna].Value;
            }
            return Convert.ToString(suma);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtCantidad.Text == "") {
                MessageBox.Show("Complete todos los textos, por favor");
                    }
        else
            dataGridView1.Rows.Add(txtIDP.Text, textBox1.Text, txtPrecio.Text, txtCantidad.Text, Convert.ToSingle(txtPrecio.Text)* Convert.ToInt32(txtCantidad.Text));
            labelTotal.Text = Sumar();
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            ListaDeClientes listaDeClientes = new ListaDeClientes();
            listaDeClientes.ShowDialog();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
