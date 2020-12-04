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
    public partial class FormListasClientes : Form
    {
        public bool Editar = false;
        private CN_Clientes objetoCN = new CN_Clientes();
        private string idCliente = null;
        
        public FormListasClientes()
        {
            InitializeComponent();
            btnCancelar.Enabled = false;
        }

        public void FormListasClientes_Load(object sender, EventArgs e)
        {
            MostrarClientes();
            
        }

        public void MostrarClientes()
        {
            CN_Clientes objeto = new CN_Clientes();
            dataGridView1.DataSource = objeto.MostrarClien();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Editar == false)
            {
                try
                {
                    objetoCN.InsertarClien(txtnombre.Text, txtapellido.Text, txtdireccion.Text, txttelefono.Text,txtLocalidad.Text);
                    MessageBox.Show("se inserto correctamente");
                    MostrarClientes();
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
                    objetoCN.EditarClien(txtid.Text, txtnombre.Text, txtapellido.Text, txtdireccion.Text, txttelefono.Text,txtLocalidad.Text);
                    MessageBox.Show("se edito correctamente");
                    MostrarClientes();
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
                txtid.Text = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                txtnombre.Text = dataGridView1.CurrentRow.Cells["Nombre"].Value.ToString();
                txtapellido.Text = dataGridView1.CurrentRow.Cells["Apellido"].Value.ToString();
                txtdireccion.Text = dataGridView1.CurrentRow.Cells["Direccion"].Value.ToString();
                txttelefono.Text = dataGridView1.CurrentRow.Cells["Telefono"].Value.ToString();
                txtLocalidad.Text = dataGridView1.CurrentRow.Cells["Localidad"].Value.ToString();
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
            txtnombre.Clear();
            txtapellido.Clear();
            txtdireccion.Clear();
            txttelefono.Clear();
            txtLocalidad.Clear();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                idCliente = dataGridView1.CurrentRow.Cells["Id"].Value.ToString();
                objetoCN.EliminarClien(idCliente);
                MessageBox.Show("Eliminado correctamente");
                MostrarClientes();
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
      

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            btnEliminar.Enabled = true;
            btnEditar.Enabled = true;
            btnCancelar.Enabled = false;
            btnHistorial.Enabled = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEliminar.Enabled = false;
            btnEditar.Enabled = false;
            btnCancelar.Enabled = false;
            btnHistorial.Enabled = false;
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
            //     btnCancelar.Enabled = true;
            // }
        }
        string NombreColumna = "";

        
        private void tuGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        { NombreColumna = dataGridView1.Columns[e.ColumnIndex].DataPropertyName.Trim();
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

        private void Activar_Guardar(object sender, EventArgs e)
        {
            if(txtapellido.Text == "" && txtdireccion.Text == "" && txtLocalidad.Text == "" && txtnombre.Text == "" && txttelefono.Text == "")
            {
                btnGuardar.Enabled = false;
            }
            else
            {
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;
            }
        }

        
        private void btnHistorial_Click(object sender, EventArgs e)
        {
            HistorialDeComprasClientes historial = new HistorialDeComprasClientes();
            historial.ShowDialog();
        }
    }
}
