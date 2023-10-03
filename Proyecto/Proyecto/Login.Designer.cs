namespace Proyecto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            panel1 = new Panel();
            pictureBox3 = new PictureBox();
            txtMail = new TextBox();
            txtPass = new TextBox();
            label1 = new Label();
            btnIngresar = new Button();
            linkPass = new LinkLabel();
            btnCerrar = new PictureBox();
            btnMinimizar = new PictureBox();
            linkRegistro = new LinkLabel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnCerrar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnMinimizar).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(0, 122, 204);
            panel1.Controls.Add(pictureBox3);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(250, 330);
            panel1.TabIndex = 0;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(36, 93);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(180, 120);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 0;
            pictureBox3.TabStop = false;
            // 
            // txtMail
            // 
            txtMail.BackColor = Color.FromArgb(15, 15, 15);
            txtMail.BorderStyle = BorderStyle.FixedSingle;
            txtMail.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtMail.ForeColor = Color.DimGray;
            txtMail.Location = new Point(311, 82);
            txtMail.Name = "txtMail";
            txtMail.PlaceholderText = "  Ingrese su mail";
            txtMail.Size = new Size(408, 27);
            txtMail.TabIndex = 2;
            txtMail.Enter += txtMail_Enter;
            txtMail.Leave += txtMail_Leave;
            // 
            // txtPass
            // 
            txtPass.BackColor = Color.FromArgb(15, 15, 15);
            txtPass.BorderStyle = BorderStyle.FixedSingle;
            txtPass.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtPass.ForeColor = Color.DimGray;
            txtPass.Location = new Point(311, 137);
            txtPass.Name = "txtPass";
            txtPass.PlaceholderText = "  Ingrese su contraseña";
            txtPass.Size = new Size(408, 27);
            txtPass.TabIndex = 3;
            txtPass.Enter += txtPass_Enter;
            txtPass.Leave += txtPass_Leave;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Calibri", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.DimGray;
            label1.Location = new Point(481, 18);
            label1.Name = "label1";
            label1.Size = new Size(84, 33);
            label1.TabIndex = 5;
            label1.Text = "LOGIN";
            // 
            // btnIngresar
            // 
            btnIngresar.BackColor = Color.FromArgb(40, 40, 40);
            btnIngresar.FlatAppearance.BorderSize = 0;
            btnIngresar.FlatAppearance.MouseDownBackColor = Color.FromArgb(28, 28, 28);
            btnIngresar.FlatAppearance.MouseOverBackColor = Color.FromArgb(64, 64, 64);
            btnIngresar.FlatStyle = FlatStyle.Flat;
            btnIngresar.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnIngresar.ForeColor = Color.LightGray;
            btnIngresar.Location = new Point(311, 212);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(408, 40);
            btnIngresar.TabIndex = 4;
            btnIngresar.Text = "Iniciar sesion";
            btnIngresar.UseVisualStyleBackColor = false;
            // 
            // linkPass
            // 
            linkPass.ActiveLinkColor = Color.FromArgb(0, 122, 204);
            linkPass.AutoSize = true;
            linkPass.Font = new Font("Calibri", 9F, FontStyle.Regular, GraphicsUnit.Point);
            linkPass.LinkColor = Color.DimGray;
            linkPass.Location = new Point(394, 275);
            linkPass.Name = "linkPass";
            linkPass.Size = new Size(163, 14);
            linkPass.TabIndex = 0;
            linkPass.TabStop = true;
            linkPass.Text = "¿Ha olvidado la contraseña?";
            // 
            // btnCerrar
            // 
            btnCerrar.Image = (Image)resources.GetObject("btnCerrar.Image");
            btnCerrar.Location = new Point(762, 4);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(15, 15);
            btnCerrar.SizeMode = PictureBoxSizeMode.Zoom;
            btnCerrar.TabIndex = 6;
            btnCerrar.TabStop = false;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // btnMinimizar
            // 
            btnMinimizar.Image = (Image)resources.GetObject("btnMinimizar.Image");
            btnMinimizar.Location = new Point(741, 4);
            btnMinimizar.Name = "btnMinimizar";
            btnMinimizar.Size = new Size(15, 15);
            btnMinimizar.SizeMode = PictureBoxSizeMode.Zoom;
            btnMinimizar.TabIndex = 7;
            btnMinimizar.TabStop = false;
            btnMinimizar.Click += btnMinimizar_Click;
            // 
            // linkRegistro
            // 
            linkRegistro.ActiveLinkColor = Color.FromArgb(0, 122, 204);
            linkRegistro.AutoSize = true;
            linkRegistro.Font = new Font("Calibri", 9F, FontStyle.Regular, GraphicsUnit.Point);
            linkRegistro.LinkColor = Color.DimGray;
            linkRegistro.Location = new Point(563, 275);
            linkRegistro.Name = "linkRegistro";
            linkRegistro.Size = new Size(69, 14);
            linkRegistro.TabIndex = 1;
            linkRegistro.TabStop = true;
            linkRegistro.Text = "Registrarse";
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 15, 15);
            ClientSize = new Size(780, 330);
            Controls.Add(linkRegistro);
            Controls.Add(btnMinimizar);
            Controls.Add(btnCerrar);
            Controls.Add(linkPass);
            Controls.Add(btnIngresar);
            Controls.Add(label1);
            Controls.Add(txtPass);
            Controls.Add(txtMail);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Login";
            Opacity = 0.9D;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            MouseDown += Login_MouseDown;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnCerrar).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnMinimizar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private TextBox txtMail;
        private TextBox txtPass;
        private Label label1;
        private Button btnIngresar;
        private LinkLabel linkPass;
        private PictureBox btnCerrar;
        private PictureBox btnMinimizar;
        private PictureBox pictureBox3;
        private LinkLabel linkRegistro;
    }
}