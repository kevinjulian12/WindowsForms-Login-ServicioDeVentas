using Domain;
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
            dataGridView1.Columns.GetFirstColumn(0).Visible = false;
         
        }

        CN_Productos Productos = new CN_Productos();
        CN_Compras compras = new CN_Compras();
        CN_ComprasItem comprasItem = new CN_ComprasItem();

        public string idProveedor;
        public string idProducto;
        public string Stock;
        public bool NuevoArticulo = false;

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
            NuevoArticulo = true;
        }

        private void limpiarFormProducto()
        {
            idProducto = null;
            Stock = null;
            NuevoArticulo = false;
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
                            if (NuevoArticulo==true)
                            {
                                dataGridView1.Rows.Add(null, txtCategoria.Text, txtDescripcion.Text, txtMarca.Text, txtCosto.Text, txtPrecio.Text, txtCantidad.Text, (Convert.ToSingle(txtCosto.Text) * Convert.ToInt32(txtCantidad.Text)), 1);
                                Sumar();
                                limpiarFormProducto();
                            }
                            else
                            {
                                dataGridView1.Rows.Add(idProducto, txtCategoria.Text, txtDescripcion.Text, txtMarca.Text, txtCosto.Text, txtPrecio.Text, txtCantidad.Text, (Convert.ToSingle(txtCosto.Text) * Convert.ToInt32(txtCantidad.Text)),Convert.ToInt32( Stock)+ Convert.ToInt32(txtCantidad.Text));
                                Sumar();
                                limpiarFormProducto();
                            }
                           

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
                total += (float)row.Cells[7].Value;
            }
            labelTotal.Text = Convert.ToString(total);
        }

        private void limpiarFormProveedor()
        {
            idProveedor = null;
            txtNombre.Clear();
            txtRazon_Soccial.Clear();
            txtEmail.Clear();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (idProveedor == null)
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
                    var idCompra = compras.InsertarCompra(Convert.ToInt32(idProveedor), hora, total);
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[0].Value != null)
                        {
                            comprasItem.InsertarItems(Convert.ToInt32(idCompra),
                                                  Convert.ToInt32(row.Cells[0].Value),
                                                  Convert.ToSingle(row.Cells[4].Value),
                                                  Convert.ToSingle(row.Cells[5].Value),
                                                  Convert.ToInt32(row.Cells[6].Value),
                                                  Convert.ToSingle(row.Cells[7].Value));
                            Productos.EditarProd(Convert.ToString(row.Cells[1].Value), Convert.ToString(row.Cells[2].Value), Convert.ToString(row.Cells[3].Value), Convert.ToString(row.Cells[4].Value), Convert.ToString(row.Cells[5].Value), Convert.ToString(row.Cells[8].Value), Convert.ToString(row.Cells[0].Value));
                        }
                        else
                        {
                            var _idProducto = Productos.InsertarProductosEnCompras(Convert.ToString(row.Cells[1].Value), Convert.ToString(row.Cells[2].Value), Convert.ToString(row.Cells[3].Value), Convert.ToString(row.Cells[4].Value), Convert.ToString(row.Cells[5].Value), Convert.ToString(row.Cells[6].Value));

                            comprasItem.InsertarItems(Convert.ToInt32(idCompra),
                                                Convert.ToInt32(_idProducto),
                                                Convert.ToSingle(row.Cells[4].Value),
                                                Convert.ToSingle(row.Cells[5].Value),
                                                Convert.ToInt32(row.Cells[6].Value),
                                                Convert.ToSingle(row.Cells[7].Value));

                        }
                    }
                    dataGridView1.DataSource = "";
                    limpiarFormProveedor();
                    MessageBox.Show("se inserto correctamente");
                    Formulario f = Application.OpenForms.OfType<Formulario>().SingleOrDefault();
                    f.LoadUserData();
                }
            }
        }
    }
}
