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
    public partial class HistorialProveedor : Form
    {
        public HistorialProveedor()
        {
            InitializeComponent();
        }
        public int id;
        CN_Compras compras = new CN_Compras();
        CN_ComprasItem ComprasItem = new CN_ComprasItem();


        private void HistorialDeComprasClientes_Load(object sender, EventArgs e)
        {
            mostrar();
        }

        public void mostrar()
        {
            CN_Compras _Compras = new CN_Compras();
            dataGridView1.DataSource = _Compras.MostraHistComp(id);
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID"].Value);
                ComprasItem.Eliminar(ID);
                compras.Eliminar(ID);
                mostrar();
                MessageBox.Show("Eliminado correctamente");


            }
            else
            {
                MessageBox.Show("seleccione una fila por favor");
            }
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            try
             {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                    FormDetalleCompras detalleCompras = new FormDetalleCompras();
                    detalleCompras.ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                    detalleCompras.ShowDialog();
                    FormDetalleVenta formDetalleVenta = new FormDetalleVenta();
              
            }
            else
                MessageBox.Show("Por favor seleccione una fila...");
            }
            catch (Exception)
            {

             MessageBox.Show("No hay un registro seleccionado");
            }
        }
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            btnHistorial.Enabled = true;
            btnEliminar.Enabled = true;
        }
    }
}
