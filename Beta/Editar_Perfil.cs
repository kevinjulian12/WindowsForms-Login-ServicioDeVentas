using Common.Cache;
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
    public partial class Editar_Perfil : Form
    {
        public Editar_Perfil()
        {
            InitializeComponent();
        }
        private void FormUserProfile_Load(object sender, EventArgs e)
        {
            loadUserData();
            initializePassEditControls();
        }
        private void loadUserData()
        {
            //View
            lblUser.Text = UserCache.LoginName;
            lblName.Text = UserCache.FirstName;
            lblLastName.Text = UserCache.LastName;
            lblMail.Text = UserCache.Email;
            lblPosition.Text = UserCache.Position;
            //Edit Panel
            txtUsername.Text = UserCache.LoginName;
            txtFirstName.Text = UserCache.FirstName;
            txtLastName.Text = UserCache.LastName;
            txtEmail.Text = UserCache.Email;
            txtPassword.Text = UserCache.Password;
            txtConfirmPass.Text = UserCache.Password;
            txtCurrentPassword.Text = "";
           
        }
        private void initializePassEditControls()
        {
            LinkEditPass.Text = "Editar";
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.Enabled = false;
            txtConfirmPass.UseSystemPasswordChar = true;
            txtConfirmPass.Enabled = false;
        }
        private void reset()
        {
            loadUserData();
            initializePassEditControls();
        }
        private void linkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel1.Visible = true;
            loadUserData();
        }
        private void LinkEditPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (LinkEditPass.Text == "Editar")
            {
                LinkEditPass.Text = "Cancelar";
                txtPassword.Enabled = true;
                txtPassword.Text = "";
                txtConfirmPass.Enabled = true;
                txtConfirmPass.Text = "";
            }
            else if (LinkEditPass.Text == "Cancelar")
            {
                initializePassEditControls();
                txtPassword.Text = UserCache.Password;
                txtConfirmPass.Text = UserCache.Password;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text.Length >= 5)
            {
                if (txtPassword.Text == txtConfirmPass.Text)
                {
                    if (txtCurrentPassword.Text == UserCache.Password)
                    {
                        var userModel = new UserModel(
                            idUser: UserCache.IdUser,
                            loginName: txtUsername.Text,
                            password: txtPassword.Text,
                            firstName: txtFirstName.Text,
                            lastName: txtLastName.Text,
                            position: null,
                            email: txtEmail.Text);                                              
                        var result = userModel.editUserProfile();
                        MessageBox.Show(result);
                        reset();                        
                        panel1.Visible = false;
                        Formulario f = Application.OpenForms.OfType<Formulario>().SingleOrDefault();
                        f.LoadUserData();
                    }
                    else
                        MessageBox.Show("Contraseña actual incorrecta, inténtelo de nuevo");
                }
                else
                    MessageBox.Show("La contraseña no coincide, inténtalo de nuevo.");
            }
            else
                MessageBox.Show("La contraseña debe tener un mínimo de 5 caracteres");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }
    }
}
