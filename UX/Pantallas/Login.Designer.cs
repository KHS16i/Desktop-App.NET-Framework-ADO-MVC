
namespace UX_WinForms_.Pantallas
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.btn_Login = new System.Windows.Forms.Button();
            this.txt_Usuario = new System.Windows.Forms.TextBox();
            this.txt_Contrasenna = new System.Windows.Forms.TextBox();
            this.btn_Limpiar = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.btn_eye = new System.Windows.Forms.PictureBox();
            this.btn_eyeOff = new System.Windows.Forms.PictureBox();
            this.errorProviderLogIn = new System.Windows.Forms.ErrorProvider(this.components);
            this.pn_PrincipalLogin = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_eye)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_eyeOff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderLogIn)).BeginInit();
            this.pn_PrincipalLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Login
            // 
            this.btn_Login.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Login.BackColor = System.Drawing.Color.DimGray;
            this.btn_Login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Login.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btn_Login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Login.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Login.ForeColor = System.Drawing.Color.White;
            this.btn_Login.Location = new System.Drawing.Point(180, 453);
            this.btn_Login.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(292, 52);
            this.btn_Login.TabIndex = 3;
            this.btn_Login.Text = "Ingresar";
            this.btn_Login.UseVisualStyleBackColor = false;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // txt_Usuario
            // 
            this.txt_Usuario.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txt_Usuario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_Usuario.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Usuario.Location = new System.Drawing.Point(191, 292);
            this.txt_Usuario.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_Usuario.Name = "txt_Usuario";
            this.txt_Usuario.Size = new System.Drawing.Size(271, 31);
            this.txt_Usuario.TabIndex = 0;
            this.txt_Usuario.Validating += new System.ComponentModel.CancelEventHandler(this.txt_Usuario_Validating);
            // 
            // txt_Contrasenna
            // 
            this.txt_Contrasenna.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txt_Contrasenna.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_Contrasenna.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Contrasenna.Location = new System.Drawing.Point(191, 362);
            this.txt_Contrasenna.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_Contrasenna.Name = "txt_Contrasenna";
            this.txt_Contrasenna.Size = new System.Drawing.Size(271, 31);
            this.txt_Contrasenna.TabIndex = 1;
            this.txt_Contrasenna.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Contrasenna_KeyPress);
            this.txt_Contrasenna.Validating += new System.ComponentModel.CancelEventHandler(this.txt_Contrasenna_Validating);
            // 
            // btn_Limpiar
            // 
            this.btn_Limpiar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Limpiar.BackColor = System.Drawing.Color.DimGray;
            this.btn_Limpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Limpiar.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btn_Limpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Limpiar.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Limpiar.ForeColor = System.Drawing.Color.White;
            this.btn_Limpiar.Location = new System.Drawing.Point(180, 512);
            this.btn_Limpiar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Limpiar.Name = "btn_Limpiar";
            this.btn_Limpiar.Size = new System.Drawing.Size(292, 52);
            this.btn_Limpiar.TabIndex = 4;
            this.btn_Limpiar.Text = "Limpiar";
            this.btn_Limpiar.UseVisualStyleBackColor = false;
            this.btn_Limpiar.Click += new System.EventHandler(this.btn_Limpiar_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(119, 292);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(52, 42);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(121, 362);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(52, 42);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 8;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(175, 298);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(303, 57);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 9;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox5.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(175, 368);
            this.pictureBox5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(303, 57);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 10;
            this.pictureBox5.TabStop = false;
            // 
            // btn_eye
            // 
            this.btn_eye.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_eye.BackColor = System.Drawing.Color.Transparent;
            this.btn_eye.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_eye.Image = ((System.Drawing.Image)(resources.GetObject("btn_eye.Image")));
            this.btn_eye.Location = new System.Drawing.Point(428, 361);
            this.btn_eye.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_eye.Name = "btn_eye";
            this.btn_eye.Size = new System.Drawing.Size(47, 34);
            this.btn_eye.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_eye.TabIndex = 11;
            this.btn_eye.TabStop = false;
            this.btn_eye.Click += new System.EventHandler(this.btn_eye_Click);
            // 
            // btn_eyeOff
            // 
            this.btn_eyeOff.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_eyeOff.BackColor = System.Drawing.Color.Transparent;
            this.btn_eyeOff.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_eyeOff.Image = ((System.Drawing.Image)(resources.GetObject("btn_eyeOff.Image")));
            this.btn_eyeOff.Location = new System.Drawing.Point(428, 361);
            this.btn_eyeOff.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_eyeOff.Name = "btn_eyeOff";
            this.btn_eyeOff.Size = new System.Drawing.Size(47, 34);
            this.btn_eyeOff.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_eyeOff.TabIndex = 12;
            this.btn_eyeOff.TabStop = false;
            this.btn_eyeOff.Visible = false;
            this.btn_eyeOff.Click += new System.EventHandler(this.btn_eyeOff_Click);
            // 
            // errorProviderLogIn
            // 
            this.errorProviderLogIn.ContainerControl = this;
            // 
            // pn_PrincipalLogin
            // 
            this.pn_PrincipalLogin.Controls.Add(this.btn_eyeOff);
            this.pn_PrincipalLogin.Controls.Add(this.btn_Limpiar);
            this.pn_PrincipalLogin.Controls.Add(this.btn_eye);
            this.pn_PrincipalLogin.Controls.Add(this.txt_Usuario);
            this.pn_PrincipalLogin.Controls.Add(this.btn_Login);
            this.pn_PrincipalLogin.Controls.Add(this.pictureBox2);
            this.pn_PrincipalLogin.Controls.Add(this.txt_Contrasenna);
            this.pn_PrincipalLogin.Controls.Add(this.pictureBox3);
            this.pn_PrincipalLogin.Controls.Add(this.pictureBox5);
            this.pn_PrincipalLogin.Controls.Add(this.pictureBox4);
            this.pn_PrincipalLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pn_PrincipalLogin.Location = new System.Drawing.Point(0, 0);
            this.pn_PrincipalLogin.Margin = new System.Windows.Forms.Padding(4);
            this.pn_PrincipalLogin.Name = "pn_PrincipalLogin";
            this.pn_PrincipalLogin.Size = new System.Drawing.Size(652, 693);
            this.pn_PrincipalLogin.TabIndex = 14;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(652, 693);
            this.Controls.Add(this.pn_PrincipalLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Activated += new System.EventHandler(this.Login_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Login_FormClosed);
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_eye)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_eyeOff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderLogIn)).EndInit();
            this.pn_PrincipalLogin.ResumeLayout(false);
            this.pn_PrincipalLogin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Login;
        private System.Windows.Forms.TextBox txt_Usuario;
        private System.Windows.Forms.TextBox txt_Contrasenna;
        private System.Windows.Forms.Button btn_Limpiar;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox btn_eye;
        private System.Windows.Forms.PictureBox btn_eyeOff;
        private System.Windows.Forms.ErrorProvider errorProviderLogIn;
        private System.Windows.Forms.Panel pn_PrincipalLogin;
    }
}