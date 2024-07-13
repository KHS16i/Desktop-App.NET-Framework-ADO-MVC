using System;
using System.Windows.Forms;
using DAL.Entidades;
using BLL.AccesoDatos;
using System.Configuration;

namespace UX_WinForms_.Pantallas
{
    public partial class Login : Form
    {
        #region VARIABLES GLOBALES

        cls_Login_DAL Obj_Login_DAL = new cls_Login_DAL();
        cls_Login_BLL Obj_Login_BLL = new cls_Login_BLL(ConfigurationManager.ConnectionStrings["SQL_AUT"].ToString()); // <---Cadena de conexion por Inyeccion de Dependencias

        Pantallas.Principal PantPrincipal = new Principal();

        #endregion

        #region EVENTOS

        public Login()
        {
            InitializeComponent();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            manejaVentanas();
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txt_Contrasenna.UseSystemPasswordChar = true;
            this.MaximizeBox = false;
        }

        private void btn_Limpiar_Click(object sender, EventArgs e)
        {
            limpiaCampos();
        }

        private void btn_eye_Click(object sender, EventArgs e)
        {
            btn_eyeOff.Visible = true;

            btn_eye.Visible = false;

            txt_Contrasenna.UseSystemPasswordChar = false;
        }

        private void btn_eyeOff_Click(object sender, EventArgs e)
        {
            btn_eyeOff.Visible = false;

            btn_eye.Visible = true;

            txt_Contrasenna.UseSystemPasswordChar = true;
        }

        private void txt_Contrasenna_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                manejaVentanas();
            }
        }

        private void Login_Activated(object sender, EventArgs e)
        {
            txt_Usuario.Focus();
        }


        private void txt_Usuario_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Usuario.Text))
            {
                errorProviderLogIn.SetError(txt_Usuario, "Debe ingresar un nombre de Usuario");
            }
            else
            {
                errorProviderLogIn.SetError(txt_Usuario, null);
            }
        }

        private void txt_Contrasenna_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Contrasenna.Text))
            {
                errorProviderLogIn.SetError(txt_Contrasenna, "Debe ingresar una contraseña");
            }
            else
            {
                errorProviderLogIn.SetError(txt_Contrasenna, null);
            }
        }

        #endregion

        #region METODOS

        public void capturaDatos()
        {
            Obj_Login_DAL.sUsuario = txt_Usuario.Text.Trim();
            Obj_Login_DAL.sConstrasenna = txt_Contrasenna.Text.Trim();

            Obj_Login_BLL.logIn(ref Obj_Login_DAL);
        }

        public void limpiaCampos()
        {
            txt_Usuario.Text = string.Empty;
            txt_Contrasenna.Text = string.Empty;

            txt_Usuario.Focus();

            errorProviderLogIn.SetError(txt_Usuario, null);
            errorProviderLogIn.SetError(txt_Contrasenna, null);
        }

        public void manejaVentanas()
        {
            capturaDatos();

            if (txt_Usuario.Text == string.Empty)
            {
                MessageBox.Show("Debe de ingresar un usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txt_Contrasenna.Text == string.Empty)
            {
                MessageBox.Show("Debe de ingresar una contraseña", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (cls_Login_DAL.bResLogin == true)
                {
                    this.Hide();

                    PantPrincipal.ShowDialog();

                    limpiaCampos();

                    errorProviderLogIn.SetError(txt_Usuario, null);
                    errorProviderLogIn.SetError(txt_Contrasenna, null);
                }
                else
                {
                    MessageBox.Show("El usuario no existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    limpiaCampos();
                }
            }

            this.Show();
        }

        #endregion

    }
}
