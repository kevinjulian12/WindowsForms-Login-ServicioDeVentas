using Common.Cache;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Domain;

namespace Beta
{
    public partial class Formulario : Form
    {
        login login = new login();
        public Formulario()
        {
            InitializeComponent();
            login.Close();
        }
        public void Formulario_Load(object sender, EventArgs e)
        {
            LoadUserData();           
            privilegio();
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private Form activeForm = null;
        public void openChildFormInPanel(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm1.Controls.Add(childForm);
            panelChildForm1.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }
        int lx, ly;
        int sw, sh;
       private void iconmaximizar_Click(object sender, EventArgs e)
        {
            lx = this.Location.X;
            ly = this.Location.Y;
            sw = this.Size.Width;
            sh = this.Size.Height;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            iconrestaurar.Visible = true;
            iconmaximizar.Visible = false;
        }
        public void Maximizar()
        {
            lx = this.Location.X;
            ly = this.Location.Y;
            sw = this.Size.Width;
            sh = this.Size.Height;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            iconrestaurar.Visible = true;
            iconmaximizar.Visible = false;
        }


        private void iconrestaurar_Click(object sender, EventArgs e)
        {
            this.Size = new Size(sw, sh);
            this.Location = new Point(lx, ly);
            iconrestaurar.Visible = false;
            iconmaximizar.Visible = true;

        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void btnProductos_Click(object sender, EventArgs e)
        {
            FormProductos FProdu = new FormProductos();
            openChildFormInPanel(FProdu);
            label4.Text = "Productos";
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            FormVentas Fvent = new FormVentas();
            openChildFormInPanel(Fvent);
            label4.Text = "Ventas";
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            FormProveedores fprove = new FormProveedores();
            openChildFormInPanel(fprove);
            label4.Text = "Proveedores";
        }

        private void btnCompras_Click(object sender, EventArgs e)
        {
            FormCompras formCompras = new FormCompras();
            openChildFormInPanel(formCompras);
            label4.Text = "Compras";
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
           // if (MessageBox.Show("Are you sure to log out?", "Warning",
            //   MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                this.Close();
        }

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            FormClientes flc = new FormClientes();
            openChildFormInPanel(flc);
            label4.Text = "Clientes";
        }
        //
        private int tolerance = 15;
        private const int WM_NCHITTEST = 132;
        private const int HTBOTTOMRIGHT = 17;
        private Rectangle sizeGripRectangle;

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    var hitPoint = this.PointToClient(new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16));
                    if (sizeGripRectangle.Contains(hitPoint))
                        m.Result = new IntPtr(HTBOTTOMRIGHT);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        //
        private void btnMenu_Click(object sender, EventArgs e)
        {
            //-------CON EFECTO SLIDING
            if (panelSideMenu.Width == 230)
            {
                this.tmContraerMenu.Start();
            }
            else if (panelSideMenu.Width == 55)
            {
                this.tmExpandirMenu.Start();
            }

            //-------SIN EFECTO 
            //if (panelMenu.Width == 55)
            //{
            //    panelMenu.Width = 230;
            //}
            //else

            //    panelMenu.Width = 55;
        }
        private void tmExpandirMenu_Tick(object sender, EventArgs e)
        {
            if (panelSideMenu.Width >= 230)
                this.tmExpandirMenu.Stop();
            else
                panelSideMenu.Width = panelSideMenu.Width + 5;

        }

        private void tmContraerMenu_Tick(object sender, EventArgs e)
        {
            if (panelSideMenu.Width <= 55)
                this.tmContraerMenu.Stop();
            else
                panelSideMenu.Width = panelSideMenu.Width - 5;
        }

        //----------------DIBUJAR RECTANGULO / EXCLUIR ESQUINA PANEL 
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            var region = new Region(new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));

            sizeGripRectangle = new Rectangle(this.ClientRectangle.Width - tolerance, this.ClientRectangle.Height - tolerance, tolerance, tolerance);

            region.Exclude(sizeGripRectangle);
            this.panelChildForm1.Region = region;
            this.Invalidate();
        }
        //----------------COLOR Y GRIP DE RECTANGULO INFERIOR
        protected override void OnPaint(PaintEventArgs e)
        {

            SolidBrush blueBrush = new SolidBrush(Color.FromArgb(55, 61, 69));
            e.Graphics.FillRectangle(blueBrush, sizeGripRectangle);

            base.OnPaint(e);
            ControlPaint.DrawSizeGrip(e.Graphics, Color.Transparent, sizeGripRectangle);
        }
        //
        private void btnReportes_Click(object sender, EventArgs e)
        {
            if(SubmenuReportes.Visible == true) { SubmenuReportes.Visible = false; }
            else SubmenuReportes.Visible = true;
        }
        
        private void btnrptventa_Click(object sender, EventArgs e)
        {
            SubmenuReportes.Visible = false;
            FormReporteVentas fv = new FormReporteVentas();
            openChildFormInPanel(fv);
            label4.Text = "Reportes de Ventas";
        }

        private void btnrptcompra_Click(object sender, EventArgs e)
        {
            SubmenuReportes.Visible = false;
        }

        private void btnrptpagos_Click(object sender, EventArgs e)
        {
            SubmenuReportes.Visible = false;
        }
      


        private void button6_Click(object sender, EventArgs e)
        {
            Pagos pagos = new Pagos();
            openChildFormInPanel(pagos);
            label4.Text = "Pagos";
        }

      

        private void botonUsuario_Click_1(object sender, EventArgs e)
        {
            Editar_Perfil editar_Perfil = new Editar_Perfil();
            openChildFormInPanel(editar_Perfil);
            label4.Text = "Mi Perfil";
        }

        private void boton1_Click(object sender, EventArgs e)
        {
            

        }

        public void LoadUserData()
        {
            CN_Productos productos = new CN_Productos();
            int stock = 0;
           //if ()
           //{
           //   // var stock +Convert.ToInt32(productos.consultarStock2().Rows);
           //}

            boton1.Text = Convert.ToString( stock);
            botonUsuario.Text = UserCache.LoginName;
           
        }

        private void privilegio()
        {
            if(UserCache.Position != "Administrator") {
                btnProductos.Enabled = false;
                btnReportes.Enabled = false;
                btnrptcompra.Enabled = false;
            }
            
        }
        
    }
   
}
