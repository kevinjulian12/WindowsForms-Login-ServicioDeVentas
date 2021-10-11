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
    public partial class ListaDeProveedor : Form
    {
        public ListaDeProveedor()
        {
            InitializeComponent();
        }

        public void MostrarProveedor()
        {
            CN_Proveedor objeto = new CN_Proveedor();
            dataGridView1.DataSource = objeto.MostrarProv();
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

        private void ListaDeProveedor_Load(object sender, EventArgs e)
        {
            MostrarProveedor();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                FormCompras compras = Application.OpenForms.OfType<FormCompras>().SingleOrDefault();
                compras.idProveedor = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                compras.txtNombre.Text = dataGridView1.CurrentRow.Cells["Nombre"].Value.ToString();
                compras.txtRazon_Soccial.Text = dataGridView1.CurrentRow.Cells["Razon_Social"].Value.ToString();
                compras.txtEmail.Text = dataGridView1.CurrentRow.Cells["Email"].Value.ToString();
                Close();
            }
            else
                MessageBox.Show("Por favor seleccione una fila...");
        }
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            btnSelect.Enabled = true;
        }
    }
}
