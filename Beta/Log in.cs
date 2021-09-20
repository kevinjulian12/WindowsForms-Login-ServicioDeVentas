using Common.Cache;
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

            if (txtUsername.Texts != "Username" && txtUsername.Texts.Length > 2)
            {
                if (encryptHelper.Sha256Encrypt(txtPassword.Texts) != "Password")
                {
                    CN_Usuario user = new CN_Usuario();
                    var validLogin = user.LoginUser(txtUsername.Texts, txtPassword.Texts);
                    if (validLogin == true)
                    {
                        Formulario mainMenu = new Formulario();
                        // MessageBox.Show("Bienvenido " + UserCache.FirstName + ", " + UserCache.LastName);
                        mainMenu.Maximizar();
                        mainMenu.Show();
                        mainMenu.FormClosed += Logout;
                        this.Hide();
                    }
                    else
                    {
                        msgError("Nombre de usuario o contraseña incorrectos. Por favor intente nuevamente.");
                        txtPassword.Texts = "";
                        txtPassword.PasswordChar = true;
                        
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
            txtPassword.Texts = "";
            txtPassword.PasswordChar = true;
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
            if (txtUsername.Texts == "Usuario")
            {
                txtUsername.Texts = "";
                txtUsername.ForeColor = Color.LightGray;
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