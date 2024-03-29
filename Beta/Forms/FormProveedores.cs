﻿using Domain;
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
        private string id;
        private bool Editar = false;
        public FormProveedores()
        {
            InitializeComponent();
            btnCancelar.Enabled = false;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = false;
            btnCompras.Enabled = false;
        }
        private void FormProductos_Load(object sender, EventArgs e)
        {
            MostrarProveedor();
        }
        private void MostrarProveedor()
        {
            CN_Proveedor objeto = new CN_Proveedor();
            dataGridView1.DataSource = objeto.MostrarProv();
            dataGridView1.Columns.GetFirstColumn(0).Visible = false;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //INSERTAR
            if (Editar == false)
            {
                try
                {
                    if (txtDireccion.Texts.Length < 3 ||txtEmail.Texts.Length < 3 || txtnombre.Texts.Length < 3 || txtRazonSocial.Texts.Length < 3 || txtTelefono.Texts.Length < 6 || txtReferencia.Texts.Length < 3)
                    {
                        MessageBox.Show("Complete todos los campos, deben tener como minimo 3 caracter y telefono minimo de 6 caracter");
                    }
                    else
                    {
                        objetoCN.InsertarPRov(txtnombre.Texts, txtRazonSocial.Texts, txtDireccion.Texts, txtTelefono.Texts, txtReferencia.Texts, txtEmail.Texts);
                        MessageBox.Show("se inserto correctamente");
                        MostrarProveedor();
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
                    if (txtDireccion.Texts.Length < 3 || txtEmail.Texts.Length < 3 || txtnombre.Texts.Length < 3 || txtRazonSocial.Texts.Length < 3 || txtTelefono.Texts.Length < 6 || txtReferencia.Texts.Length < 3)
                    {
                        MessageBox.Show("Complete todos los campos, deben tener como minimo 3 caracter y telefono minimo de 6 caracter");
                    }
                    else
                    {
                        objetoCN.EditarProv(id, txtnombre.Texts, txtRazonSocial.Texts, txtDireccion.Texts, txtTelefono.Texts, txtReferencia.Texts, txtEmail.Texts);
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
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Editar = true;
                txtnombre.Texts = dataGridView1.CurrentRow.Cells["Nombre"].Value.ToString();
                txtRazonSocial.Texts = dataGridView1.CurrentRow.Cells["Razon_Social"].Value.ToString();
                txtDireccion.Texts = dataGridView1.CurrentRow.Cells["Direccion"].Value.ToString();
                txtTelefono.Texts = dataGridView1.CurrentRow.Cells["Telefono"].Value.ToString();
                txtReferencia.Texts = dataGridView1.CurrentRow.Cells["Referencia"].Value.ToString();
                txtEmail.Texts = dataGridView1.CurrentRow.Cells["Email"].Value.ToString();
                id = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
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
            txtRazonSocial.Texts = "";
            txtDireccion.Texts = "";
            txtTelefono.Texts = "";
            txtReferencia.Texts = "";
            txtnombre.Texts = "";
            txtEmail.Texts = "";
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
            if (txtRazonSocial.Texts == "" && txtDireccion.Texts == "" && txtReferencia.Texts == "" && txtnombre.Texts == "" && txtTelefono.Texts == ""&& txtEmail.Texts=="")
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
            historialProveedor.id= Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID"].Value);
            historialProveedor.ShowDialog();
        }

    }
}
