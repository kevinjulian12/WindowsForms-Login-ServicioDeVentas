using Domain;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Beta
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Entrar();
        }

        private void Entrar()
        {
            EncryptHelper encryptHelper = new EncryptHelper();

            if (txtUsername.Text != "Username" && txtUsername.TextLength > 2)
            {
                if (encryptHelper.Sha256Encrypt(txtPassword.Text) != "Password")
                {
                    UserModel user = new UserModel();
                    var validLogin = user.LoginUser(txtUsername.Text, txtPassword.Text);
                    if (validLogin == true)
                    {
                        Formulario mainMenu = new Formulario();
                        // MessageBox.Show("Bienvenido " + UserCache.FirstName + ", " + UserCache.LastName);
                        mainMenu.Show();
                        mainMenu.FormClosed += Logout;
                        this.Hide();
                    }
                    else
                    {
                        msgError("Nombre de usuario o contraseña incorrectos. Por favor intente nuevamente.");
                        txtPassword.Text = "";
                        txtPassword.UseSystemPasswordChar = false;
                        txtUsername.Focus();
                    }
                }
                else msgError("Por favor,ingrese contraseña.");
            }
            else msgError("Por favor ingrese nombre de usuario.");
        }

        private void msgError(string msg)
        {
            lblErrorMessage.Text = "    " + msg;
            lblErrorMessage.Visible = true;
        }

        private void Logout(object sender, FormClosedEventArgs e)
        {
            txtPassword.Text = "";
            txtPassword.UseSystemPasswordChar = false;
            txtUsername.Text = "";
            lblErrorMessage.Visible = false;
            this.Show();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            if (txtUsername.Text == "Usuario")
            {
                txtUsername.Text = "";
                txtUsername.ForeColor = Color.LightGray;
            }
        }
        private void txtUsername_Leave(object sender, EventArgs e)
        {
            if (txtUsername.Text == "")
            {
                txtUsername.Text = "Usuario";
                txtUsername.ForeColor = Color.Silver;

            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                Entrar();
            }
        }
    }
}