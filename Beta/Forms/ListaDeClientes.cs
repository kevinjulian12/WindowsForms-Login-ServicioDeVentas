using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Domain;

namespace Beta
{
    public partial class ListaDeClientes : Form
    {
        public ListaDeClientes()
        {
            InitializeComponent();
        }

        public void MostrarClientes()
        {
            CN_Clientes objeto = new CN_Clientes();
            dataGridView1.DataSource = objeto.MostrarClientes();
            dataGridView1.Columns.GetFirstColumn(0).Visible = false;
        }

        string NombreColumna = "";
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

        private void tuGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            NombreColumna = dataGridView1.Columns[e.ColumnIndex].DataPropertyName.Trim();
            textBox1.Enabled = true;
            label6.Visible = false;
        }

        private void ListaDeClientes_Load(object sender, EventArgs e)
        {
            MostrarClientes();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                FormVentas ventas = Application.OpenForms.OfType<FormVentas>().SingleOrDefault();
                ventas.IDcliente = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                ventas.txtNombre.Text = dataGridView1.CurrentRow.Cells["Nombre"].Value.ToString();
                ventas.txtApellido.Text = dataGridView1.CurrentRow.Cells["Apellido"].Value.ToString();
                ventas.txtDireccion.Text = dataGridView1.CurrentRow.Cells["Direccion"].Value.ToString();
                Close();
            }
            else
            {
                MessageBox.Show("Por favor seleccione una fila...");
            }
        }
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            btnSelect.Enabled = true;
        }
    }
}
