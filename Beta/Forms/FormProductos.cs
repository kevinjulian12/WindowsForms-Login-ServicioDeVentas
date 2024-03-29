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
    public partial class FormProductos : Form
    {
        CN_Productos objetoCN = new CN_Productos();
        private string idProducto = null;
        private bool Editar = false;
        private string id;
        private string costo = "0";

        public FormProductos()
        {
            InitializeComponent();
            btnCancelar.Enabled = false;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = false;
        }

        private void FormProductos_Load(object sender, EventArgs e)
        {
            MostrarProductos();
        }

        private void MostrarProductos()
        {
            CN_Productos objeto = new CN_Productos();
            dataGridView1.DataSource = objeto.MostrarProductos();
            dataGridView1.Columns.GetFirstColumn(0).Visible = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //INSERTAR
            if (Editar == false)
            {
                try
                {
                    if (txtPrecio.Texts.Length == 0)
                    {
                        MessageBox.Show("Ingrese un valor a precio");
                    }
                    else
                    {
                        if (txtStock.Texts.Length == 0)
                        {
                            MessageBox.Show("Ingrese un valor a stock");
                        }
                        else
                        {
                            if (txtMarca.Texts.Length == 0 || txtNombre.Texts.Length == 0 || txtDesc.Texts.Length == 0)
                            {
                                MessageBox.Show("Complete todos los campos");
                            }
                            else
                            {
                                costo = "0";
                                objetoCN.InsertarProducto(txtNombre.Texts, txtDesc.Texts, txtMarca.Texts,Convert.ToString(costo) ,txtPrecio.Texts, txtStock.Texts);
                                MessageBox.Show("se inserto correctamente");
                                MostrarProductos();
                                limpiarForm();
                                textBox1.Enabled = false;
                                textBox1.Clear();
                                label6.Visible = true;
                                Formulario f = Application.OpenForms.OfType<Formulario>().SingleOrDefault();
                                f.LoadUserData();
                            }
                        }
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
                    if (txtPrecio.Texts.Length == 0)
                    {
                        MessageBox.Show("Ingrese un valor a precio");
                    }
                    else
                    {
                        if (txtStock.Texts.Length == 0)
                        {
                            MessageBox.Show("Ingrese un valor a stock");
                        }
                        else
                        {
                            if (txtMarca.Texts.Length == 0 || txtNombre.Texts.Length == 0 || txtDesc.Texts.Length == 0)
                            {
                                MessageBox.Show("Complete todos los campos");
                            }
                            else
                            {
                                objetoCN.EditarProducto(txtNombre.Texts, txtDesc.Texts, txtMarca.Texts,costo , txtPrecio.Texts, txtStock.Texts, id);
                                MessageBox.Show("se edito correctamente");
                                MostrarProductos();
                                limpiarForm();
                                Editar = false;
                                btnCancelar.Enabled = false;
                                dataGridView1.Enabled = true;
                                textBox1.Enabled = false;
                                textBox1.Clear();
                                label6.Visible = true;
                                Formulario f = Application.OpenForms.OfType<Formulario>().SingleOrDefault();
                                f.LoadUserData();
                            }
                        }
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
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
            // Si deseas, puedes permitir numeros decimales (o float)
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
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
                txtNombre.Texts = dataGridView1.CurrentRow.Cells["Producto"].Value.ToString();
                txtMarca.Texts = dataGridView1.CurrentRow.Cells["Marca"].Value.ToString();
                txtDesc.Texts = dataGridView1.CurrentRow.Cells["Descripcion"].Value.ToString();
                txtPrecio.Texts = dataGridView1.CurrentRow.Cells["Precio"].Value.ToString();
                txtStock.Texts = dataGridView1.CurrentRow.Cells["Stock"].Value.ToString();
                id = dataGridView1.CurrentRow.Cells["Id"].Value.ToString();
                costo = dataGridView1.CurrentRow.Cells["Costo"].Value.ToString();
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
            txtDesc.Texts = "";
            txtMarca.Texts = "";
            txtPrecio.Texts = "";
            txtStock.Texts = "";
            txtNombre.Texts = "";
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                idProducto = dataGridView1.CurrentRow.Cells["Id"].Value.ToString();
                objetoCN.EliminarProducto(idProducto);
                MessageBox.Show("Eliminado correctamente");
                MostrarProductos();
                btnEliminar.Enabled = false;
                btnEditar.Enabled = false;
                btnCancelar.Enabled = false;
                textBox1.Enabled = false;
                textBox1.Clear();
                label6.Visible = true;
                Formulario f = Application.OpenForms.OfType<Formulario>().SingleOrDefault();
                f.LoadUserData();
            }
            else
                MessageBox.Show("seleccione una fila por favor");
        }

        private void Activar_Guardar(object sender, EventArgs e)
        {
            if (txtDesc.Texts == "" && txtMarca.Texts == "" && txtNombre.Texts == "" && txtPrecio.Texts == "" && txtStock.Texts == "")
            {
                btnGuardar.Enabled = false;
            }
            else
            {
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            btnEliminar.Enabled = true;
            btnEditar.Enabled = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEliminar.Enabled = false;
            btnEditar.Enabled = false;
            btnCancelar.Enabled = false;
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
    }
}
