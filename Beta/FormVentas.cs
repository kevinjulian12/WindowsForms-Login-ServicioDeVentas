using Domain;
using System;
using System.Windows.Forms;

namespace Beta
{
    public partial class FormVentas : Form
    {
        public FormVentas()
        {
            InitializeComponent();
            dataGridView1.Columns[0].Visible = false;
        }

        CN_Ventas ventas = new CN_Ventas();
        CN_VentasItem ventasItem = new CN_VentasItem();
        CN_Productos Productos = new CN_Productos();
        public string IDcliente;
        public string iDproducto;
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
            if (IDcliente == null)
            {
                MessageBox.Show("seleccionar un cliente");
            }
            else
            {
                if (dataGridView1.RowCount == 0)
                {
                    MessageBox.Show("No hay productos");
                }

                else
                {
                    DateTime hora = DateTime.Today;
                    float total = Convert.ToSingle(labelTotal.Text);
                    var idVenta = ventas.InsertarVenta(Convert.ToInt32(IDcliente), hora, total);
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[0].Value != null)
                        {
                            ventasItem.InsertarItems(Convert.ToInt32(idVenta),
                                                 Convert.ToInt32(row.Cells[0].Value),
                                                 Convert.ToSingle(row.Cells[3].Value),
                                                 Convert.ToInt32(row.Cells[4].Value),
                                                 Convert.ToSingle(row.Cells[5].Value));
                            Productos.RestarStock(Convert.ToInt32(row.Cells[0].Value), Convert.ToInt32(row.Cells[4].Value));
                        }
                    }
                    dataGridView1.DataSource = "";
                    limpiarFormCliente();
                    MessageBox.Show("se inserto correctamente");
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                Sumar();
            }
            else
                MessageBox.Show("seleccione una fila por favor");

        }

        public void Sumar()
        {
            float total = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                total += (float)row.Cells[5].Value;
            }
            labelTotal.Text = Convert.ToString(total);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var stock = Productos.consultarStock(Convert.ToInt32(iDproducto));
            int sobreCarga = 0;
            if (txtCantidad.Text == "")
            {
                MessageBox.Show("Complete todos los textos, por favor");
               
            }
            else
            {
                if (txtCantidad.Text == "0")
                {
                    MessageBox.Show("Ingresar una cantidad mayor a 0");
                }
                else
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (Convert.ToInt32(row.Cells[0].Value) == Convert.ToInt32(iDproducto))
                        {
                            sobreCarga += Convert.ToInt32(row.Cells[4].Value);
                        }
                    }

                    if (sobreCarga + Convert.ToInt32(txtCantidad.Text) <= Convert.ToInt32(stock))
                    {
                        dataGridView1.Rows.Add(iDproducto, textBox1.Text, txtMarca.Text, txtPrecio.Text, txtCantidad.Text, Convert.ToSingle(txtPrecio.Text) * Convert.ToInt32(txtCantidad.Text));
                        Sumar();
                        limpiarFormProducto();
                    }
                    else
                    {
                        MessageBox.Show("No hay stock");
                    }
                }
           
            
            }
        }

        private void limpiarFormProducto()
        {
            txtMarca.Clear();
            textBox1.Clear();
            txtCantidad.Clear();
            txtPrecio.Clear();
        }

        private void limpiarFormCliente()
        {
            IDcliente = null;
            txtNombre.Clear();
            txtDireccion.Clear();
            txtApellido.Clear();
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            ListaDeClientes listaDeClientes = new ListaDeClientes();
            listaDeClientes.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListaDeProductos listaDeProductos = new ListaDeProductos();
            listaDeProductos.ShowDialog();
        }
    }
}
