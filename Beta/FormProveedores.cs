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
    public partial class FormProveedores : Form
    {
        CN_Proveedor objetoCN = new CN_Proveedor();
        private string idProveedor = null;
        private bool Editar = false;
        public FormProveedores()
        {
            InitializeComponent();
            btnCancelar.Enabled = false;
        }
        private void FormProductos_Load(object sender, EventArgs e)
        {
            MostrarProveedor();
        }
        private void MostrarProveedor()
        {
            CN_Proveedor objeto = new CN_Proveedor();
            dataGridView1.DataSource = objeto.MostrarProv();
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //INSERTAR
            if (Editar == false)
            {
                try
                {
                    objetoCN.InsertarPRov(txtnombre.Text, txtRazonSocial.Text, txtDireccion.Text, txtTelefono.Text, txtReferencia.Text, txtEmail.Text);
                    MessageBox.Show("se inserto correctamente");
                    MostrarProveedor();
                    limpiarForm();
                    textBox1.Enabled = false;
                    textBox1.Clear();
                    label6.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("no se pudo insertar los datos por: " + ex);
                }
            }
            //EDITAR
            if (Editar == true)
            {
                try
                {
                    objetoCN.EditarProv(txtid.Text,txtnombre.Text, txtRazonSocial.Text, txtDireccion.Text, txtTelefono.Text, txtReferencia.Text, txtEmail.Text);
                    MessageBox.Show("se edito correctamente");
                    MostrarProveedor();
                    limpiarForm();
                    Editar = false;
                    btnCancelar.Enabled = false;
                    dataGridView1.Enabled = true;
                    textBox1.Enabled = false;
                    textBox1.Clear();
                    label6.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("no se pudo editar los datos por: " + ex);
                }
            }
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Editar = true;
                txtnombre.Text = dataGridView1.CurrentRow.Cells["Nombre"].Value.ToString();
                txtRazonSocial.Text = dataGridView1.CurrentRow.Cells["Razon_Social"].Value.ToString();
                txtDireccion.Text = dataGridView1.CurrentRow.Cells["Direccion"].Value.ToString();
                txtTelefono.Text = dataGridView1.CurrentRow.Cells["Telefono"].Value.ToString();
                txtReferencia.Text = dataGridView1.CurrentRow.Cells["Referencia"].Value.ToString();
                txtEmail.Text = dataGridView1.CurrentRow.Cells["Email"].Value.ToString();
                txtid.Text = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
                dataGridView1.Enabled = false;
                btnCancelar.Enabled = true;
            }
            else
                MessageBox.Show("seleccione una fila por favor");
        }
        private void limpiarForm()
        {
            txtRazonSocial.Clear();
            txtDireccion.Text = "";
            txtTelefono.Clear();
            txtReferencia.Clear();
            txtnombre.Clear();
            txtEmail.Clear();
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                idProveedor = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                objetoCN.EliminarPRov(idProveedor);
                MessageBox.Show("Eliminado correctamente");
                MostrarProveedor();
                btnEliminar.Enabled = false;
                btnEditar.Enabled = false;
                btnCancelar.Enabled = false;
                textBox1.Enabled = false;
                textBox1.Clear();
                label6.Visible = true;
            }
            else
                MessageBox.Show("seleccione una fila por favor");
        }
        string NombreColumna = "";


        private void tuGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            NombreColumna = dataGridView1.Columns[e.ColumnIndex].DataPropertyName.Trim();
            textBox1.Enabled = true;
            label6.Visible = false;
        }

        private void FiltrarDatosDatagridview(DataGridView datagrid, string nombre_columna, TextBox txt_buscar)
        {
            ///Al texto recibido si contiene un asterisco (*) lo reemplazo de la cadena
            ///para que no provoque una excepción.
            string cadena = txt_buscar.Text.Trim().Replace("*", "");
            string filtro = string.Format("convert([{0}], System.String) LIKE '{1}%'", nombre_columna, cadena);

            ///A la vista del DataGridView con la propiedad RowFilter
            ///se le asigna la cadena del filtro para mostrarla en el DataGridView
            (datagrid.DataSource as DataTable).DefaultView.RowFilter = filtro;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            FiltrarDatosDatagridview(dataGridView1, NombreColumna, textBox1);
        }
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            btnEliminar.Enabled = true;
            btnEditar.Enabled = true;
            btnCancelar.Enabled = false;
            btnCompras.Enabled = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEliminar.Enabled = false;
            btnEditar.Enabled = false;
            btnCancelar.Enabled = false;
            btnCompras.Enabled = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = true;
            Editar = false;
            
            limpiarForm();
            textBox1.Enabled = false;
            textBox1.Clear();
            label6.Visible = true;
            btnCancelar.Enabled = false;
            // if (dataGridView1.SelectedRows.Count > 0)
            // {
            //    btnCancelar.Enabled = true;
            //}
        }
        private void Activar_Guardar(object sender, EventArgs e)
        {
            if (txtRazonSocial.Text == "" && txtDireccion.Text == "" && txtReferencia.Text == "" && txtnombre.Text == "" && txtTelefono.Text == ""&& txtEmail.Text=="")
            {
                btnGuardar.Enabled = false;
            }
            else
            {
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;
            }
        }

        private void btnCompras_Click(object sender, EventArgs e)
        {
            HistorialProveedor historialProveedor = new HistorialProveedor();
            historialProveedor.ShowDialog();
        }
    }
}
