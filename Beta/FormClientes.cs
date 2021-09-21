using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Domain;

namespace Beta
{
    public partial class FormClientes : Form
    {
        private bool Editar = false;
        private CN_Clientes objetoCN = new CN_Clientes();

        public FormClientes()
        {
            InitializeComponent();
            btnCancelar.Enabled = false;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = false;
            btnHistorial.Enabled = false;
        }

        public void FormListasClientes_Load(object sender, EventArgs e)
        {
            MostrarClientes();           
        }

        public void MostrarClientes()
        {
            CN_Clientes objeto = new CN_Clientes();
            dataGridView1.DataSource = objeto.MostrarClientes();
            dataGridView1.Columns.GetFirstColumn(0).Visible = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Editar == false)
            {
                try
                {
                    if (txtapellido.Texts.Length < 3 || txtdireccion.Texts.Length < 3 || txtLocalidad.Texts.Length < 3 || txtnombre.Texts.Length < 3 || txttelefono.Texts.Length < 6)
                    {
                        MessageBox.Show("Complete todos los campos, deben tener como minimo 3 caracter y telefono minimo de 6 caracter");
                    }
                    else
                    {
                        objetoCN.Nombre = txtnombre.Texts;
                        objetoCN.Apellido = txtapellido.Texts;
                        objetoCN.Direccion = txtdireccion.Texts;
                        objetoCN.Localidad = txtLocalidad.Texts;
                        objetoCN.Telefono = txttelefono.Texts;
                        objetoCN.InsertarCliente();
                        MessageBox.Show("se inserto correctamente");
                        MostrarClientes();
                        limpiarForm();
                        textBox1.Enabled = false;
                        textBox1.Clear();
                        label6.Visible = true;
                    }
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
                    if (txtapellido.Texts.Length < 3 || txtdireccion.Texts.Length < 3 || txtLocalidad.Texts.Length < 3 || txtnombre.Texts.Length < 3 || txttelefono.Texts.Length < 6)
                    {
                        MessageBox.Show("Complete todos los campos, deben tener como minimo 3 caracter y telefono minimo de 6 caracter");
                    }
                    else
                    {
                        objetoCN.Nombre = txtnombre.Texts;
                        objetoCN.Apellido = txtapellido.Texts;
                        objetoCN.Direccion = txtdireccion.Texts;
                        objetoCN.Localidad = txtLocalidad.Texts;
                        objetoCN.Telefono = txttelefono.Texts;
                        objetoCN.EditarCliente();
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show("no se pudo editar los datos por: " + ex);
                }
            }

        }

        private void txtCaracter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtCaracternDecimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // Si deseas, puedes permitir numeros decimales (o float)
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Editar = true;
                objetoCN.id = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                txtnombre.Texts = dataGridView1.CurrentRow.Cells["Nombre"].Value.ToString();
                txtapellido.Texts = dataGridView1.CurrentRow.Cells["Apellido"].Value.ToString();
                txtdireccion.Texts = dataGridView1.CurrentRow.Cells["Direccion"].Value.ToString();
                txttelefono.Texts = dataGridView1.CurrentRow.Cells["Telefono"].Value.ToString();
                txtLocalidad.Texts = dataGridView1.CurrentRow.Cells["Localidad"].Value.ToString();
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
                dataGridView1.Enabled = false;
                btnCancelar.Enabled = true;
            }
            else
            {
                MessageBox.Show("seleccione una fila por favor");
            }
        }

        private void limpiarForm()
        {
            txtnombre.Texts = "";
            txtapellido.Texts = "";
            txtdireccion.Texts = "";
            txttelefono.Texts = "";
            txtLocalidad.Texts = "";
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                objetoCN.id = dataGridView1.CurrentRow.Cells["Id"].Value.ToString();
                objetoCN.EliminarCliente();
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
            {
                MessageBox.Show("seleccione una fila por favor");
            }
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

        private void Activar_Guardar(object sender, EventArgs e)
        {
            if(txtapellido.Texts == "" && txtdireccion.Texts == "" && txtLocalidad.Texts == "" && txtnombre.Texts == "" && txttelefono.Texts == "")
            {
                btnGuardar.Enabled = false;
            }
            else
            {
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;
            }
        }

        private CN_Ventas ventas = new CN_Ventas();
        private void btnHistorial_Click(object sender, EventArgs e)
        {
            RegistrosClientes historial = new RegistrosClientes(ventas);
            ventas.IDCliente = Convert.ToString(dataGridView1.CurrentRow.Cells["ID"].Value);
            Formulario formulario = Application.OpenForms.OfType<Formulario>().SingleOrDefault();
            formulario.openChildFormInPanel(historial);
        }
    }
}
